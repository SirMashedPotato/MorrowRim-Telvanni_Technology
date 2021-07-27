using System;
using System.Collections.Generic;
using Verse;
using RimWorld;

namespace MorrowRim_Telvanni
{
	public class CompTargetable_GolemRepair : CompTargetable
	{
		protected override bool PlayerChoosesTarget
		{
			get
			{
				return true;
			}
		}

		protected override TargetingParameters GetTargetingParameters()
		{
			return new TargetingParameters
			{
				canTargetPawns = true,
				validator = ((TargetInfo x) => TargetValidator(x.Thing))
			};
		}

		public bool TargetValidator(Thing t)
		{
			Pawn pawn = t as Pawn;
			if (pawn != null)
			{
				if (GolemUtility.IsGolem(pawn) && GolemUtility.CheckGolemForRepair(pawn))
				{
					return true;
				}
			}
			return false;
		}

		public override IEnumerable<Thing> GetTargets(Thing targetChosenByPlayer = null)
		{
			if (GolemUtility.IsGolem(targetChosenByPlayer))
			{
				yield return targetChosenByPlayer;
			}
			yield break;
		}
	}
}
