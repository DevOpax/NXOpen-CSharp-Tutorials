using NXOpen;
using NXOpen.Assemblies;
using NXOpen.Positioning;
using System;

namespace Test_arrangement
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

                TouchAlignConstraint(workPart, "Close");


                //theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Information, "Hello world");
            }
            catch (Exception ex)
            {
                theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Error, ex.Message);
            }

        }

        public static void TouchAlignConstraint(Part prt, string name)
        {
            Session.UndoMarkId markId = theSession.SetUndoMark(Session.MarkVisibility.Visible, "add constraint");

            Selection selMgr = theUI.SelectionManager;
            int cnt = selMgr.GetNumSelectedObjects();

            if (cnt == 2)
            {
                //Get geometry seletced
                TaggedObject tag1 = selMgr.GetSelectedTaggedObject(0);
                TaggedObject tag2 = selMgr.GetSelectedTaggedObject(1);
                NXObject obj1 = tag1 as NXObject;
                NXObject obj2 = tag2 as NXObject;

                //Get component from geometry
                Component comp1 = obj1.OwningComponent;
                Component comp2 = obj2.OwningComponent;

                if (comp1 != comp2)
                {
                    //Initialize constraint
                    ComponentPositioner compPos = prt.ComponentAssembly.Positioner;
                    compPos.ClearNetwork();
                    compPos.BeginAssemblyConstraints();

                    //Get Arrangement
                    Arrangement arrangement = prt.ComponentAssembly.Arrangements.FindObject(name);
                    compPos.PrimaryArrangement = arrangement;

                    ComponentConstraint constr = (ComponentConstraint)compPos.CreateConstraint(true);
   
                    constr.ConstraintType = Constraint.Type.Touch;
                    constr.ConstraintAlignment = Constraint.Alignment.ContraAlign;

                    //Create references for the constraint
                    ConstraintReference constRef1 = constr.CreateConstraintReference(comp1, obj1, false, false);
                    ConstraintReference constRef2 = constr.CreateConstraintReference(comp2, obj2, false, false);

                    //Specify specific arrangement
                    constr.SetSpecificInArrangement(arrangement, true);
                    constr.SetSharedSuppressed(true);

                    theSession.UpdateManager.DoUpdate(markId);
                    compPos.EndAssemblyConstraints();
                }
                else
                {
                    theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Information, "Geometries must be on different components.");
                }

            }
            else
            {

                theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Information, "Select 2 geometries please.");
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
