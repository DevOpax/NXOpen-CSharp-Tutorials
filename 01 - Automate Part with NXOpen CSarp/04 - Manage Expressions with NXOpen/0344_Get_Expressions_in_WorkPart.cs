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
				//Read All Expressions in a part
				ListingWindow Lw = theSession.ListingWindow;
				Lw.Open();
				
				ExpressionCollection exps = WorkPart.Expressions;

				foreach (Expression exp in exps)
				{
					Lw.WriteLine("******** " + exp.Name + " ************");
					Lw.WriteLine("Value : " + exp.Value.ToString());
					Lw.WriteLine("Tag : " + exp.Tag.ToString());
					Lw.WriteLine("Type : " + exp.Type.ToString());
					Lw.WriteLine("Units : " + exp.Units.ToString());
					Lw.WriteLine("Formula : " + exp.GetFormula());
					Lw.WriteLine("***************************************");
				} 
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
