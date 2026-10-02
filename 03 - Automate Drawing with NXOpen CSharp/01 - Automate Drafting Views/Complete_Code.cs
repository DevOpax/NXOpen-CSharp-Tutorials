using NXOpen;
using NXOpen.Drawings;
using NXOpenUI;
using System;

namespace Insert_views
{
    public class Program
    {
        private static Session theSession = Session.GetSession();
        private static UI theUI = UI.GetUI();

        public static void Main(string[] args)
        {
            Part workPart = theSession.Parts.Work;

            try
            {
                //Get active sheet
                DrawingSheet currSheet = workPart.DrawingSheets.CurrentDrawingSheet;
                if (currSheet == null)
                {
                    theUI.NXMessageBox.Show("NXOpen", NXMessageBox.DialogType.Information, "No active Sheet.");
                    return;
                }

                //Prompt fo view name
                string name = NXInputBox.GetInputString("Which base view do you want.", "Base View");
                if (string.IsNullOrEmpty(name))
                {
                    theUI.NXMessageBox.Show("NXOpen", NXMessageBox.DialogType.Information, "Name can not be empty.");
                    return;
                }

                //Check if view exist
                ModelingView modelingView = GetModelingView(workPart, name);
                if (modelingView == null)
                {
                    theUI.NXMessageBox.Show("NXOpen", NXMessageBox.DialogType.Information, "The view " + name + " do not exist.");
                    return;
                }

                double h1 = currSheet.Height / 4;
                double l1 = currSheet.Length / 4;

                //Specific A4 Landscape format
                if(currSheet.Height < 297)
                {
                    h1 = currSheet.Height / 5;
                }

                //Base View
                Point3d ptBase = new Point3d(l1, h1 * 3, 0.0);
                string baseViewName = CreateBaseView(workPart, modelingView, ptBase);

                //Right view
                Point3d pRight = new Point3d(l1 * 3, h1 * 3, 0.0);
                CreateProjectedView(workPart, baseViewName, ViewPlacementBuilder.Method.Horizontal, pRight);

                if (currSheet.Height >= 297)
                {
                    //Top view
                    Point3d pTop = new Point3d(l1, h1, 0.0);
                    CreateProjectedView(workPart, baseViewName, ViewPlacementBuilder.Method.Vertical, pTop);

                    //Isometric view
                    Point3d pIso = new Point3d(l1 * 3, h1 + (h1 / 2), 0.0);
                    ModelingView modelingViewIso = GetModelingView(workPart, "Isometric");
                    CreateBaseView(workPart, modelingViewIso, pIso, true);
                }
           
            }
            catch (Exception ex)
            {
                theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Error, ex.Message);
            }

        }

        private static void CreateProjectedView(Part prt, string baseName, ViewPlacementBuilder.Method method, Point3d point)
        {
            NXOpen.Session.UndoMarkId markId = theSession.SetUndoMark(Session.MarkVisibility.Visible, "Create projected view.");

            ProjectedViewBuilder pvBuilder = prt.DraftingViews.CreateProjectedViewBuilder(null);
            pvBuilder.Placement.Associative = true;
            pvBuilder.Placement.AlignmentMethod = method;
            pvBuilder.Placement.AlignmentVector = null;
            pvBuilder.Placement.AlignmentOption = ViewPlacementBuilder.Option.ToView;

            BaseView baseView =((BaseView)prt.DraftingViews.FindObject(baseName));
            pvBuilder.Parent.View.Value = baseView;
            pvBuilder.Style.ViewStyleBase.Part = prt;
            pvBuilder.Placement.AlignmentView.Value = baseView;
            pvBuilder.Placement.Placement.SetValue(baseView, prt.Views.WorkView, point);

            pvBuilder.Commit();
            pvBuilder.Destroy();

        }

        private static string CreateBaseView(Part prt, ModelingView modelingView, Point3d point3D, bool shaded = false)
        {
            NXOpen.Session.UndoMarkId markId = theSession.SetUndoMark(Session.MarkVisibility.Visible, "Create " + modelingView.Name + " view");

            BaseViewBuilder bvBuilder = prt.DraftingViews.CreateBaseViewBuilder(null);
            bvBuilder.Placement.Associative = true;
            bvBuilder.SelectModelView.SelectedView = modelingView;
            bvBuilder.Style.ViewStyleBase.Part = prt;

            if (shaded)
            {
                bvBuilder.Style.ViewStyleShading.RenderingStyle = NXOpen.Preferences.ShadingRenderingStyleOption.FullyShaded;
                bvBuilder.Style.ViewStyleHiddenLines.HiddenLine = true;
                bvBuilder.Style.ViewStyleHiddenLines.Font = NXOpen.Preferences.Font.Invisible;
            }

            bvBuilder.Placement.Placement.SetValue(null, prt.Views.WorkView, point3D);

            NXObject obj = bvBuilder.Commit();
            bvBuilder.Destroy();

            return obj.Name;


        }

        private static ModelingView GetModelingView(Part prt, string viewname)
        {
            ModelingView[] views = prt.ModelingViews.ToArray();

            foreach (ModelingView view in views)
            {
                if(view.Name == viewname)
                {
                    return view;
                }
            }
            return null;
        }

        public static int GetUnloadOption(string arg)
        {
            return System.Convert.ToInt32(Session.LibraryUnloadOption.Immediately);
            //return System.Convert.ToInt32(Session.LibraryUnloadOption.Explicitly);
            //return System.Convert.ToInt32(Session.LibraryUnloadOption.AtTermination);

        }
    }
}
