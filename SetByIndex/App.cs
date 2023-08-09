using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using System.Reflection;
using Utilities;
using System.IO;

namespace SetByIndex
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    [Autodesk.Revit.DB.Macros.AddInId("00A4C164-52CE-437F-8E11-2C6FA63A0334")]
    public class ThisApplication : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication uiApp)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication uiApp)
        {

            try
            {
                #region GAS ADDIN BOILERPLATE
                // Assembly that contains the invoke method
                string exeConfigPath = Utils.GetExeConfigPath("SetByIndex.dll");

                // Finds and creates the tab, finds and creates the panel
                RibbonPanel DefaultPanel = Utils.GetRevitPanel(uiApp, GlobalVars.PANEL_NAME, GlobalVars.TAB_NAME);
                #endregion

                // Button configuration
                string SetByIndexName = "Set by index";
                PushButtonData SetByIndexData = new PushButtonData(SetByIndexName, SetByIndexName, exeConfigPath, "SetByIndex.ThisCommand");
                SetByIndexData.LargeImage = Utils.RetriveImage("SetByIndex.Resources.SetByIndex32x32.ico", Assembly.GetExecutingAssembly()); // Pushbutton image
                SetByIndexData.ToolTip = "Create printing sheet sets from sheet schedules";
                RibbonItem CadDetectiveButton = DefaultPanel.AddItem(SetByIndexData); // Add pushbutton

                return Result.Succeeded;
            }


            catch (Exception ex)
            {
                Utils.CatchDialog(ex);
                return Result.Failed;
            }
        }
    }
}
