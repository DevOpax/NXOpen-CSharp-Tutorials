using NXOpen;
using NXOpen.Assemblies;
using NXOpen.Positioning;
using System;

namespace Constraints
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
				/*
				  * to = Touch
				  * pe = Perpendicular
				  * pa = parallel
				  * al = AlignLock
				*/
				  
				// string type = NXInputBox.GetInputString("Which constraint do you want ?");
				// Constraint.Type constType = new Constraint.Type();

				// switch (type)
				// {
				    // case "to":
				        // constType = Constraint.Type.Touch;
				        // break;
				    // case "pe":
				        // constType = Constraint.Type.Perpendicular;
				        // break;
				    // case "pa":
				        // constType = Constraint.Type.Parallel;
				        // break;
				    // case "al":
				        // constType = Constraint.Type.AlignLock;
				        // break;
				    // default:

				        // break;
				// }

				// Add2RefsConbstraint(workPart, constType); 
				
				AddCenter(workPart);
					
            }
            catch (Exception ex)
            {
                theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Error, ex.Message);
            }
        }
		
		public static void AddCenter(Part prt)
		{
			Session.UndoMarkId markId = theSession.SetUndoMark(Session.MarkVisibility.Visible, "add constraint");

			Selection selMgr = theUI.SelectionManager;
			int cnt = selMgr.GetNumSelectedObjects();

			if (cnt >= 3 && cnt <= 4)
			{
				TaggedObject[] tagObjs = new TaggedObject[cnt];
				NXObject[] nxObjs = new NXObject[cnt];
				Component[] comps = new Component[cnt];

				ComponentPositioner compPos = prt.ComponentAssembly.Positioner;
				compPos.ClearNetwork();
				compPos.BeginAssemblyConstraints();
				Constraint constr = compPos.CreateConstraint(true);

				if(cnt == 3)
				{
					constr.ConstraintType = Constraint.Type.Center12;
				}
				else
				{
					constr.ConstraintType = Constraint.Type.Center22;
				}

				for (int i =0; i < cnt; i++)
				{
					tagObjs[i] = selMgr.GetSelectedTaggedObject(i);
					nxObjs[i] = tagObjs[i] as NXObject;
					comps[i] = nxObjs[i].OwningComponent;

					constr.CreateConstraintReference(comps[i], nxObjs[i], false, false);
				}

				compPos.EndAssemblyConstraints();
				compPos.ClearNetwork();
				theSession.UpdateManager.DoUpdate(markId);
			}
			else
			{
				theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Information, "Select 3 or 4 geometries please....");
			}
		}
		
		public static void Add2RefsConbstraint(Part prt, Constraint.Type type)
		{
			Session.UndoMarkId markId = theSession.SetUndoMark(Session.MarkVisibility.Visible, "add constraint");

			Selection selMgr = theUI.SelectionManager;
			int cnt = selMgr.GetNumSelectedObjects();

			if (cnt == 2)
			{
				//Get geometry seletced
				TaggedObject tag1 = selMgr.GetSelectedTaggedObject(0);
				TaggedObject tag2 = selMgr.GetSelectedTaggedObject(1);
				NXObject obj1 = tag1 as NXObject;
				NXObject obj2 = tag2 as NXObject;

				//Get component frm geometry
				Component comp1 = obj1.OwningComponent;
				Component comp2 = obj2.OwningComponent;

				if (comp1 != comp2)
				{
					//Initialize constraint
					ComponentPositioner compPos = prt.ComponentAssembly.Positioner;
					compPos.ClearNetwork();
					compPos.BeginAssemblyConstraints();

					Constraint constr = compPos.CreateConstraint(true);

					constr.ConstraintType = type;

					if (type == Constraint.Type.Touch)
					{
						constr.ConstraintAlignment = Constraint.Alignment.InferAlign;
					}			

					//Create references for the constraint
					ConstraintReference constRef1 = constr.CreateConstraintReference(comp1, obj1, false, false);
					ConstraintReference constRef2 = constr.CreateConstraintReference(comp2, obj2, false, false);
			 
					compPos.EndAssemblyConstraints();
					theSession.UpdateManager.DoUpdate(markId);
				}
				else
				{
					theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Information, "Geometries must be on different components.");
				}
			}
			else
			{
				theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Information, "Select 2 geometries please.");
			}

		}
		
		public static void TouchAlignConstraint(Part prt, Constraint.Alignment align)
		{
			Session.UndoMarkId markId = theSession.SetUndoMark(Session.MarkVisibility.Visible, "add constraint");

			Selection selMgr = theUI.SelectionManager;
			int cnt = selMgr.GetNumSelectedObjects();

			if (cnt == 2)
			{
				//Get geometry seletced
				TaggedObject tag1 = selMgr.GetSelectedTaggedObject(0);
				TaggedObject tag2 = selMgr.GetSelectedTaggedObject(1);
				NXObject obj1 = tag1 as NXObject;
				NXObject obj2 = tag2 as NXObject;

				//Get component from geometry
				Component comp1 = obj1.OwningComponent;
				Component comp2 = obj2.OwningComponent;

				if (comp1 != comp2)
				{
					//Initialize constraint
					ComponentPositioner compPos = prt.ComponentAssembly.Positioner;
					compPos.ClearNetwork();
					compPos.BeginAssemblyConstraints();

					Constraint constr = compPos.CreateConstraint(true);
					constr.ConstraintType = Constraint.Type.Touch;
					constr.ConstraintAlignment = align;

					//Create references for the constraint
					ConstraintReference constRef1 = constr.CreateConstraintReference(comp1, obj1, false, false);
					ConstraintReference constRef2 = constr.CreateConstraintReference(comp2, obj2, false, false);

					theSession.UpdateManager.DoUpdate(markId);
					compPos.EndAssemblyConstraints();
				}
				else
				{
					theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Information, "Geometries must be on different components.");
				}

			}
			else
			{

				theUI.NXMessageBox.Show("NX Open", NXMessageBox.DialogType.Information, "Select 2 geometries please.");
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