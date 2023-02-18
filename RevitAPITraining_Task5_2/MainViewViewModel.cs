using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITraining_Task5_2
{
    public class MainViewViewModel
    {
        private ExternalCommandData _commandData;

        public DelegateCommand SetTypeWallCommand { get; set; }
        public List<Wall> PickedWalls { get; set; }
        public List<WallType> WallTypes { get; set; }
        public WallType SelectedWallType { get;set; }

        public event EventHandler CloseRequest;
        private void RaiseCloseRequest()
        {
            CloseRequest?.Invoke(this, EventArgs.Empty);
        }

        public MainViewViewModel(ExternalCommandData commandData)
        {
            _commandData = commandData;
            SetTypeWallCommand = new DelegateCommand(OnSetTypeWallCommand);
            PickedWalls = WallUtils.PickWalls(commandData);
            //TaskDialog.Show("Сообщение", $"Выбрано стен:{PickedWalls.Count} шт.") ;
            WallTypes = WallUtils.GetWallTypes(commandData);
            //TaskDialog.Show("Сообщение", $"WallTypes[0].Name:{WallTypes[0].Name} ");
        }

        private void OnSetTypeWallCommand()
        {
            UIApplication uiapp = _commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            if (PickedWalls.Count == 0 || SelectedWallType == null)
                return;


            using (Transaction ts = new Transaction(doc, "Set type walls"))
            {
                ts.Start();
                foreach (var wall in PickedWalls)
                {
                    if (wall is Wall)
                    {
                        wall.WallType=SelectedWallType;
                    }
                }
                ts.Commit();
            }
            RaiseCloseRequest();
        }


    }
}
