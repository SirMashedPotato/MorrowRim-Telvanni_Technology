using System;
using Verse;
using RimWorld;
using System.Collections.Generic;


namespace MorrowRim_Telvanni
{
	class CompProperties_GrowthModifier : CompProperties
	{
		public CompProperties_GrowthModifier()
		{
			this.compClass = typeof(Comp_GrowthModifier);
		}
		public List<WeatherDef> weather;
		public float amount = 0.005f;
		public bool checkLight = false;
		public bool allowIndoors = false;
		public float minGrowth = 0.1f;
	}
}