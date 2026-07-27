using NXOpen;
using System;

namespace Expressions
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

                Unit mmCube = WorkPart.UnitCollection.GetBase("Volume");
                Expression exp = WorkPart.Expressions.NewExpression("Number", "Volume=(p1-p0)*p2*p3", mmCube, false, false);

                if (exp != null)
                {

                    NXObject.AttributeInformation info = new NXObject.AttributeInformation();
                    info.Type = NXObject.AttributeType.Real;
                    info.Expression = exp;
                    info.Title = "Volume";
                    WorkPart.SetUserAttribute(info, Update.Option.Now);
                }
                else
                {
                    theUI.NXMessageBox.Show("NX Open Tuto", NXMessageBox.DialogType.Error, "Expression Volume not created.");
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
