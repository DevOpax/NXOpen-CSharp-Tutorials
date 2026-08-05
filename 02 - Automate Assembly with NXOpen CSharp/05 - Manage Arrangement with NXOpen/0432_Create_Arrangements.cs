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

                CreateArrangement(workPart, "Close");
                CreateArrangement(workPart, "Open");
                CreateArrangement(workPart, "Test");


                //theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Information, "Hello world");
            }
            catch (Exception ex)
            {
                theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Error, ex.Message);
            }

        }

        public static void CreateArrangement(Part prt, string name)
        {
            Session.UndoMarkId markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Create " + name + " Assembly Arrangements");

            Arrangement tplArr = prt.ComponentAssembly.RootComponent.UsedArrangement;

            if (tplArr != null)
            {
                Arrangement arr = prt.ComponentAssembly.Arrangements.Create(tplArr, name);
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
