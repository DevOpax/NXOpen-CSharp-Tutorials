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
				NXOpen.Selection selection = theUI.SelectionManager;
				
				TaggedObject[] tagObjs = null;
				
				NXOpen.Selection.SelectionType[] selectionType =
				{
					NXOpen.Selection.SelectionType.Faces
				};
				
				NXOpen.Selection.Response resp = selection.SelectTaggedObjects("Select something", "Selection", 
					NXOpen.Selection.SelectionScope.AnyInAssembly, false, selectionType, out tagObjs);
					
				if (resp == NXOpen.Selection.Response.Ok && tagObjs.Length > 0)
				{
					foreach(TaggedObject tagObj in tagObjs)
					{
						 if (tagObj.GetType() == typeof(NXOpen.Face))
						 {
							 NXOpen.Face face = (NXOpen.Face)tagObj;                       
							 int newColor = 145;
							 face.Color = newColor;
							 face.RedisplayObject();
						 }
					}
				}
				else
				{
					theUI.NXMessageBox.Show("NXOpen", NXMessageBox.DialogType.Information, "You did not select anything, program will stop !");
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
