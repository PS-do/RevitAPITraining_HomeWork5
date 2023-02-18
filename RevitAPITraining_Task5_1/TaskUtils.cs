using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITraining_Task5_1
{
    public class TaskUtils
    {
        public static void DisplayPipesCount(ExternalCommandData commandData)
        {
            //выводить окно с количество труб
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            List<Pipe> pipes = new FilteredElementCollector(doc)
               .OfCategory(BuiltInCategory.OST_PipeCurves)
               .WhereElementIsNotElementType()
               .Cast<Pipe>()
               .ToList();
            TaskDialog.Show("сообщение", $"Количество труб в моделе: {pipes.Count}");
        }


        //Объём всех стен
        public static void DisplayVolumeAllWals(ExternalCommandData commandData)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            List<Wall> walls = new FilteredElementCollector(doc)
               .OfCategory(BuiltInCategory.OST_Walls)
               .WhereElementIsNotElementType()
               .Cast<Wall>()
               .ToList();
            double VolumeAllWals = 0;
            foreach (Wall wall in walls)
            {
                VolumeAllWals += UnitUtils.ConvertFromInternalUnits(
                    wall.get_Parameter(BuiltInParameter.HOST_VOLUME_COMPUTED).AsDouble(),
                     UnitTypeId.CubicMeters);
            }
            TaskDialog.Show("сообщение", $"Объём всех стен в моделе: {VolumeAllWals:f3} м³");
        }

        //Количество дверей
        public static void DisplayDorsCount(ExternalCommandData commandData)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            List<FamilyInstance> doors = new FilteredElementCollector(doc)
               .OfCategory(BuiltInCategory.OST_Doors)
               .WhereElementIsNotElementType()
               .Cast<FamilyInstance>()
               .ToList();
            TaskDialog.Show("сообщение", $"Количество всех дверей в моделе: {doors.Count}");
        }
    }
}
