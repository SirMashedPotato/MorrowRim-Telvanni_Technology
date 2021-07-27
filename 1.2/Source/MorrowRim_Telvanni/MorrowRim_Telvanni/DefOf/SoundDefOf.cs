using System;
using Verse;
using RimWorld;

namespace MorrowRim_Telvanni
{
	[DefOf]
	public static class SoundDefOf
	{
		static SoundDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(SoundDefOf));
		}
		public static SoundDef MorrowRim_Impact_TelvanniSpider; 
	}
}
