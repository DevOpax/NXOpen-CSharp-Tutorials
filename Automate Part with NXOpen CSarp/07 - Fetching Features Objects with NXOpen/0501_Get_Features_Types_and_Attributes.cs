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
