using System;
using Verse;
using RimWorld;

namespace MorrowRim_Telvanni
{
	[DefOf]
	public static class BodyPartDefOf
	{
		static BodyPartDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(BodyPartDefOf));
		}

		public static BodyPartDef MorrowRim_GolemBrainCore;
		public static BodyPartDef MorrowRim_GolemHeartCore;
	}
}
