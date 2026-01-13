using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleNavisworksProject
{
    public class HtmlReportBuilder
    {
        public void Export(List<ClashTestData> tests)
        {
            string root = FileHelper.CreateRootFolder();

            foreach (var test in tests)
            {
                string testFolder = FileHelper.CreateTestFolder(root, test.TestName);

                foreach (var group in test.Groups)
                {
                    string html = BuildGroupHtml(test.TestName, group);
                    string filePath = Path.Combine(
                        testFolder,
                        $"{group.GroupName}.html");

                    File.WriteAllText(filePath, html);
                }
            }
        }

        private string BuildGroupHtml(string testName, ClashGroupData group)
        {
            var sb = new StringBuilder();

            sb.AppendLine("<html><head>");
            sb.AppendLine("<title>Clash Group Report</title>");
            sb.AppendLine("</head><body>");

            sb.AppendLine($"<h1>{testName}</h1>");
            sb.AppendLine($"<h2>Group: {group.GroupName}</h2>");

            sb.AppendLine("<table border='1'>");
            sb.AppendLine("<tr><th>Clash</th><th>Item 1</th><th>Item 2</th><th>Status</th></tr>");

            foreach (var clash in group.Clashes)
            {
                sb.AppendLine("<tr>");
                sb.AppendLine($"<td>{clash.ClashName}</td>");
                sb.AppendLine($"<td>{clash.Item1}</td>");
                sb.AppendLine($"<td>{clash.Item2}</td>");
                sb.AppendLine($"<td>{clash.Status}</td>");
                sb.AppendLine("</tr>");
            }

            sb.AppendLine("</table>");
            sb.AppendLine("</body></html>");

            return sb.ToString();
        }
    }
}
