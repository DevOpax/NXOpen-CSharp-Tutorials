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
				NXOpen.Expression bgD2 = WorkPart.Expressions.FindObject("bgD2");
				NXOpen.Expression bgD1 = WorkPart.Expressions.FindObject("bgD1");
				
				//WorkPart.Expressions.EditExpression(bgD2, "120");
				//WorkPart.Expressions.EditExpression(bgD1, "60");
				
				WorkPart.Expressions.EditExpression(bgD2, "smD2*2");
				WorkPart.Expressions.EditExpression(bgD1, "smD1*2");

				WorkPart.Expressions.UpdateForExternalChange(); 
				
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
