using NXOpen;
using NXOpen.Assemblies;
using System;

namespace Assembly
{
    public class Program
    {
        private static Session theSession = Session.GetSession();
        private static UI theUI = UI.GetUI();
        private static ListingWindow Lw = theSession.ListingWindow;		

        public static void Main(string[] args)
        {
            Part workPart = theSession.Parts.Work;
			Component root = workPart.ComponentAssembly.RootComponent;
			

            try
            {
				Lw.Open();
				Lw.WriteLine("Root :" + root.DisplayName);
				GetRecursiveComponent(root, 0);
            }
            catch (Exception ex)
            {
                theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Error, ex.Message);
            }
        }
		
		public static void GetRecursiveComponent(Component comp, int level)
		{
			foreach (Component cp in comp.GetChildren())
			{
				string indent = new string(' ', level * 2);
				Lw.WriteLine($"{indent}- {cp.DisplayName}");

				if (cp.GetChildren().Length > 0)
				{
					GetRecursiveComponent(cp,level + 1);
				}		 
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
