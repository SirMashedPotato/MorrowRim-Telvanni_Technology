using System;
using Verse;
using RimWorld;

namespace MorrowRim_Telvanni
{
	[DefOf]
	public static class DesignationDefOf
	{
		static DesignationDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(DesignationDefOf));
		}

		public static DesignationDef MorrowRim_DestroyGolem;
		public static DesignationDef MorrowRim_RepairGolem;

	}
}
