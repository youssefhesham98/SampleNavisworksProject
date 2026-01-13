using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleNavisworksProject
{
    public static class FileHelper
    {
        public static string CreateRootFolder()
        {
            string path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                "Navisworks_Clash_Reports");

            Directory.CreateDirectory(path);
            return path;
        }

        public static string CreateTestFolder(string root, string testName)
        {
            string safeName = testName.Replace(" ", "_");
            string path = Path.Combine(root, safeName);

            Directory.CreateDirectory(path);
            return path;
        }
    }
}
