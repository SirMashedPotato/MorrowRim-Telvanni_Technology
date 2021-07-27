using System;
using Verse;
using RimWorld;

namespace MorrowRim_Telvanni
{
    class CompTargetEffect_CreateAt : CompTargetEffect
    {
		public override void DoEffectOn(Pawn user, Thing target)
		{
			Log.Message("I'm trying, target is: " + target);
			GenSpawn.Spawn(RimWorld.ThingDefOf.Wall, target.Position, target.Map);
			Find.BattleLog.Add(new BattleLogEntry_ItemUsed(user, target, this.parent.def, RulePackDefOf.Event_ItemUsed));
		}
	}
}
