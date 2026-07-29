using NXOpen;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Sample_ERP
{
    public class Exercice
    {
        private static Session theSession = Session.GetSession();
        private static UI theUI = UI.GetUI();
        //private static UFSession theUfSession = UFSession.GetUFSession();


        public static void Main(string[] args)
        {
            
            var db = new List<Dictionary<string, string>> {
                new Dictionary<string, string> {
                    { "No Article", "A123" },
                    { "Name", "Plate" },
                    { "Description", "Base Plate" }
                },
                new Dictionary<string, string> {
                    { "No Article", "B702" },
                    { "Name", "Support" },
                    { "Description", "Engine support" }
                },
                new Dictionary<string, string> {
                    { "No Article", "C300" },
                    { "Name", "Axe" },
                    { "Description", "Main axe" }
                },
            };

            Part WorkPart = theSession.Parts.Work;

            if (WorkPart != null)
            {
                try
                {

                    string inputArt = NXOpenUI.NXInputBox.GetInputString("Enter N° Article :", "Input", "");

                    if (!String.IsNullOrEmpty(inputArt))
                    {
                        var result = db.FirstOrDefault(x => (string)x["No Article"] == inputArt);

                        if (result != null)
                        {
                            
                            NXObject.AttributeInformation attrId = new NXObject.AttributeInformation();
                            attrId.Type = NXObject.AttributeType.String;          
                            attrId.Category = "ERP Info";
                            attrId.Title = "Article Number";
                            attrId.StringValue = result["No Article"];
                            WorkPart.SetUserAttribute(attrId, Update.Option.Now);

                            NXObject.AttributeInformation attrName = new NXObject.AttributeInformation();
                            attrName.Type = NXObject.AttributeType.String;          
                            attrName.Category = "ERP Info";
                            attrName.Title = "Name";
                            attrName.StringValue = result["Name"];
                            WorkPart.SetUserAttribute(attrName, Update.Option.Now);

                            NXObject.AttributeInformation attrMaterial = new NXObject.AttributeInformation();
                            attrMaterial.Type = NXObject.AttributeType.String;        
                            attrMaterial.Category = "ERP Info";
                            attrMaterial.Title = "Description";
                            attrMaterial.StringValue = result["Description"];
                            WorkPart.SetUserAttribute(attrMaterial, Update.Option.Now);

                        }
                        else
                        {
                            theUI.NXMessageBox.Show("Message", NXMessageBox.DialogType.Error, "N° article not available");
                        }

                    }
                    else
                    {
                        theUI.NXMessageBox.Show("Message", NXMessageBox.DialogType.Error, "N° article not avalaible");
                    }

                }
                catch (Exception ex)
                {
                    theUI.NXMessageBox.Show("Message", NXMessageBox.DialogType.Error, ex.Message);
                }


            }
            else
            {
                theUI.NXMessageBox.Show("Message", NXMessageBox.DialogType.Error, "No active Workpart");
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
