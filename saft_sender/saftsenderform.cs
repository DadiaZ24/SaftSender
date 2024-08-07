using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace saft_sender
{
    public partial class SaftSenderForm : Form
    {
        //Initialize the form design
        public SaftSenderForm()
        {
            InitializeComponent();
        }
        //Create the saft file path variable
        private string saft_file_path;
        private string jar_file_path;
        private string resume_saft_path;
        private string saft_file_name;

        //Open Saft Button Code
        private void opensaft_button_Click(object sender, EventArgs e)
        {
            var saftpath_dialog = opensaft_dialog.ShowDialog();
            if (saftpath_dialog == DialogResult.OK)
            {
                saft_file_path = opensaft_dialog.FileName;
                if (Path.GetExtension(saft_file_path).Equals(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    opensaft_button.Text = saft_file_path;

                    saft_file_name = Path.GetFileName(saft_file_path);
                    opensaft_button.Text = saft_file_name;
                }
                else
                    MessageBox.Show("Erro: O ficheiro não é do tipo xml!");
            }
        }

        //submit button Code
        private void submit_button_Click(object sender, EventArgs e)
        {
            string nif      = nif_txtbox.Text;
            string password = pass_txtbox.Text;
            string year     = year_txtbox.Text;
            string month    = month_txtbox.Text;

            try
            {
                //PARSING
                if (string.IsNullOrEmpty(nif) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(year) || string.IsNullOrEmpty(month))
                {
                    MessageBox.Show("Erro: Deve preencher todos os campos (NIF, password, ano e mês)");
                    return;
                }
                if (nif.Length != 9)
                {
                    MessageBox.Show("Erro: O NIF tem que conter 9 dígitos.");
                    return;
                }
                if (year.Length != 4)
                {
                    MessageBox.Show("Erro: O ano tem que conter 4 dígitos.");
                    return;
                }
                try
                {
                    int month_number = int.Parse(month);
                    if (month_number > 12 || month_number < 1)
                    {
                        MessageBox.Show("Erro: O mês deve estar no intervalo entre 1 e 12");
                        return;
                    }
                }
                catch
                {
                    MessageBox.Show("Erro: O mês deve ser um número!");
                    return;
                }
                if (string.IsNullOrEmpty(saft_file_path))
                {
                    MessageBox.Show("Erro: Não selecionou nenhum ficheiro saft!");
                    return;
                }
                if (string.IsNullOrEmpty(jar_file_path))
                {
                    MessageBox.Show("Erro: Não efetuou o download do ficheiro JAR!");
                    return;
                }
                if (string.IsNullOrEmpty(resume_saft_path))
                {
                    MessageBox.Show("Erro: Não selecionou um caminho para guardar o ficheiro resumido!");
                    return;
                }

                string cmd = $"-jar \"{jar_file_path}\" -n {nif} -p {password} -a {year} -m {month} -op enviar -i \"{saft_file_path}\" -o \"{resume_saft_path}\"";
                MessageBox.Show("A INICIAR");
            
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = "java",
                    Arguments = cmd,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };


                using (Process process = new Process())
                {
                    process.StartInfo = startInfo;
                    process.Start();

                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();
                    if (!string.IsNullOrEmpty(output))
                        MessageBox.Show("Output: " + output);
                    else if (!string.IsNullOrEmpty(error))
                        MessageBox.Show("ERROR: " + error);
                    else
                        MessageBox.Show("Nothing has been done.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error ocurred!" + ex);
            }

        }


        private async void downloadjar_button_Click(object sender, EventArgs e)
        {
           using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string url = "https://faturas.portaldasfinancas.gov.pt/factemipf_static/java/FACTEMICLI-2.8.4-60332-cmdClient.jar";
                    jar_file_path = Path.Combine(folderBrowserDialog.SelectedPath, "FACTEMICLI-2.8.4-60332-cmdClient.jar");

                    await DownloadJarFile(url, jar_file_path);
                }
            }
        }
       private void resumesaft_button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(saft_file_name))
            {
                MessageBox.Show("Erro: Deve selecionar o ficheiro saft primeiro!");
                return;
            }
            using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    resume_saft_path = Path.Combine(folderBrowserDialog.SelectedPath, "resumido_" + saft_file_name);
                    resumesaft_button.Text = "resumido_" + saft_file_name;
                }
            }
        }

        private async Task DownloadJarFile(string url, string jar_file_path)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    byte[] fileBytes = await client.GetByteArrayAsync(url);

                    File.WriteAllBytes(jar_file_path, fileBytes);
                    MessageBox.Show("Download do ficheiro efetuada com sucesso!");
                }
                catch
                {
                    MessageBox.Show("Erro a fazer download do ficheiro. Confirme se selecionou um caminho válido onde guardar o ficheiro!");
                }
            }
        }

        private void exit_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void loading_Click(object sender, EventArgs e)
        {

        }
    }
}
