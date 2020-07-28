using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

namespace JSONSplitter
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            fbd.RootFolder = Environment.SpecialFolder.MyComputer;
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                lblPath.Text = fbd.SelectedPath;
                btnSplit.Enabled = true;
            }

            wbMessage.DocumentText = "";
        }

        private void btnSplit_Click(object sender, EventArgs e)
        {
            wbMessage.DocumentText = "";
            var path = lblPath.Text;

            if (!string.IsNullOrEmpty(path) && !path.Equals("No path selected"))
            {
                string[] fileEntries = Directory.GetFiles(path);

                var totalJsonFiles = fileEntries.Where(x => x.EndsWith(".json"));
                if (totalJsonFiles.Any())
                {

                    var confirmationMesssage =
                        "Split action will be perform on the following " + totalJsonFiles.Count() + " JSON files." + Environment.NewLine + Environment.NewLine;
                    foreach (var filePath in fileEntries)
                    {
                        if (filePath.EndsWith(".json"))
                        {
                            var file = filePath.Replace(path, "").Replace(".json", "").Replace("\\", "")
                                .Replace("/", "");

                            confirmationMesssage = confirmationMesssage + "- " + file + ".json" + Environment.NewLine;
                        }
                    }

                    MessageBox.Show(confirmationMesssage);
                    var message = "";
                    foreach (var filePath in fileEntries)
                    {
                        if (filePath.EndsWith(".json"))
                        {
                            var file = filePath.Replace(path, "").Replace(".json", "").Replace("\\", "")
                                .Replace("/", "");

                            message = message + SplitJsonFile(file, path);
                        }
                    }

                    wbMessage.DocumentText = message;

                }
                else
                {
                    MessageBox.Show("No JSON file(s) in the path " + path);
                }
            }
            else
            {
                MessageBox.Show("Select path to JSON file(s)");
            }
        }

        private string SplitJsonFile(string file, string path)
        {
            try
            {
                using (StreamReader r = new StreamReader(path + "\\" + file + ".json"))
                {
                    var folderName = path + "\\" + file;

                    if (Directory.Exists(folderName))
                    {
                        var dir = new DirectoryInfo(folderName);
                        foreach (var f in dir.GetFiles())
                        {
                            f.Delete();
                        }
                    }
                    else
                        Directory.CreateDirectory(folderName);

                    var json = r.ReadToEnd();

                    dynamic stuff = JsonConvert.DeserializeObject(json);
                    var count = 1;
                    foreach (var s in stuff)
                    {
                        string fileName = file + count + ".json";
                        var filePath = System.IO.Path.Combine(folderName, fileName);
                        WriteToJsonFile(filePath, s, false);
                        count++;

                    }

                    return "<strong>" + (count - 1) +
                              "</strong> JSON files have been split from <strong>" + file +
                              ".json</strong> file in the path: <strong>" + folderName + "</strong> </br> </br>";

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return "";
        }

        private void WriteToJsonFile(string filePath, dynamic objectToWrite, bool append = false)
        {
            TextWriter writer = null;
            try
            {
                writer = new StreamWriter(filePath, append);
                writer.Write(objectToWrite);
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
        }
    }
}
