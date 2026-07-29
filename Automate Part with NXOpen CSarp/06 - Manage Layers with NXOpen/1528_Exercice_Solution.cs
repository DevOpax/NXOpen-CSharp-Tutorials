using NXOpen;
using NXOpen.Layer;
using System;
using System.Linq;

namespace Layers
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

                ListingWindow lw = theSession.ListingWindow;
                lw.Open();

                lw.WriteLine(" ************ INFORMATION *********");

                Session.UndoMarkId undoMoveToLayer = theSession.SetUndoMark(Session.MarkVisibility.Visible, "MoveToLayer");
                //Code to move objects on layers

                LayerManager layManager = workPart.Layers;

                Body[] bodies = workPart.Bodies.ToArray();
                if (bodies != null && bodies.Length > 0)
                {
                    Body[] solidBodies = bodies.Where(x => x.IsSolidBody).ToArray();
                    Body[] sheetBodies = bodies.Where(x => x.IsSheetBody).ToArray();

                    if (solidBodies.Length > 0)
                    {
                        layManager.MoveDisplayableObjects(1, solidBodies);
                        lw.WriteLine(solidBodies.Length.ToString() + " solid body(ies) moved on layer 1");
                    }
                    
                    if(sheetBodies.Length > 0)
                    {
                        layManager.MoveDisplayableObjects(11, sheetBodies);
                        lw.WriteLine(sheetBodies.Length.ToString() + " sheet body(ies) moved on layer 11");
                    }

                }
                else
                {
                    lw.WriteLine("No body to move");
                }

                //Move sketches on layer 21
                Sketch[] sketches = workPart.Sketches.ToArray();
                if (sketches != null && sketches.Length > 0)
                {
                    layManager.MoveDisplayableObjects(21, sketches);
                    lw.WriteLine(sketches.Length.ToString() + " sketch(es) moved on layer 21");
                }
                else
                {
                    lw.WriteLine("No sketch to move.");
                }


                Session.UndoMarkId undoHideLayer = theSession.SetUndoMark(Session.MarkVisibility.Visible, "HideLayer");
                //Code to Hide Layers

                Category[] categories = workPart.LayerCategories.ToArray();
                foreach (Category category in categories)
                {
                    if (category.Name == "ALL")
                    {
                        category.SetState(NXOpen.Layer.State.Hidden);
                        lw.WriteLine("The category " + category.Name + " has been hidden.");
                    }
                }

                Category solids = workPart.LayerCategories.FindObject("SOLIDS");
                solids.SetState(NXOpen.Layer.State.Selectable);
                lw.WriteLine("The category " + solids.Name + " is selectable");

                layManager.WorkLayer = 1;
                lw.WriteLine("Work layer set to 1");

            }
            catch (Exception ex)
            {
                theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Error, ex.Message);
            }

        }

        public static int GetUnloadOption(string arg)
        {
            return System.Convert.ToInt32(Session.LibraryUnloadOption.Immediately);

            //return System.Convert.ToInt32(Session.LibraryUnloadOption.Explicitly);

            //return System.Convert.ToInt32(Session.LibraryUnloadOption.AtTermination);

        }

    }
}

