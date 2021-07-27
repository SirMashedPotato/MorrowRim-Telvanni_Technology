using System;
using System.Linq;
using RimWorld;
using UnityEngine;
using Verse;

namespace MorrowRim_Telvanni
{
	class DamageWorker_StormStave : DamageWorker_Blunt
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
			if (Rand.Chance(0.25f))
			{
				DamageInfo damageInfo = new DamageInfo();
				damageInfo.Def = DamageDefOf.EMP;
				damageInfo.SetAmount(damageResult.totalDamageDealt);
				victim.TakeDamage(damageInfo);
			}
			return damageResult;
		}
	}
}
