using Autodesk.Navisworks.Api.Plugins;
using Autodesk.Navisworks.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Application = Autodesk.Navisworks.Api.Application;

namespace SampleNavisworksProject
{
    [Plugin("ExportGroupedClashes", "YOURCOMPANY",
    DisplayName = "Export Grouped Clash Reports",
    ToolTip = "Exports grouped clashes to HTML")]
    public class ExportGroupedClashes : AddInPlugin
    {
        public override int Execute(params string[] parameters)
        {
            var document = Application.ActiveDocument;

            if (document == null)
            {
                MessageBox.Show("No active document");
                return 0;
            }

            // 1️⃣ Read clash data
            var clashReader = new ClashReaderService();
            var testsData = clashReader.ReadGroupedClashes(document);

            // 2️⃣ Export HTML
            var htmlBuilder = new HtmlReportBuilder();
            htmlBuilder.Export(testsData);

            MessageBox.Show("Grouped clash reports exported successfully.");
            return 0;
        }
    }

    #region Old
    //[PluginAttribute("HelloWorld",
    ////Plugin name
    //"ADSK",
    ////character Developer ID or GUID
    //ToolTip = "Hello World",
    ////The tooltip for the item in the ribbon
    //DisplayName = "HelloWorld")]
    ////Display name for the Plugin in the Ribbon
    ////other kind of locations  
    ////[AddInPlugin(AddInLocation.Export)]
    ////[AddInPlugin(AddInLocation.CurrentSelectionContextMenu)]
    //public class HelloWorld : AddInPlugin
    //{
    //    public override int Execute(params string[] parameters)
    //    {
    //        MessageBox.Show("Hello World");
    //        return 0;
    //    }
    //}
    #endregion
}
