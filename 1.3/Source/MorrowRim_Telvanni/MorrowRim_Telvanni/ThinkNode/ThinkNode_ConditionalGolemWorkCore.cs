using System;
using Verse;
using Verse.AI;
using RimWorld;

namespace MorrowRim_Telvanni
{
    class ThinkNode_ConditionalGolemWorkCore : ThinkNode_Conditional
	{
		public override ThinkNode DeepCopy(bool resolve = true)
		{
			ThinkNode_ConditionalGolemWorkCore thinkNode_ConditionalGolemWorkCore = (ThinkNode_ConditionalGolemWorkCore)base.DeepCopy(resolve);
			thinkNode_ConditionalGolemWorkCore.coreHediff = this.coreHediff;
			thinkNode_ConditionalGolemWorkCore.inverse = this.inverse;
			return thinkNode_ConditionalGolemWorkCore;
		}


		protected override bool Satisfied(Pawn pawn)
		{
            if (inverse)
            {
				return pawn.health.hediffSet.GetFirstHediffOfDef(this.coreHediff) == null;

			}
			return pawn.health.hediffSet.GetFirstHediffOfDef(this.coreHediff) != null;
		}


		private HediffDef coreHediff;
		private bool inverse = false;
	}
}
