using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Verse;
using RimWorld;

namespace MorrowRim_Telvanni
{
	// Token: 0x02000EC1 RID: 3777
	public class Designator_RepairGolem
		: Designator
	{
		// Token: 0x17001068 RID: 4200
		// (get) Token: 0x06005C14 RID: 23572 RVA: 0x0007E738 File Offset: 0x0007C938
		public override int DraggableDimensions
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17001069 RID: 4201
		// (get) Token: 0x06005C15 RID: 23573 RVA: 0x001E9D6F File Offset: 0x001E7F6F
		protected override DesignationDef Designation
		{
			get
			{
				return DesignationDefOf.MorrowRim_RepairGolem;
			}
		}

		// Token: 0x06005C16 RID: 23574 RVA: 0x001E9D78 File Offset: 0x001E7F78
		public Designator_RepairGolem()
		{
			this.defaultLabel = "MorrowRim_DesignatorRepairGolem".Translate();
			this.defaultDesc = "MorrowRim_DesignatorRepairGolemDesc".Translate();
			this.icon = ContentFinder<Texture2D>.Get("UI/Designators/MorrowRim_RepairGolem", true);
			this.soundDragSustain = RimWorld.SoundDefOf.Designate_DragStandard;
			this.soundDragChanged = RimWorld.SoundDefOf.Designate_DragStandard_Changed;
			this.useMouseIcon = true;
			this.soundSucceeded = RimWorld.SoundDefOf.Designate_Hunt;
		}

		// Token: 0x06005C17 RID: 23575 RVA: 0x001E9E04 File Offset: 0x001E8004
		public override AcceptanceReport CanDesignateCell(IntVec3 c)
		{
			if (!c.InBounds(base.Map))
			{
				return false;
			}
			if (!this.SlaughterablesInCell(c).Any<Pawn>())
			{
				return "MorrowRim_MessageMustDesignateGolemRepair".Translate();
			}
			return true;
		}

		// Token: 0x06005C18 RID: 23576 RVA: 0x001E9E40 File Offset: 0x001E8040
		public override void DesignateSingleCell(IntVec3 loc)
		{
			foreach (Pawn t in this.SlaughterablesInCell(loc))
			{
				this.DesignateThing(t);
			}
		}

		// Token: 0x06005C19 RID: 23577 RVA: 0x001E9E90 File Offset: 0x001E8090
		public override AcceptanceReport CanDesignateThing(Thing t)
		{
			Pawn pawn = t as Pawn;
			if (pawn != null && GolemUtility.IsGolem(pawn) && GolemUtility.CheckGolemForRepair(pawn) && pawn.Faction == Faction.OfPlayer && base.Map.designationManager.DesignationOn(pawn, this.Designation) == null && !pawn.InAggroMentalState)
			{
				return true;
			}
			return false;
		}

		// Token: 0x06005C1A RID: 23578 RVA: 0x001E9EF4 File Offset: 0x001E80F4
		public override void DesignateThing(Thing t)
		{
			base.Map.designationManager.RemoveAllDesignationsOn(t, false);
			base.Map.designationManager.AddDesignation(new Designation(t, this.Designation));
			this.justDesignated.Add((Pawn)t);
		}

		// Token: 0x06005C1B RID: 23579 RVA: 0x001E9F28 File Offset: 0x001E8128
		protected override void FinalizeDesignationSucceeded()
		{
			base.FinalizeDesignationSucceeded();
			this.justDesignated.Clear();
		}

		// Token: 0x06005C1C RID: 23580 RVA: 0x001E9F6D File Offset: 0x001E816D
		private IEnumerable<Pawn> SlaughterablesInCell(IntVec3 c)
		{
			if (c.Fogged(base.Map))
			{
				yield break;
			}
			List<Thing> thingList = c.GetThingList(base.Map);
			int num;
			for (int i = 0; i < thingList.Count; i = num + 1)
			{
				if (this.CanDesignateThing(thingList[i]).Accepted)
				{
					yield return (Pawn)thingList[i];
				}
				num = i;
			}
			yield break;
		}

		// Token: 0x04003240 RID: 12864
		private List<Pawn> justDesignated = new List<Pawn>();
	}
}
