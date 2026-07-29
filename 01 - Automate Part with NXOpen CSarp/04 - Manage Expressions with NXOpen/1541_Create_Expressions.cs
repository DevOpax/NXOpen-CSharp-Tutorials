using NXOpen;
using System;


namespace Expressions
{
    public class Exercice
    {
        private static Session theSession = Session.GetSession();
        private static UI theUI = UI.GetUI();
        //private static UFSession theUfSession = UFSession.GetUFSession();


        public static void Main(string[] args)
        {
            Part WorkPart = theSession.Parts.Work;

            try
            {
				//Get List of default unit
				// ListingWindow Lw = theSession.ListingWindow;
				// Lw.Open();
				// UnitCollection units = WorkPart.UnitCollection;
				// foreach (Unit unit in units)
				// {
					// if(unit.IsBaseUnit)
					// {                 
						// Lw.WriteLine("Unit Base : " + unit.Measure + ", unit : " + unit.Symbol);
					// }
				// }

				//Create New Expression for area in mm2
				NXOpen.Unit unit = WorkPart.UnitCollection.GetBase("Area");
				WorkPart.Expressions.NewExpression("Number", "area2 = Length * 	Width", unit, false, false);

				
            }
            catch (Exception ex)
            {
                theUI.NXMessageBox.Show("NX Open Tuto", NXMessageBox.DialogType.Error, ex.Message);
            }
        }


        public static int GetUnloadOption(string arg)
        {
            //Unloads the image explicitly, via an unload dialog
            //return System.Convert.ToInt32(Session.LibraryUnloadOption.Explicitly);

            //Unloads the image immediately after execution within NX
            return System.Convert.ToInt32(Session.LibraryUnloadOption.Immediately);

            //Unloads the image when the NX session terminates
            // return System.Convert.ToInt32(Session.LibraryUnloadOption.AtTermination);
        }

    }
}
