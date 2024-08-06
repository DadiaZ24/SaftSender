using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Management.Automation;
using System.Collections.ObjectModel;

namespace MyWinFormsApp
{
    public partial class Form1 : Form
    {
        private TextBox txtJarPath;
        private TextBox txtNIF;
        private TextBox txtPassword;
        private TextBox txtYear;
        private TextBox txtMonth;
        private TextBox txtSendFilePath;
        private Button btnSubmit;
        private Button btnDownload;

        public Form1()
        {
            InitializeComponent();
        }

        private async void btnDownload_Click(object sender, EventArgs e)
        {
            string url = "http://example.com/path/to/your/jarfile.jar"; // Replace with your JAR file URL
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string jarFilePath = Path.Combine(desktopPath, "FACTEMICLI-latest-cmdClient.jar");

            await DownloadFileAsync(url, jarFilePath);

            txtJarPath.Text = jarFilePath;
            MessageBox.Show("JAR file downloaded successfully!");
        }

        private async Task DownloadFileAsync(string url, string filePath)
        {
            using (HttpClient client = new HttpClient())
            {
                using (HttpResponseMessage response = await client.GetAsync(url))
                {
                    response.EnsureSuccessStatusCode();
                    using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
                    {
                        await response.Content.CopyToAsync(fs);
                    }
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string jarPath = txtJarPath.Text;
            string nif = txtNIF.Text;
            string password = txtPassword.Text;
            string year = txtYear.Text;
            string month = txtMonth.Text;
            string sendFilePath = txtSendFilePath.Text;

            string command = $"java -jar \"{jarPath}\" -n {nif} -p {password} -a {year} -m {month} -op enviar -i \"{sendFilePath}\"";

            string result = ExecutePowerShellCommand(command);
            MessageBox.Show(result);
        }

        private string ExecutePowerShellCommand(string command)
        {
            using (PowerShell ps = PowerShell.Create())
            {
                ps.AddScript(command);
                Collection<PSObject> results = ps.Invoke();

                if (ps.Streams.Error.Count > 0)
                {
                    // Capture and return errors
                    var errors = string.Join(Environment.NewLine, ps.Streams.Error);
                    return "Error: " + errors;
                }
                else
                {
                    // Capture and return output
                    var output = string.Join(Environment.NewLine, results);
                    return "Success: " + output;
                }
            }
        }

        private void InitializeComponent()
        {
            this.txtJarPath = new System.Windows.Forms.TextBox();
            this.txtNIF = new System.Windows.Forms.TextBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.txtYear = new System.Windows.Forms.TextBox();
            this.txtMonth = new System.Windows.Forms.TextBox();
            this.txtSendFilePath = new System.Windows.Forms.TextBox();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.btnDownload = new System.Windows.Forms.Button();
            this.SuspendLayout();

            // txtJarPath
            this.txtJarPath.Location = new System.Drawing.Point(150, 20);
            this.txtJarPath.Name = "txtJarPath";
            this.txtJarPath.Size = new System.Drawing.Size(200, 20);
            this.txtJarPath.TabIndex = 0;

            // txtNIF
            this.txtNIF.Location = new System.Drawing.Point(150, 50);
            this.txtNIF.Name = "txtNIF";
            this.txtNIF.Size = new System.Drawing.Size(200, 20);
            this.txtNIF.TabIndex = 1;

            // txtPassword
            this.txtPassword.Location = new System.Drawing.Point(150, 80);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(200, 20);
            this.txtPassword.TabIndex = 2;

            // txtYear
            this.txtYear.Location = new System.Drawing.Point(150, 110);
            this.txtYear.Name = "txtYear";
            this.txtYear.Size = new System.Drawing.Size(200, 20);
            this.txtYear.TabIndex = 3;

            // txtMonth
           
