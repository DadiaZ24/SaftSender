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
        private string year;
        private string month;

        private void error_message_box(string errorstr)
        {
            MessageBox.Show(errorstr, "Ocorreu um erro!", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //Open Saft Button Code
        private void opensaft_button_Click(object sender, EventArgs e)
        {
            var saftpath_dialog = opensaft_dialog.ShowDialog();
            if (saftpath_dialog == DialogResult.OK)
            {
                saft_file_path = opensaft_dialog.FileName;
                if (Path.GetExtension(saft_file_path).Equals(".xml", StringComparison.OrdinalIgnoreCase))
                {
                    saft_file_name = Path.GetFileName(saft_file_path);
                    abrirsaft_path.Text = saft_file_name;
                }
                else
                    error_message_box("O ficheiro não é do tipo xml!");
            }
        }

        //submit button Code
        private void submit_button_Click(object sender, EventArgs e)
        {
            string nif      = nif_txtbox.Text;
            string password = pass_txtbox.Text;
            //string year     = year_txtbox.Text;
            //string month    = month_txtbox.Text;

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
                if (string.IsNullOrEmpty(saft_file_path))
                {
                    error_message_box("Não selecionou nenhum ficheiro saft!");
                    return;
                }
                if (string.IsNullOrEmpty(jar_file_path))
                {
                    error_message_box("Não efetuou o download do ficheiro JAR!");
                    return;
                }
                if (string.IsNullOrEmpty(resume_saft_path))
                {
                    error_message_box("Não selecionou um caminho para guardar o ficheiro resumido!");
                    return;
                }

                string cmd = $"-jar {jar_file_path} -n {nif} -p {password} -a {year} -m {month} -op enviar -i \"{saft_file_path}\" -md \"{resume_saft_path}\"";
            
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
                        if (error.Contains("<response code=\"-1\">"))
                            error_message_box("Ocorreu um erro durante o envio do ficheiro.");
                        else if (error.Contains("<response code=\"-2\">"))
                            error_message_box("O ficheiro recebido não tem o mesmo tamanho que o ficheiro enviado.");
                        else if (error.Contains("<response code=\"-3\">"))
                            error_message_box("Mensagem específica da validação que não está a ser respeitada.");
                        else if (error.Contains("<response code=\"-4\">"))
                            error_message_box("Ocorreu um erro durante o envio do ficheiro.");
                        else if (error.Contains("<response code=\"-5\">"))
                            error_message_box("O ficheiro selecionado já foi enviado para a AT.");
                        else if (error.Contains("<response code=\"-6\">"))
                            error_message_box("Erro no processo de conversão.");
                        else if (error.Contains("<response code=\"-7\">"))
                            error_message_box("O cliente de linha de comandos que está a utilizar não se encontra atualizado. Por favor aceda ao portal e-fatura e obtenha a nova versão.");
                        else if (error.Contains("<response code=\"-8\">"))
                            error_message_box("O ficheiro resumido não pode ser o mesmo que o ficheiro seleccionado para envio.");
                        else if (error.Contains("<response code=\"-9\">"))
                            error_message_box("Para poder entregar o SAF-T na versão que indicou necessita de atualizar o cliente de linha de comandos. Para isso, por favor, aceda ao portal e-fatura e obtenha a nova versão.");
                        else if (error.Contains("<response code=\"-401\">"))
                            error_message_box("Login failed for user. ERROR CODE: <ERRO ANTENTICAÇÃO>");
                        else if (error.Contains("<response code=\"-666\">"))
                            error_message_box("Ocorreu um erro.");
                        else if (error.Contains("<response code=\"200\">"))
                            MessageBox.Show("Succeso no envio!", "Sucesso!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else if (error.Contains("Exception in thread"))
                            error_message_box("Há um problema com o seu ficheiro saft. Contacte o desenvolvedor deste programa para mais informações.");
                    }
                    else if (!string.IsNullOrEmpty (output))
                    {
                        if (output.Contains("<response code=\"-1\">"))
                            MessageBox.Show("ERRO: Ocorreu um erro durante o envio do ficheiro.");
                        else if (output.Contains("<response code=\"-2\">"))
                            MessageBox.Show("ERRO: O ficheiro recebido não tem o mesmo tamanho que o ficheiro enviado.");
                        else if (output.Contains("<response code=\"-3\">"))
                            MessageBox.Show("ERRO: Mensagem específica da validação que não está a ser respeitada.");
                        else if (output.Contains("<response code=\"-4\">"))
                            MessageBox.Show("ERRO: Ocorreu um erro durante o envio do ficheiro.");
                        else if (output.Contains("<response code=\"-5\">"))
                            MessageBox.Show("ERRO: O ficheiro selecionado já foi enviado para a AT.");
                        else if (output.Contains("<response code=\"-6\">"))
                            MessageBox.Show("ERRO: Erro no processo de conversão.");
                        else if (output.Contains("<response code=\"-7\">"))
                            MessageBox.Show("ERRO: O cliente de linha de comandos que está a utilizar não se encontra atualizado. Por favor aceda ao portal e-fatura e obtenha a nova versão.");
                        else if (output.Contains("<response code=\"-8\">"))
                            MessageBox.Show("ERRO: O ficheiro resumido não pode ser o mesmo que o ficheiro seleccionado para envio.");
                        else if (output.Contains("<response code=\"-9\">"))
                            MessageBox.Show("ERRO: Para poder entregar o SAF-T na versão que indicou necessita de atualizar o cliente de linha de comandos. Para isso, por favor, aceda ao portal e-fatura e obtenha a nova versão.");
                        else if (output.Contains("<response code=\"-401\">"))
                            MessageBox.Show("ERRO: Login failed for user. ERROR CODE: <ERRO ANTENTICAÇÃO>");
                        else if (output.Contains("<response code=\"-666\">"))
                            MessageBox.Show("ERRO: Ocorreu um erro.");
                        else if (output.Contains("<response code=\"200\">"))
                        {
                            XDocument xdoc = XDocument.Parse(output);
                            string totalfaturas = xdoc.Root.Element("totalFaturas").Value;
                            string totalcreditos = xdoc.Root.Element("totalCreditos").Value;
                            string totaldebitos = xdoc.Root.Element("totalDebitos").Value;
                            string warning = xdoc.Root.Element("warning").Value;
                            string nomeficheiro = xdoc.Root.Element("nomeFicheiro").Value;
                            string createdate = xdoc.Root.Element("createdDate").Value;

                            if (string.IsNullOrEmpty(totalfaturas) || string.IsNullOrEmpty(totalcreditos) || string.IsNullOrEmpty(totaldebitos))
                                error_message_box("Apesar do sucesso no envio, houve um problema em obter o output esperado.");
                            else
                            {
                                if (!string.IsNullOrEmpty(warning))
                                    MessageBox.Show("Succeso no envio!\nTotal de Faturas: {totalfaturas}\nTotal de Créditos: {totalcreditos}\nTotal de Débitos: {totaldebitos}\nAtenção: {warning}\nNome do Ficheiro: {nomeficheiro}\nData de Envio: {createdate}");
                                else
                                    MessageBox.Show("Succeso no envio!\nTotal de Faturas: {totalfaturas}\nTotal de Créditos: {totalcreditos}\nTotal de Débitos: {totaldebitos}\nNome do Ficheiro: {nomeficheiro}\nData de Envio: {createdate}");
                            }
                        }
                        else if (output.Contains("Exception in thread"))
                            MessageBox.Show("ERRO: Há um problema com o seu ficheiro saft. Contacte o desenvolvedor deste programa para mais informações.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An Error ocurred!" + ex);
            }

        }

        /*private void output_return(string output, string error)
        {
            try
            {
                if (!string.IsNullOrEmpty(output)
                {
                    if (output.Contains("Exception in thread"))
                        MessageBox.Show("OLHA ELEEEE!!!");
                }
            }
        }*/
        private async void downloadjar_button_Click(object sender, EventArgs e)
        {
           using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
            {
                folderBrowserDialog.RootFolder = Environment.SpecialFolder.MyComputer;
                folderBrowserDialog.SelectedPath = @"\\srvfiscomelres\OneDrive\basededados";
                if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
                {
                    string url = "https://faturas.portaldasfinancas.gov.pt/factemipf_static/java/FACTEMICLI-2.8.4-60332-cmdClient.jar";
                    jar_file_path = Path.Combine(folderBrowserDialog.SelectedPath, "FACTEMICLI-2.8.4-60332-cmdClient.jar");

                    await DownloadJarFile(url, jar_file_path);
                    jar_path.Text = jar_file_path;
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
                    resume_path.Text = "resumido_" + saft_file_name;
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

        private void abrirsaft_path_Click(object sender, EventArgs e)
        {

        }
    }
}
