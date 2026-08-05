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

                ActivateArrangement(workPart, "Open");


                //theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Information, "Hello world");
            }
            catch (Exception ex)
            {
                theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Error, ex.Message);
            }

        }

        public static void ActivateArrangement(Part prt, string name)
        {

            Arrangement[] arrs = prt.ComponentAssembly.Arrangements.ToArray();

            foreach (Arrangement arr in arrs)
            {
                if(arr.Name == name)
                {
                    prt.ComponentAssembly.ActiveArrangement = arr;
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
