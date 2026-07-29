using NXOpen;
using NXOpen.Assemblies;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BOM_Extractor
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
                //Component cRoot = workPart.ComponentAssembly.RootComponent;
                Component cRoot;
                Selection selMgr = theUI.SelectionManager;
                int count = selMgr.GetNumSelectedObjects();

                if (count == 1)
                {
                    TaggedObject tgObj = selMgr.GetSelectedTaggedObject(0);
                    cRoot = tgObj as Component;
                    if (cRoot.GetChildren().Length == 0)
                    {
                        theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Information, "Selected component is not an assembly.");
                        return;
                    }
                }
                else if (count > 1)
                {
                    theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Information, "Selected one assembly only.");
                    return;
                }
                else
                {
                    cRoot = workPart.ComponentAssembly.RootComponent;
                }

                if (cRoot != null)
                {
                    Part pRoot = cRoot.Prototype as Part;
                    PartAttributes rAttr = new PartAttributes(pRoot);
                    List<Component> listComp = new List<Component>();
                    List<PartAttributes> partAttrs = new List<PartAttributes>();
                    Traverse(cRoot, ref listComp);
                    foreach (Component cp in listComp)
                    {
                        Part prt = cp.Prototype as Part;
                        PartAttributes pa = new PartAttributes(prt);

                        var item = partAttrs.FirstOrDefault(x => x.Id == pa.Id);
                        if (item != null)
                        {
                            item.Increment();
                        }
                        else
                        {
                            partAttrs.Add(pa);
                        }
                    }

                    WriteConsole(partAttrs, rAttr);
                    WriteCSVFile(partAttrs, rAttr);

                }
                else
                {
                    theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Information, "Root component is not an assembly.");
                }

                //theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Information, "Hello world");
            }
            catch (Exception ex)
            {
                theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Error, ex.Message);
            }

        }

        public static void WriteConsole(List<PartAttributes> list, PartAttributes rAttr)
        {
            ListingWindow lw = theSession.ListingWindow;
            lw.Open();

            lw.WriteLine($"{rAttr.Id}/{rAttr.Revision} - {rAttr.Name}");
            lw.WriteLine("----------------------------------------------------");

            string col0 = "ID";
            string col1 = "Revision";
            string col2 = "Name";
            string col3 = "Quantity";
            string col4 = "Material";
            string col5 = "Mass";

            //Header
            lw.WriteLine($"" +
                $"{col0.PadRight(20)}" +
                $"{col1.PadRight(10)}" +
                $"{col2.PadRight(30)}" +
                $"{col3.PadRight(15)}" +
                $"{col4.PadRight(30)}" +
                $"{col5}");

            foreach (PartAttributes cp in list)
            {
                lw.WriteLine($"" +
                    $"{cp.Id.PadRight(20)}" +
                    $"{cp.Revision.PadRight(10)}" +
                    $"{cp.Name.PadRight(30)}" +
                    $"{cp.Quantity.ToString().PadRight(15)}" +
                    $"{cp.Material.PadRight(30)}" +
                    $"{Math.Round(cp.Mass, 4)}" + " kg");

            }

        }

        public static void WriteCSVFile(List<PartAttributes> list, PartAttributes rAttr)
        {
            StringBuilder sb = new StringBuilder();

            //Assembly info
            sb.AppendLine($"Assembly;{rAttr.Id}/{rAttr.Revision};{rAttr.Name}");

            //Column header
            sb.AppendLine("ID;REVISION;NAME;QUANTITY;MATERIAL;MASS");


            foreach (PartAttributes cp in list)
            {
                sb.AppendLine($"{cp.Id};{cp.Revision};{cp.Name};{cp.Quantity.ToString()};{cp.Material};{cp.Mass}");
            }

            string filename = @"c:\temp\" + rAttr.Id + ".csv";

            File.WriteAllText(filename, sb.ToString(), Encoding.UTF8);


        }

        public static void Traverse(Component comp, ref List<Component> listComp)
        {
            Component[] children = comp.GetChildren();

            if (children.Length == 0)
            { 
                if (comp.IsSuppressed == false)
                    listComp.Add(comp);
                return;
            }

            foreach (Component child in children)
            {
                Traverse(child, ref listComp);
            }
        }

        public static int GetUnloadOption(string arg)
        {
            return System.Convert.ToInt32(Session.LibraryUnloadOption.Immediately);
            //return System.Convert.ToInt32(Session.LibraryUnloadOption.Explicitly);
            //return System.Convert.ToInt32(Session.LibraryUnloadOption.AtTermination);

        }
    }

    public class PartAttributes
    {
        private readonly Part thePart;


        public string Id { get; }
        public string Revision { get; }
        public string Name { get; }
        public int Quantity { get; set; }
        public string Material { get; }
        public double Mass { get; set; }


        public PartAttributes(Part prt)
        {
            thePart = prt;

            Id = GetStringAttr("DB_PART_NO");
            Revision = GetStringAttr("DB_PART_REV");
            Name = GetStringAttr("DB_PART_NAME");
            Quantity = 1;
            Material = GetStringAttr("Material");
            Mass = GetDoubleAttr("MassPropMass");


        }

        public void Increment()
        {
            Quantity++;
            Mass *= 2;
        }

        private string GetStringAttr(string name)
        {
            if(thePart.HasUserAttribute(name, NXObject.AttributeType.String, -1))
            {
                return thePart.GetUserAttributeAsString(name, NXObject.AttributeType.String, -1);
            }
            else
            {
                return "Not Specified";
            }
        }

        private double GetDoubleAttr(string name)
        {
            if (thePart.HasUserAttribute(name, NXObject.AttributeType.Real, -1))
            {
                NXObject.AttributeInformation info = thePart.GetUserAttribute(name, NXObject.AttributeType.Real, -1);
                return info.RealValue;
            }
            else
            {
                return 0.00;
            }
        }

    }
}
