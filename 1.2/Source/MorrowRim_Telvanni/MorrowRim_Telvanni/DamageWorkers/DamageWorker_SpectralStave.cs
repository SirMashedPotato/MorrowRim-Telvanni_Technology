using System;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;

namespace MorrowRim_Telvanni
{
    class DamageWorker_SpectralStave : DamageWorker_AddInjury
    {
		public override DamageWorker.DamageResult Apply(DamageInfo dinfo, Thing victim)
		{
			Pawn pawn = victim as Pawn;
			if (pawn != null && pawn.Faction == Faction.OfPlayer)
			{
				Find.TickManager.slower.SignalForceNormalSpeedShort();
			}
			DamageWorker.DamageResult damageResult = base.Apply(dinfo, victim);
			//Start the fire engines
			if (Rand.Chance(0.25f) && pawn != null)
			{
				DamageInfo damageInfo = new DamageInfo();
				int rand = Rand.RangeInclusive(1, 3);
				switch (rand)
				{
					case 1:
						damageInfo.Def = DamageDefOf.Bite;
						break;
					case 2:
						damageInfo.Def = DamageDefOf.Blunt;
						break;
					default:
						damageInfo.Def = DamageDefOf.Scratch;
						break;
				}
				damageInfo.SetAmount(damageResult.totalDamageDealt / 4);
				victim.TakeDamage(damageInfo);
			}
			return damageResult;
		}
	}
}
