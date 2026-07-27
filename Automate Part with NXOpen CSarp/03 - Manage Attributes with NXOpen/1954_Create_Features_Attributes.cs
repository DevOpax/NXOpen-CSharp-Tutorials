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
                foreach (NXOpen.Features.Feature feat in WorkPart.Features)
                {

                    if(feat.Name == "MIDDLE_DATUM")
                    {
                        NXObject.AttributeInformation info = new NXObject.AttributeInformation();
                        info.Type = NXObject.AttributeType.Integer;
                        info.Category = "Datum Plane";
                        info.Title = feat.Name;
                        info.IntegerValue = 125;
                        feat.SetUserAttribute(info, Update.Option.Now);
                    }
                    
                }
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
