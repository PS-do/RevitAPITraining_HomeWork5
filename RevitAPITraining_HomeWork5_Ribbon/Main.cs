using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITraining_HomeWork5_Ribbon
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalApplication
    {        
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication application)
        {
            //Для двух приложений выше создайте вкладку на ленте в Revit, панель и 2 кнопки.
            string tabName = "HomeWork5";
            application.CreateRibbonTab(tabName);
            string utilsFloberPath = @"C:\Program Files\RevitAPITrainig\";
            var panel = application.CreateRibbonPanel(tabName, "Задание 5.3");
            var button = new PushButtonData("5.1", "Task5.1",
                Path.Combine(utilsFloberPath, "RevitAPITraining_Task5_1.dll"),
                "RevitAPITraining_Task5_1.Main");
            panel.AddItem(button);
            button = new PushButtonData("5.2", "Task5.2",
                Path.Combine(utilsFloberPath, "RevitAPITraining_Task5_2.dll"),
                "RevitAPITraining_Task5_2.Main");
            panel.AddItem(button);
            return Result.Succeeded;
        }
    }
}
