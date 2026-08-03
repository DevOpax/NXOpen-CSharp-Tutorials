using NXOpen;
using NXOpen.Positioning;
using System;

namespace Constraints
{
    public class Program
    {
        private static Session theSession = Session.GetSession();
        private static UI theUI = UI.GetUI();

        public static void Main(string[] args)
        {
            Part workPart = theSession.Parts.Work;
			
			ListingWindow lw = theSession.ListingWindow;
            lw.Open();

            try
            {
                ConstraintCollection constCol = workPart.ComponentAssembly.Positioner.Constraints;
				int cnt = 1;
				
				foreach (Constraint cst in constCol)
				{
					lw.WriteLine($"{cnt.ToString()} - {cst.Name}");
					lw.WriteLine($"    Type : {cst.ConstraintType.GetType().FullName}");
					lw.WriteLine($"    Status: {cst.GetConstraintStatus().ToString()}");
					lw.WriteLine($"    Is Suppressed : {cst.Suppressed.ToString()}");
					lw.WriteLine($"    Created : {cst.GetCreationDate().Day}.{cst.GetCreationDate().Month}.{cst.GetCreationDate().Year}");					
					lw.WriteLine("*********************************");
					cnt++;
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


