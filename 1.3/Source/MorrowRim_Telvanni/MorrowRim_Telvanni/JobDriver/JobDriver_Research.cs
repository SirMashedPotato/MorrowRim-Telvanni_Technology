using System;
using System.Collections.Generic;
using Verse;
using Verse.AI;
using RimWorld;

namespace MorrowRim_Telvanni
{
	// Token: 0x02000684 RID: 1668
	public class JobDriver_Research : JobDriver
	{
		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x06002DDD RID: 11741 RVA: 0x001040CB File Offset: 0x001022CB
		private ResearchProjectDef Project
		{
			get
			{
				return Find.ResearchManager.currentProj;
			}
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06002DDE RID: 11742 RVA: 0x001040D7 File Offset: 0x001022D7
		private Building_ResearchBench ResearchBench
		{
			get
			{
				return (Building_ResearchBench)base.TargetThingA;
			}
		}

		// Token: 0x06002DDF RID: 11743 RVA: 0x001040E4 File Offset: 0x001022E4
		public override bool TryMakePreToilReservations(bool errorOnFailed)
		{
			return this.pawn.Reserve(this.ResearchBench, this.job, 1, -1, null, errorOnFailed);
		}

		// Token: 0x06002DE0 RID: 11744 RVA: 0x00104106 File Offset: 0x00102306
		protected override IEnumerable<Toil> MakeNewToils()
		{
			this.FailOnDespawnedNullOrForbidden(TargetIndex.A);
			yield return Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.InteractionCell);
			Toil research = new Toil();
			research.tickAction = delegate ()
			{
				Pawn actor = research.actor;
				float num = GolemUtility.GetWork_Intellectual(actor);
				Find.ResearchManager.ResearchPerformed(num, actor);
			};
			research.FailOn(() => this.Project == null);
			research.FailOn(() => !this.Project.CanBeResearchedAt(this.ResearchBench, false));
			research.FailOnCannotTouch(TargetIndex.A, PathEndMode.InteractionCell);
			research.WithEffect(EffecterDefOf.Research, TargetIndex.A);
			research.WithProgressBar(TargetIndex.A, delegate
			{
				ResearchProjectDef project = this.Project;
				if (project == null)
				{
					return 0f;
				}
				return project.ProgressPercent;
			}, false, -0.5f);
			research.defaultCompleteMode = ToilCompleteMode.Delay;
			research.defaultDuration = JobEndInterval;
			//research.activeSkill = (() => SkillDefOf.Intellectual);
			yield return research;
			yield return Toils_General.Wait(2, TargetIndex.None);
			yield break;
		}

		// Token: 0x04001A8E RID: 6798
		private const int JobEndInterval = 4000;
	}
}
