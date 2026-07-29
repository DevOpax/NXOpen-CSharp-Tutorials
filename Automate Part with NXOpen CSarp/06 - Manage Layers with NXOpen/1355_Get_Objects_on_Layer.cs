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
                
				//Get all object in a layer
				LayerManager layManager = workPart.Layers;
				NXObject[] objects = layManager.GetAllObjectsOnLayer(61);
				foreach(NXObject obj in objects)
				{
					lw.WriteLine("Object Type : " + obj.GetType().Name);
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
