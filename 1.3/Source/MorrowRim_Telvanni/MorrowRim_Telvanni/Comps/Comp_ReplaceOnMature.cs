using System;
using Verse;
using RimWorld;

namespace MorrowRim_Telvanni
{
	class Comp_ReplaceOnMature : ThingComp
	{
		public CompProperties_ReplaceOnMature Props
		{
			get
			{
				return (CompProperties_ReplaceOnMature)this.props;
			}
		}

		public override void CompTickRare()
		{
			base.CompTickRare();
			Plant plant = parent as Plant;
			Log.Message("Growth at: " + plant.Growth);
			if(plant.Growth == 1)
			{
				GenSpawn.Spawn(Props.MatureInto, plant.Position, plant.Map);
			}
		}
	}
}
