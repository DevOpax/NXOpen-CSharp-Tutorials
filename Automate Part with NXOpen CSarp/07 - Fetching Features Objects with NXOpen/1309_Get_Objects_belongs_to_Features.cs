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
                ListingWindow lw = theSession.ListingWindow;
                lw.Open();

                Feature[] features = workPart.Features.GetFeatures();
                foreach (Feature feature in features)
                {
					if(!feature.IsInternal) { 

						lw.WriteLine("-------------------------");
						lw.WriteLine(feature.GetFeatureName());
						lw.WriteLine("-------------------------");

						lw.WriteLine("Type : " + feature.FeatureType);
						lw.WriteLine("Tag : " + feature.Tag.ToString());

						NXObject.AttributeInformation[] attributes = feature.GetUserAttributes().ToArray();
						lw.WriteLine("Attribute(s) : " + attributes.Length);
						if (attributes.Length > 0)
						{
							foreach(NXObject.AttributeInformation attribute in attributes)
							{
								lw.WriteLine("|__ Attribute : " + attribute.Title + ", " + attribute.StringValue);
							}
						}

						Feature[] childs = feature.GetAllChildren();
						lw.WriteLine("Children : " + childs.Length);
						if(childs.Length > 0)
						{
							foreach(Feature child in childs)
							{
								lw.WriteLine("|__ Child : " + child.GetFeatureName());                           
							}
						}
						
						NXObject[] objects = feature.GetEntities();
						lw.WriteLine("NXObject(s) : " + objects.Length);
						if(objects.Length > 0)
						{
							foreach(NXObject obj in objects)
							{
								DisplayableObject dispObjects = obj as DisplayableObject;
								NXObject.AttributeInformation[] dispAttr = dispObjects.GetUserAttributes().ToArray();

								lw.WriteLine("|__ Displayable object : " + dispObjects.GetType().FullName);
								lw.WriteLine("    Tag :" + dispObjects.Tag.ToString());
								lw.WriteLine("    Name : " + dispObjects.Name);
								lw.WriteLine("    Layer : " + dispObjects.Layer);

								lw.WriteLine("    Attribute(s) : " + dispAttr.Length);
								if(dispAttr.Length > 0)
								{
									foreach(NXObject.AttributeInformation disp in dispAttr)
									{
										lw.WriteLine("    |__ Title : " + disp.Title + ", Value : " + disp.StringValue);
									}
								}
							}
						}
					}
					 
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
