using NXOpen;
using NXOpen.Assemblies;
using System;


namespace Test_arrangement
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
                string oldName= "Arrangement 1";
                string newName = "Free";

                Arrangement arr = workPart.ComponentAssembly.Arrangements.FindObject(oldName);
                if (arr != null)
                {
                    arr.SetName(newName);
                }else
                {
                    theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Information, "The " + oldName + " arrangement doesn’t exist.");
                }
                

                //theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Information, "Hello world");
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
