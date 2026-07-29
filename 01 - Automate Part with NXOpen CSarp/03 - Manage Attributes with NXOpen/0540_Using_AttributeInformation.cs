using NXOpen;
using System;

namespace Attributes
{
    public class Exercice
    {
        private static Session theSession = Session.GetSession();
        private static UI theUI = UI.GetUI();
        //private static UFSession theUfSession = UFSession.GetUFSession();


        public static void Main(string[] args)
        {
            Part WorkPart = theSession.Parts.Work;
            try
            {
                // Create Name Attribute
				NXObject.AttributeInformation info = new NXObject.AttributeInformation();
				info.Category = "NXOpen";
				info.Title = "Name";
				info.Type = NXObject.AttributeType.String;
				info.StringValue = "Support";
				WorkPart.SetUserAttribute(info, Update.Option.Now);
				
				// Create N°Article Attribute
				NXObject.AttributeInformation info1 = new NXObject.AttributeInformation();
				info1.Category = "NXOpen";
				info1.Title = "N°Article";
				info1.Type = NXObject.AttributeType.String;
				info1.StringValue = "C435";
				WorkPart.SetUserAttribute(info1, Update.Option.Now);
				
				// Create Specification Attribute based on an array
				// Index 0
				NXObject.AttributeInformation info2 = new NXObject.AttributeInformation();
				info2.Category = "NXOpen";
				info2.Title = "Specification";
				info2.Type = NXObject.AttributeType.String;
				info2.Array = true;
				info2.ArrayElementIndex = 0;
				info2.StringValue = "SP212";
				WorkPart.SetUserAttribute(info2, Update.Option.Now);
				
				// Index 1
				NXObject.AttributeInformation info3 = new NXObject.AttributeInformation();
				info3.Category = "NXOpen";
				info3.Title = "Specification";
				info3.Type = NXObject.AttributeType.String;
				info3.Array = true;
				info3.ArrayElementIndex = 1;
				info3.StringValue = "test";
				WorkPart.SetUserAttribute(info3, Update.Option.Now);
						
            }
            catch (Exception ex)
            {
                theUI.NXMessageBox.Show("NX Open Tuto", NXMessageBox.DialogType.Error, ex.Message);
            }
        }

        public static int GetUnloadOption(string arg)
        {
            //Unloads the image explicitly, via an unload dialog
            //return System.Convert.ToInt32(Session.LibraryUnloadOption.Explicitly);

            //Unloads the image immediately after execution within NX
            return System.Convert.ToInt32(Session.LibraryUnloadOption.Immediately);

            //Unloads the image when the NX session terminates
            // return System.Convert.ToInt32(Session.LibraryUnloadOption.AtTermination);
        }
    }
}
