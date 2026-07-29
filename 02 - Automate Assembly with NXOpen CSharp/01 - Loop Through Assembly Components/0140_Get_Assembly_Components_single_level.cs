using NXOpen;
using NXOpen.Assemblies;
using System;

namespace Assembly
{
    public class Program
    {
        private static Session theSession = Session.GetSession();
        private static UI theUI = UI.GetUI();       

        public static void Main(string[] args)
        {
            Part workPart = theSession.Parts.Work;
			Component root = workPart.ComponentAssembly.RootComponent;
			ListingWindow Lw = theSession.ListingWindow;

            try
            {
				Lw.Open();
				Lw.WriteLine("Root :" + root.DisplayName);

				foreach (Component cp in root.GetChildren())
				{
					Lw.WriteLine("-> " + cp.DisplayName);
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
