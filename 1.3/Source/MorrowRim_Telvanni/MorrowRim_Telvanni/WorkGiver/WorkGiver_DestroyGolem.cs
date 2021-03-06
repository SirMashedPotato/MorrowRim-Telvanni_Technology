using System;
using System.Collections.Generic;
using Verse;
using Verse.AI;
using RimWorld;

namespace MorrowRim_Telvanni
{
    class WorkGiver_DestroyGolem : WorkGiver_Scanner
    {
		public override IEnumerable<Thing> PotentialWorkThingsGlobal(Pawn pawn)
		{
			foreach (Designation designation in pawn.Map.designationManager.SpawnedDesignationsOfDef(DesignationDefOf.MorrowRim_DestroyGolem))
			{
				yield return designation.target.Thing;
			}
			IEnumerator<Designation> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x060030F4 RID: 12532 RVA: 0x00010451 File Offset: 0x0000E651
		public override PathEndMode PathEndMode
		{
			get
			{
				return PathEndMode.OnCell;
			}
		}

		// Token: 0x060030F5 RID: 12533 RVA: 0x00113AB7 File Offset: 0x00111CB7
		public override bool ShouldSkip(Pawn pawn, bool forced = false)
		{
			return !pawn.Map.designationManager.AnySpawnedDesignationOfDef(DesignationDefOf.MorrowRim_DestroyGolem);
		}

		// Token: 0x060030F6 RID: 12534 RVA: 0x00113AD4 File Offset: 0x00111CD4
		public override bool HasJobOnThing(Pawn pawn, Thing t, bool forced = false)
		{
			Pawn pawn2 = t as Pawn;
			if (pawn2 == null)
			{
				return false;
			}
			if (pawn.Map.designationManager.DesignationOn(t, DesignationDefOf.MorrowRim_DestroyGolem) == null)
			{
				return false;
			}
			if (pawn.Faction != t.Faction)
			{
				return false;
			}
			if (pawn2.InAggroMentalState)
			{
				return false;
			}
			if (!pawn.CanReserve(t, 1, -1, null, forced))
			{
				return false;
			}
			return true;
		}

		// Token: 0x060030F7 RID: 12535 RVA: 0x00113B69 File Offset: 0x00111D69
		public override Job JobOnThing(Pawn pawn, Thing t, bool forced = false)
		{
			return JobMaker.MakeJob(JobDefOf.MorrowRim_DestroyGolem, t);
		}
	}
}
