using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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
                btnGenerateBlockTypes.Enabled = true;
            }
        }

        private void CreateBlockContent_Click(object sender, EventArgs e)
        {


            var destinationPath = lblPath.Text;
            var proceed = true;

            if (string.IsNullOrEmpty(destinationPath) || destinationPath.Equals("No path selected"))
            {
                MessageBox.Show(@"Select path to create block types structure");
                proceed = false;
            }

            if (string.IsNullOrEmpty(txtBlockTypeNames.Text))
            {
                MessageBox.Show(@"There are no block types name ");
                proceed = false;
            }

            if (string.IsNullOrEmpty(txtCSTemplate.Text))
            {
                MessageBox.Show(@"The template for .cs is empty");
                proceed = false;
            }

            if (string.IsNullOrEmpty(txtCSHTMLTemplate.Text))
            {
                MessageBox.Show(@"The template for razor view is empty");
                proceed = false;
            }

            if (proceed)
            {
                var blockNames = txtBlockTypeNames.Text.Split(new[] { ',', '\n' }, StringSplitOptions.RemoveEmptyEntries).OrderBy(x => x);

                var blocksPath = Path.Combine(destinationPath, "Blocks");

                // Check and create the "Blocks" folder if it doesn't exist
                if (!Directory.Exists(blocksPath))
                {
                    Directory.CreateDirectory(blocksPath);
                }

                var order = 100;
                foreach (var blockNameRaw in blockNames)
                {
                    var blockName = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(blockNameRaw.Trim()).Replace(" ", "");
                    var blockFolderPath = Path.Combine(blocksPath, blockName);

                    if (!Directory.Exists(blockFolderPath))
                    {
                        Directory.CreateDirectory(blockFolderPath);
                    }

                    var blockCsFilePath = Path.Combine(blockFolderPath, $"{blockName}.cs");
                    var blockCshtmlFilePath = Path.Combine(blockFolderPath, $"{blockName}.cshtml");

                    var blockNameFull = CultureInfo.InvariantCulture.TextInfo.ToTitleCase(blockNameRaw.Replace("\r\n", "").Replace("\n", "")).Trim();

                    var csTemplate = ReplaceTokens(txtCSTemplate.Text, blockName, blockNameFull, Guid.NewGuid().ToString(), order);
                    var cshtmlTemplate = ReplaceTokens(txtCSHTMLTemplate.Text, blockName, blockNameFull, Guid.NewGuid().ToString(), order);

                    // Only create the .cs file if it doesn't already exist
                    if (!File.Exists(blockCsFilePath))
                    {
                        File.WriteAllText(blockCsFilePath, csTemplate);
                    }

                    // Only create the .cshtml file if it doesn't already exist
                    if (!File.Exists(blockCshtmlFilePath))
                    {
                        File.WriteAllText(blockCshtmlFilePath, cshtmlTemplate);
                    }

                    order = order + 50;
                }

                MessageBox.Show(@"SUCCESS");
            }
        }

        private string ReplaceTokens(string template, string name, string nameRaw, string guid, int order)
        {
            return template.Replace("{{BLOCKNAME}}", name.Trim())
                .Replace("{{BLOCKNAMERAW}}", nameRaw.Trim())
                .Replace("{{BLOCKNAMERAWLOWER}}", nameRaw.Trim().ToLower())
                .Replace("{{ORDER}}", order.ToString())
                .Replace("{{GUID}}", guid.Trim());
        }


    }
}
