using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITraining_Task5_2
{
    public class WallFilter : ISelectionFilter
    {
        public bool AllowElement(Element elem)
        {
            return elem is Wall;
        }

        public bool AllowReference(Reference reference, XYZ position)
        {
            return false;
        }
    }



    internal class WallUtils
    {
        public static List<WallType> GetWallTypes(ExternalCommandData commandData)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            List<WallType> wallTypes = new FilteredElementCollector(doc)
                                                            .OfClass(typeof(WallType))
                                                            .Cast<WallType>()
                                                            .ToList();
            return wallTypes;
        }

        public static List<Wall> PickWalls(ExternalCommandData commandData, string message = "Выберите стены")
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;

            IList<Reference> selectedObjects = uidoc.Selection.PickObjects(
                ObjectType.Element, 
                new WallFilter(),
                message);
            List<Element> elementList = selectedObjects.Select(sObj => doc.GetElement(sObj)).ToList();
            List<Wall> walls = new List<Wall>();
            foreach (Element element in elementList)
            {
                walls.Add(element as Wall);
            }
            return walls;
        }
    }
}
