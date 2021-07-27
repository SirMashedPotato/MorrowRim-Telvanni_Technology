using System;
using System.Collections.Generic;
using Verse;
using Verse.AI;
using RimWorld;
using System.Linq;
using UnityEngine;

namespace MorrowRim_Telvanni
{
	public class JobDriver_DestroyGolem : JobDriver
	{
		protected Pawn Victim
		{
			get
			{
				return (Pawn)this.job.targetA.Thing;
			}
		}

		public override bool TryMakePreToilReservations(bool errorOnFailed)
		{
			return this.pawn.Reserve(this.Victim, this.job, 1, -1, null, errorOnFailed);
		}

		protected override IEnumerable<Toil> MakeNewToils()
		{
			this.FailOnAggroMentalState(TargetIndex.A);
			this.FailOnThingMissingDesignation(TargetIndex.A, DesignationDefOf.MorrowRim_DestroyGolem);
			yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.Touch);
			yield return Toils_General.WaitWith(TargetIndex.A, 180, true, false);
			yield return Toils_General.Do(delegate
			{
				DoExecutionBySmash(this.pawn, this.Victim);
				if (this.pawn.InMentalState)
				{
					this.pawn.MentalState.Notify_SlaughteredAnimal();
				}
			});
			yield break;
		}

		public const int SlaughterDuration = 180;

		public static void DoExecutionBySmash(Pawn executioner, Pawn victim)
		{
			Map map = victim.Map;
			IntVec3 position = victim.Position;
			int num = Mathf.Max(GenMath.RoundRandom(victim.BodySize * 8f), 1);
			for (int i = 0; i < num; i++)
			{
				victim.health.DropBloodFilth();
			}
			BodyPartRecord bodyPartRecord = ExecuteSmashPart(victim);
			int num2 = Mathf.Clamp((int)victim.health.hediffSet.GetPartHealth(bodyPartRecord) - 1, 1, 20);
			DamageInfo damageInfo = new DamageInfo(DamageDefOf.Stab, (float)num2, 999f, -1f, executioner, bodyPartRecord, null, DamageInfo.SourceCategory.ThingOrUnknown, null);
			victim.TakeDamage(damageInfo);
			if (!victim.Dead)
			{
				victim.Kill(new DamageInfo?(damageInfo), null);
			}
		}

		private static BodyPartRecord ExecuteSmashPart(Pawn pawn)
		{
			BodyPartRecord bodyPartRecord = pawn.health.hediffSet.GetNotMissingParts(BodyPartHeight.Undefined, BodyPartDepth.Undefined, null, null).FirstOrDefault((BodyPartRecord x) => x.def == BodyPartDefOf.MorrowRim_GolemBrainCore);
			if (bodyPartRecord != null)
			{
				return bodyPartRecord;
			}
			bodyPartRecord = pawn.health.hediffSet.GetNotMissingParts(BodyPartHeight.Undefined, BodyPartDepth.Undefined, null, null).FirstOrDefault((BodyPartRecord x) => x.def == BodyPartDefOf.MorrowRim_GolemHeartCore);
			if (bodyPartRecord != null)
			{
				return bodyPartRecord;
			}
			Log.Error("No good destroy smash part found for " + pawn, false);
			return pawn.health.hediffSet.GetNotMissingParts(BodyPartHeight.Undefined, BodyPartDepth.Undefined, null, null).RandomElementByWeight((BodyPartRecord x) => x.coverageAbsWithChildren);
		}
	}
}
