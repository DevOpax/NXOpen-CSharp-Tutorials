using NXOpen;
using NXOpen.Layer;
using System;

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
				
				//Get a  specific category info
				Category category = categories.FindObject("DATUMS");
				lw.WriteLine("Name : " + category.Name);
				int[] nbs = category.GetMemberLayers();
				foreach (int i in nbs)
				{
					lw.WriteLine(" -> " + i.ToString());
				}
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
