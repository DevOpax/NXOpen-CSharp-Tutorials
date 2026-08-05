using NXOpen;
using NXOpen.Assemblies;
using NXOpen.Positioning;
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

                SuppressCompInArrangement(workPart, "Free", "Screw");


                //theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Information, "Hello world");
            }
            catch (Exception ex)
            {
                theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Error, ex.Message);
            }

        }

        public static void SuppressCompInArrangement(Part prt, string arrrName, string compName)
        {

            Session.UndoMarkId markId = theSession.SetUndoMark(Session.MarkVisibility.Visible, "suppress component");

            Component[] comps = new Component[1];
            Arrangement[] arrs = new Arrangement[1];

            arrs[0] = prt.ComponentAssembly.Arrangements.FindObject(arrrName);

            foreach (Component comp in prt.ComponentAssembly.RootComponent.GetChildren()) 
            {
                if(comp.DisplayName == compName)
                {
                    comps[0] = comp;
                }
            }

            prt.ComponentAssembly.SuppressComponents(comps, arrs);

            theSession.UpdateManager.DoUpdate(markId);

        }

        public static int GetUnloadOption(string arg)
        {
            return System.Convert.ToInt32(Session.LibraryUnloadOption.Immediately);
            //return System.Convert.ToInt32(Session.LibraryUnloadOption.Explicitly);
            //return System.Convert.ToInt32(Session.LibraryUnloadOption.AtTermination);

        }
    }
}
