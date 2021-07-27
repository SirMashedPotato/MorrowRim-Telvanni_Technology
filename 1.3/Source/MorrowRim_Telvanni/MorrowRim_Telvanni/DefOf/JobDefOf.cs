using System;
using Verse;
using RimWorld;

namespace MorrowRim_Telvanni
{
	[DefOf]
	public static class JobDefOf
	{
		static JobDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(JobDefOf));
		}

		public static JobDef MorrowRim_ApplyGolemCore_Upgrade;
		public static JobDef MorrowRim_ApplyGolemCore;
		public static JobDef MorrowRim_RepairGolem;
		public static JobDef MorrowRim_DestroyGolem;

		//Golem
		public static JobDef MorrowRim_ServantRepair;
		public static JobDef MorrowRim_ServantDeconstruct;
		public static JobDef MorrowRim_ServantFinishFrame;
		public static JobDef MorrowRim_ServantResearch;
		public static JobDef MorrowRim_ServantSelfRepair;
	}
}
