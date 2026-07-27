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
				AttributeIterator iterator = WorkPart.CreateAttributeIterator();
				iterator.SetIncludeOnlyCategory("NXOpen");
				NXObject.AttributeInformation[] attributes = WorkPart.GetUserAttributes(iterator);
				
				lw.WriteLine("***** NXOpen Attributes Only *****");
				int cnt = 1;
				foreach (NXObject.AttributeInformation attribute in attributes)
				{
					lw.WriteLine("N°" + cnt.ToString());
					lw.WriteLine("-> Title : " + attribute.Title);
					lw.WriteLine("-> Value : " + attribute.StringValue);
					lw.WriteLine("-> Type : " + attribute.Type.ToString());
					cnt++;
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
