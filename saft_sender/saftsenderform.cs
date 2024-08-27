using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace saft_sender
{
    public partial class SaftSenderForm : Form
    {
        //Initialize the form design
        public SaftSenderForm()
        {
            InitializeComponent();
            int currentyear = DateTime.Now.Year;
            for (int selectyear = currentyear; selectyear >= 2020; selectyear--)
            {
                this.year_combobox.Items.Add(selectyear.ToString());
            }
        }
        //Create the needed variables
        private string[] saft_file_path = new string[99];
        private string   jar_file_path;
        private string[] resume_saft_path = new string[99];
        private string[] saft_file_name = new string[99];
        private string   year;
        private string   month;
        private string[] status = new string[99];

        //error message box format create
        private void error_message_box(string errorstr)
        {
            MessageBox.Show(errorstr, "Ocorreu um erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //Open Saft Button Code
        private void opensaft_button_Click(object sender, EventArgs e)
        {
            OpenFileDialog opensaft_dialog = new OpenFileDialog
            {
                Multiselect = true
            };

            //Clear everything in resume saft - safety protocol
            if (!string.IsNullOrEmpty(resume_saft_path[0]))
            {
                for (int j = 0; j < resume_saft_path.Length; j++)
                    resume_saft_path[j] = null;
            }

            var saftpath_dialog = opensaft_dialog.ShowDialog();
            if (saftpath_dialog == DialogResult.OK)
            {
                saft_file_path = opensaft_dialog.FileNames;
                int saft_file_path_count = ArrayCount(saft_file_path);

                for (int i = 0; i < saft_file_path_count; i++)
                {
                    if (Path.GetExtension(saft_file_path[i]).Equals(".xml", StringComparison.OrdinalIgnoreCase))
                    {
                        int saft_total = saft_file_path_count;
                        if (saft_total == 1)
                        {
                            saft_file_name[0] = Path.GetFileName(saft_file_path[i]);
                            abrirsaft_path.Text = saft_file_name[0];
                        }
                        else if (saft_total <= 0)
                            error_message_box("Nenhum ficheiro selecionado!");
                        else
                        {
                            string saftfile_display = saft_file_path_count.ToString() + " ficheiros selecionados.";
                            abrirsaft_path.Text = saftfile_display;
                            saft_file_name[i] = Path.GetFileName(saft_file_path[i]);
                        }
                    }
                    else
                    {
                        error_message_box("Um dos ficheiros que tentou abrir não é do tipo xml!");
                        for (int j = 0; j < saft_file_path.Length; j++)
                            saft_file_path[i] = null;
                    }
                }

            }
        }

        //submit button Code
        private void submit_button_Click(object sender, EventArgs e)
        {
            string nif = nif_txtbox.Text;
            string password = pass_txtbox.Text;

            try
            {
                //PARSING
                if (string.IsNullOrEmpty(nif) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(year) || string.IsNullOrEmpty(month))
                {
                    error_message_box("Deve preencher todos os campos (NIF, password, ano e mês)");
                    return;
                }
                if (nif.Length != 9)
                {
                    error_message_box("O NIF tem que conter 9 dígitos.");
                    return;
                }
                if (string.IsNullOrEmpty(saft_file_path[0]))
                {
                    error_message_box("Não selecionou nenhum ficheiro saft!");
                    return;
                }
                if (string.IsNullOrEmpty(jar_file_path))
                {
                    error_message_box("Ocorreu um erro no download das dependências. Por favor, tente novamente.");
                    return;
                }
                if (string.IsNullOrEmpty(resume_saft_path[0]))
                {
                    error_message_box("Não selecionou um caminho para guardar o ficheiro resumido!");
                    return;
                }

                jar_download();
                int saft_file_path_count = ArrayCount(saft_file_path);
                for (int i = 0; i < saft_file_path_count; i++)
                {
                    string cmd = $"-jar {jar_file_path} -n {nif} -p {password} -a {year} -m {month} -op enviar -i \"{saft_file_path[i]}\" -md \"{resume_saft_path[i]}\"";

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
                        if (!string.IsNullOrEmpty(error))
                        {
                            if (errorParser(error, i) == true)
                            {
                                error_message_box(status[i]);
                                continue;
                            }
                        }
                        else if (!string.IsNullOrEmpty(output))
                        {
                            if (errorParser(output, i) == true)
                            {
                                error_message_box(status[i]);
                                continue;
                            }
                            else
                            {
                                nif_txtbox.Text = null;
                                pass_txtbox.Text = null;
                                for (int n = 0; n < saft_file_path_count; n++)
                                {
                                    saft_file_path[n] = null;
                                    saft_file_name[n] = null;
                                }
                                abrirsaft_path = null;
                            }
                        }
                    }

                }
                string terminus_message = string.Join(Environment.NewLine, status.Where(s => s != null));
                MessageBox.Show(terminus_message, "Informação de Envio:", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error ocurred!" + ex);
            }


        }

        //DOWNLOAD JAR BUTTON --------------------------------------------------------------------------------

        private async void jar_download()
        {
            string url = "https://faturas.portaldasfinancas.gov.pt/factemipf_static/java/FACTEMICLI-2.8.4-60332-cmdClient.jar";
            jar_file_path = "./jar/FACTEMICLI-2.8.4-60332-cmdClient.jar";
            await DownloadJarFile(url, jar_file_path);
        }

        //RESUME SAFT BUTTON CLICK---------------------------------------------------------------
        private void resumesaft_button_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(saft_file_name[0]))
            {
                MessageBox.Show("Erro: Deve selecionar o ficheiro saft primeiro!");
                return;
            }
            using (var folderBrowserDialog = new OpenFileDialog())
            {
                folderBrowserDialog.CheckFileExists = false;
                folderBrowserDialog.ValidateNames = false;
                folderBrowserDialog.ShowReadOnly = false;
                folderBrowserDialog.Filter = "Folders|*.none";
                folderBrowserDialog.FileName = "Folder Selection";

                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string folderpath = System.IO.Path.GetDirectoryName(folderBrowserDialog.FileName);
                    int saft_file_path_count = ArrayCount(saft_file_path);
                    for (int i = 0; i < saft_file_path_count; i++)
                    {
                        resume_saft_path[i] = Path.Combine(folderpath, "resumido_" + saft_file_name[i]);
                        if (saft_file_path_count <= 0)
                        {
                            error_message_box("Deve selecionar um ficheiro saft primeiro!");
                            return;
                        }
                        else
                            resume_path.Text = folderpath;
                    }
                }
            }
        }

        //DOWNLOAD JAR FILE TASK ----------------------------------------------------------------------
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

        //EXIT BUTTON ---------------------------------------------------------------------------------
        private void exit_button_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void year_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (year_combobox.SelectedItem.ToString() != null)
            {
                year = year_combobox.SelectedItem.ToString();
            }

        }
        private void month_combobox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (month_combobox.SelectedItem.ToString() != null)
            {
                if (month_combobox.SelectedItem.ToString().Contains("01"))
                    month = "01";
                else if (month_combobox.SelectedItem.ToString().Contains("02"))
                    month = "02";
                else if (month_combobox.SelectedItem.ToString().Contains("03"))
                    month = "03";
                else if (month_combobox.SelectedItem.ToString().Contains("04"))
                    month = "04";
                else if (month_combobox.SelectedItem.ToString().Contains("05"))
                    month = "05";
                else if (month_combobox.SelectedItem.ToString().Contains("06"))
                    month = "06";
                else if (month_combobox.SelectedItem.ToString().Contains("07"))
                    month = "07";
                else if (month_combobox.SelectedItem.ToString().Contains("08"))
                    month = "08";
                else if (month_combobox.SelectedItem.ToString().Contains("09"))
                    month = "09";
                else if (month_combobox.SelectedItem.ToString().Contains("10"))
                    month = "10";
                else if (month_combobox.SelectedItem.ToString().Contains("11"))
                    month = "11";
                else if (month_combobox.SelectedItem.ToString().Contains("12"))
                    month = "12";
            }
        }
        //Parsing errors ----------------------------------------------------------------------
        private bool errorParser(string error, int saftnbr)
        {
            if (error.Contains("<response code=\"-1\">"))
            {
                status[saftnbr] = $"Erro no saft \"{saft_file_name[saftnbr]}\": Ocorreu um erro durante o envio do ficheiro.";
                return true;
            }
            else if (error.Contains("<response code=\"-2\">"))
            {
                status[saftnbr] = $"Erro no saft \"{saft_file_name[saftnbr]}\": O ficheiro recebido não tem o mesmo tamanho que o ficheiro enviado.";
                return true;
            }
            else if (error.Contains("<response code=\"-3\">"))
            {
                status[saftnbr] = $"Erro no saft \"{saft_file_name[saftnbr]}\": Mensagem específica da validação que não está a ser respeitada.";
                return true;
            }
            else if (error.Contains("<response code=\"-4\">"))
            {
                status[saftnbr] = $"Erro no saft \"{saft_file_name[saftnbr]}\": Ocorreu um erro durante o envio do ficheiro.";
                return true;
            }
            else if (error.Contains("<response code=\"-5\">"))
            {
                status[saftnbr] = $"Erro no saft \"{saft_file_name[saftnbr]}\": O ficheiro selecionado já foi enviado para a AT.";
                return true;
            }
            else if (error.Contains("<response code=\"-6\">"))
            {
                status[saftnbr] = $"Erro no saft \"{saft_file_name[saftnbr]}\": Erro no processo de conversão.";
                return true;
            }
            else if (error.Contains("<response code=\"-7\">"))
            {
                status[saftnbr] = $"Erro no saft \"{saft_file_name[saftnbr]}\": O cliente de linha de comandos que está a utilizar não se encontra atualizado. Por favor aceda ao portal e-fatura e obtenha a nova versão.";
                return true;
            }
            else if (error.Contains("<response code=\"-8\">"))
            {
                status[saftnbr] = $"Erro no saft \"{saft_file_name[saftnbr]}\": O ficheiro resumido não pode ser o mesmo que o ficheiro seleccionado para envio.";
                return true;
            }
            else if (error.Contains("<response code=\"-9\">"))
            {
                status[saftnbr] = $"Erro no saft \"{saft_file_name[saftnbr]}\": Para poder entregar o SAF-T na versão que indicou necessita de atualizar o cliente de linha de comandos. Para isso, por favor, aceda ao portal e-fatura e obtenha a nova versão.";
                return true;
            }
            else if (error.Contains("<response code=\"-401\">"))
            {
                status[saftnbr] = $"Erro no saft \"{saft_file_name[saftnbr]}\": Login failed for user. ERROR CODE: <ERRO ANTENTICAÇÃO>";
                return true;
            }
            else if (error.Contains("<response code=\"-666\">"))
            {
                status[saftnbr] = $"Erro no saft \"{saft_file_name[saftnbr]}\": Ocorreu um erro.";
                return true;
            }
            else if (error.Contains("Exception in thread"))
            {
                status[saftnbr] = $"Erro no saft \"{saft_file_name[saftnbr]}\": Há um problema com o seu ficheiro saft. Contacte o desenvolvedor deste programa para mais informações.";
                return true;
            }
            else if (error.Contains("<response code=\"200\">"))
            {
                status[saftnbr] = $"Saft \"{saft_file_name[saftnbr]}\" enviado com sucesso!";
                return false;
            }
            return false;
        }

        private int ArrayCount(string[] array)
        {
            int count = 0;
            foreach (var item in array) {
                if (item.Length > 0)
                    count++;
            }
            return count;
        }

    }
}
