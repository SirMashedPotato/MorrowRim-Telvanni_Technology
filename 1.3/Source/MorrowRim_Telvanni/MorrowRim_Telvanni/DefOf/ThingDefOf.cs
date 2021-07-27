using System;
using Verse;
using RimWorld;

namespace MorrowRim_Telvanni
{
	[DefOf]
	public static class ThingDefOf
	{
		static ThingDefOf()
		{
			DefOfHelper.EnsureInitializedInCtor(typeof(ThingDefOf));
		}
		public static ThingDef MorrowRim_HeartStone;
		public static ThingDef MorrowRim_HeartStoneDepleted;
		public static ThingDef MorrowRim_MineableHeartStone;

		//salts flowers
		public static ThingDef Telvanni_AshCap;
		public static ThingDef Telvanni_FirePetal;
		public static ThingDef Telvanni_FrostLeaf;
		public static ThingDef Telvanni_VoidBloom;
		public static ThingDef Telvanni_WindBulb;
		public static ThingDef Telvanni_StoneAnther;
		public static ThingDef Telvanni_WaterRoot;

		//extract plants
		public static ThingDef Telvanni_GraveNut;
		public static ThingDef Telvanni_VampireGrass;
		public static ThingDef Telvanni_SpectralBell;
		public static ThingDef Telvanni_NightPitcher;
		public static ThingDef Telvanni_BoltVine;

		//golems
		public static ThingDef MorrowRim_GolemRepairCrystal;

		//Vanilla RimWorld
		public static ThingDef Filth_BloodInsect;
	}
}
