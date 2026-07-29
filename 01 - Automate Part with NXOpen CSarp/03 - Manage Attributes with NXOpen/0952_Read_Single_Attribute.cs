using NXOpen;
using System;

namespace Attributes
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
				ListingWindow Lw = theSession.ListingWindow;
				Lw.Open();
                NXObject.AttributeInformation article = WorkPart.GetUserAttribute("N°Article",NXObject.AttributeType.String, -1);
				Lw.WriteLine(article.StringValue);
				Lw.WriteLine(article.Category);
				Lw.WriteLine(article.GetType().ToString());
						
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
