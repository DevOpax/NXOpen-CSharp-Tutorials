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

                DeleteArrangement(workPart, "Test");

                //theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Information, "Hello world");
            }
            catch (Exception ex)
            {
                theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Error, ex.Message);
            }

        }

        private static void DeleteArrangement(Part prt, string name)
        {
            Session.UndoMarkId markId = theSession.SetUndoMark(Session.MarkVisibility.Visible, "Delete arrangement");

            Arrangement curArr = prt.ComponentAssembly.RootComponent.UsedArrangement;
            Arrangement delArr = prt.ComponentAssembly.Arrangements.FindObject(name);

            if (curArr != delArr)
            {
                delArr.Delete(true);
            }
            else
            {
                theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Information, "The " + name + " arrangement is active, you can not delete it.");
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
