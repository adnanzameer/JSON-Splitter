using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;

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
            //var path = lblPath.Text;
            var path = @"C:\Projects\KnightFrank.WebPlatform\src\KFWT.WebPlatform.Web\Features";

            if (!string.IsNullOrEmpty(path) && !path.Equals("No path selected"))
            {
                ProcessFiles(path);
            }
            else
            {
                MessageBox.Show(@"Select path to Content Type file(s)");
            }
        }

        public static void ProcessFiles(string rootPath)
        {
            var allCsFiles = Directory.GetFiles(rootPath, "*.cs", SearchOption.AllDirectories);
            var contentTypeElements = new List<XElement>();
            var sharedProperties = new List<XElement>(); // For abstract/shared base properties

            foreach (var filePath in allCsFiles)
            {
                var fileContent = File.ReadAllText(filePath);

                // Detect abstract class
                if (Regex.IsMatch(fileContent, @"public\s+abstract\s+class\s+"))
                {
                    var sharedProps = ExtractDisplayProperties(fileContent);
                    sharedProperties.AddRange(sharedProps);
                    continue;
                }

                // Skip if it's not a content type
                if (!fileContent.Contains("[SiteContentType") && !fileContent.Contains("[ContentType"))
                    continue;

                var className = ExtractClassName(fileContent);
                var (displayName, description) = ExtractContentTypeAttributes(fileContent);
                var properties = ExtractDisplayProperties(fileContent);

                if (string.IsNullOrWhiteSpace(className))
                    continue;

                var typeElement = new XElement(className.ToLowerInvariant(),
                    new XElement("description", description ?? string.Empty),
                    new XElement("name", displayName ?? className),
                    new XElement("properties", properties.OrderBy(e => e.Name.LocalName))
                );

                contentTypeElements.Add(typeElement);
            }

            // Separate <icontentdata> and other types
            var icontentData = new XElement("icontentdata",
                new XElement("properties", sharedProperties.OrderBy(e => e.Name.LocalName))
            );

            // Sort all other content types by element name
            var sortedContentTypes = contentTypeElements
                .OrderBy(e => e.Name.LocalName)
                .ToList();

            // Final <contenttypes> element
            var contentTypesElement = new XElement("contenttypes",
                icontentData,           // Always first
                sortedContentTypes      // Sorted after
            );

            // Build final XML document
            var doc = new XDocument(
                new XDeclaration("1.0", "utf-8", null),
                new XElement("languages",
                    new XElement("language", new XAttribute("name", "English"), new XAttribute("id", "en"),
                        contentTypesElement
                    )
                )
            );

            // Save to ContentTypes.xml
            var outputPath = Path.Combine(rootPath, "ContentTypes.xml");
            doc.Save(outputPath);
        }
        private static string ExtractClassName(string content)
        {
            var match = Regex.Match(content, @"public\s+class\s+(\w+)");
            return match.Success ? match.Groups[1].Value : null;
        }

        private static (string displayName, string description) ExtractContentTypeAttributes(string content)
        {
            var match = Regex.Match(content, @"\[SiteContentType\((.*?)\)\]|\[ContentType\((.*?)\)\]", RegexOptions.Singleline);
            if (!match.Success)
                return (null, null);

            var args = match.Groups[1].Success ? match.Groups[1].Value : match.Groups[2].Value;
            var name = ExtractNamedArgument(args, "DisplayName");
            var desc = ExtractNamedArgument(args, "Description");

            return (name, desc);
        }

        private static IEnumerable<XElement> ExtractDisplayProperties(string content)
        {
            var props = new List<XElement>();

            // Match all public virtual properties and capture their name
            var propertyRegex = new Regex(@"(?<attributes>(\[[^\]]+\]\s*)+)\s*public\s+virtual\s+[^\s]+\s+(?<name>\w+)\s*\{", RegexOptions.Multiline);
            var matches = propertyRegex.Matches(content);

            foreach (Match match in matches)
            {
                var attributesBlock = match.Groups["attributes"].Value;
                var propName = match.Groups["name"].Value;

                // Find Display(Name = "...", Description = "...")
                var displayMatch = Regex.Match(attributesBlock, @"\[Display\s*\((.*?)\)\]", RegexOptions.Singleline);
                if (!displayMatch.Success) continue;

                var args = displayMatch.Groups[1].Value;
                var rawCaption = ExtractNamedArgument(args, "Name") ?? propName;
                var caption = ToSentenceCaseKeepAllCaps(rawCaption);

                var help = ExtractNamedArgument(args, "Description");

                var element = new XElement(propName.ToLowerInvariant(),
                    new XElement("caption", caption)
                );

                if (!string.IsNullOrWhiteSpace(help))
                    element.Add(new XElement("help", help));

                props.Add(element);
            }

            return props;
        }

        private static string ExtractNamedArgument(string args, string key)
        {
            var match = Regex.Match(args, $@"{key}\s*=\s*""([^""]+)""");
            return match.Success ? match.Groups[1].Value : null;
        }

        public static string ToSentenceCaseKeepAllCaps(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return input;

            var textInfo = CultureInfo.CurrentCulture.TextInfo;
            var result = new StringBuilder(input.Length);
            bool newSentence = true;

            for (int i = 0; i < input.Length;)
            {
                if (newSentence && char.IsLetter(input[i]))
                {
                    // Check if this is an ALL-CAPS word (like "RV", "NASA")
                    if (IsAllCapsWord(input, i, out int wordLength))
                    {
                        // Keep the ALL-CAPS word as-is
                        result.Append(input.Substring(i, wordLength));
                        i += wordLength;
                        newSentence = false;
                        continue;
                    }
                    else
                    {
                        // Normal sentence case - capitalize first letter
                        result.Append(char.ToUpper(input[i]));
                        i++;
                        newSentence = false;
                        continue;
                    }
                }

                // Handle non-first letters
                if (IsAllCapsWord(input, i, out int capsLength))
                {
                    result.Append(input.Substring(i, capsLength));
                    i += capsLength;
                }
                else
                {
                    result.Append(char.ToLower(input[i]));
                    i++;
                }

                // Detect sentence endings
                if (i > 0 && (input[i - 1] == '.' || input[i - 1] == '!' || input[i - 1] == '?'))
                {
                    newSentence = true;
                }
            }

            return result.ToString();
        }

        private static bool IsAllCapsWord(string input, int startIndex, out int length)
        {
            length = 0;
            int end = startIndex;

            // Find the whole word (letters only)
            while (end < input.Length && char.IsLetter(input[end]))
            {
                end++;
            }

            length = end - startIndex;
            if (length < 2) return false; // Single letters aren't considered ALL-CAPS

            // Verify ALL letters are uppercase
            for (int i = startIndex; i < end; i++)
            {
                if (char.IsLetter(input[i]) && !char.IsUpper(input[i]))
                    return false;
            }

            return true;
        }
    }
}
