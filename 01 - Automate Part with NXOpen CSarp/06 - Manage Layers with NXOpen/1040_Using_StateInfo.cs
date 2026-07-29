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
                //Declare a layer manager
				LayerManager layers = WorkPart.Layers;
				
				//Using StaeInfo to change visibility
				StateInfo[] info = new StateInfo[2];
				info[0] = new StateInfo(21, State.Selectable);
				info[1] = new StateInfo(61, State.Visible);
				
				layers.ChangeStates(info,true);
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
