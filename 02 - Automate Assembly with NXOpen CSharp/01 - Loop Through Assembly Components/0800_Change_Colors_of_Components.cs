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
			
            try
            {
	
				Random rnd = new Random();
				ChangeComponentColor(root, rnd);
				
            }
            catch (Exception ex)
            {
                theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Error, ex.Message);
            }
        }
		
		public static void ChangeComponentColor(Component comp, Random rnd) 
		{
			foreach (Component cp in comp.GetChildren())
			{
				DisplayModification dm = theSession.DisplayManager.NewDisplayModification();
				dm.ApplyToOwningParts = false;
				dm.NewColor = rnd.Next(1, 217);

				DisplayableObject[] obj = new NXOpen.DisplayableObject[1];
				obj[0] = cp;
				dm.Apply(obj);
				dm.Dispose();
			
				if (cp.GetChildren().Length > 0)
				{
					ChangeComponentColor(cp,rnd);
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
