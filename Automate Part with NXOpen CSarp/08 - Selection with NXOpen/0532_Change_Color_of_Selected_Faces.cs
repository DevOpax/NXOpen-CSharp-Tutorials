using NXOpen;
using NXOpen.Features;
using System;
using System.Linq;

namespace Features
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
				//Get currently selected Objects
				NXOpen.Selection selection = theUI.SelectionManager;
				int cnt = selection.GetNumSelectedObjects();
				if (cnt > 0)
				{
					ListingWindow lw = theSession.ListingWindow;
					lw.Open();
					lw.WriteLine("You have " + cnt.ToString() + " object(s) selected.");
					
					for (int i = 0; i< cnt; i++)
					{
						TaggedObject tob = selection.GetSelectedTaggedObject(i);
						if (tob.GetType() == typeof(NXOpen.Face))
						{
							NXOpen.Face face = (NXOpen.Face)tob;
							int oldColor = face.Color;
							int newColor = 145;
							face.Color = newColor;
							face.RedisplayObject();
							lw.WriteLine("Face color " + oldColor.ToString() + " changed to " + newColor.ToString());
						}
					}
					lw.Close();
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
