using NXOpen;
using NXOpen.Assemblies;
using System;

namespace Assembly
{
    public class Program
    {
        private static Session theSession = Session.GetSession();
        private static UI theUI = UI.GetUI();

        public static void Main(string[] args)
        {
            Part workPart = theSession.Parts.Work;
			Component root = workPart.ComponentAssembly.RootComponent;
			

            try
            {
				Session.UndoMarkId markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Undo color");

				Random rnd = new Random();
				ChangePartsColor(root, rnd);
				
            }
            catch (Exception ex)
            {
                theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Error, ex.Message);
            }
        }
		
		public static void ChangePartsColor(Component comp, Random rnd)
        {
			foreach (Component cp in comp.GetChildren())
			{
				Part proto = cp.Prototype as Part;

				if (proto != null)
				{
					Body[] bodies = proto.Bodies.ToArray();
					if (bodies.Length > 0)
					{
						DisplayModification dm = theSession.DisplayManager.NewDisplayModification();
						dm.ApplyToOwningParts = true;
						dm.NewColor = rnd.Next(1, 217);
						dm.Apply(bodies);
						dm.Dispose();
					}
				}

				if (cp.GetChildren().Length > 0)
				{
					ChangePartsColor(cp, rnd);
				}
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
