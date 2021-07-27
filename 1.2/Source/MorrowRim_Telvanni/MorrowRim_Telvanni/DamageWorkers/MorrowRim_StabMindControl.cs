using RimWorld;
using Verse;

namespace MorrowRim_Telvanni
{
    class MorrowRim_StabMindControl : DamageWorker_Stab
    {
		public override DamageWorker.DamageResult Apply(DamageInfo dinfo, Thing victim)
		{
			Pawn pawn = victim as Pawn;
			if (pawn != null && pawn.Faction == Faction.OfPlayer)
			{
				Find.TickManager.slower.SignalForceNormalSpeedShort();
			}
			DamageWorker.DamageResult damageResult = base.Apply(dinfo, victim);

            if (ValidTarget(pawn))
            {
				DoMindControl(pawn, dinfo.Instigator as Pawn);
			}
			return damageResult;
		}

		public static void DoMindControl(Pawn victim, Pawn instigator)
        {
			victim.health.AddHediff(HediffDefOf.MorrowRim_MindControl).Severity = 1;
			instigator.Kill(null);
        }

		public static bool ValidTarget(Pawn victim)
        {
			return victim != null && victim.RaceProps.IsFlesh;
        }
	}
}
