using System;
using System.Collections;
using System.Collections.Generic;

public static class UnlockTriggers
{
	public static class Jobs
	{
		public static UnlockTriggerCategory<JobContext> StartPolitician => null;

		public static UnlockTriggerCategory<JobContext> StartActor => null;

		public static UnlockTriggerCategory<PartTimeJobContext> StartPartTimeWork => null;
	}

	public static class Vampire
	{
		public static UnlockTriggerCategory<BecomeVampireContext> BecomeAVampire => null;

		public static UnlockTriggerCategory<DrainBloodContext> DrainedBlood => null;

		public static UnlockTriggerCategory<HypnotizeContext> Hypnotize => null;
	}

	public static class Cult
	{
		public static UnlockTriggerCategory<CultContext> StartCult => null;
	}

	public static class Business
	{
		public static UnlockTriggerCategory<BusinessPurchaseContext> PurchaseExistingBusiness => null;
	}

	public static class Crime
	{
		public static UnlockTriggerCategory<MurderContext> Murder => null;

		public static UnlockTriggerCategory<MischiefContext> Mischief => null;
	}

	public static class Shopping
	{
		public static UnlockTriggerCategory<CarPurchaseContext> CarPurchase => null;

		public static UnlockTriggerCategory<PetPurchaseContext> PetPurchase => null;
	}

	public static class MindAndBody
	{
		public static UnlockTriggerCategory<MeditationContext> Meditate => null;

		public static UnlockTriggerCategory<AlternativeMedicineContext> VisitAlternativePhysician => null;

		public static UnlockTriggerCategory<WalkContext> Walk => null;

		public static UnlockTriggerCategory<SpaceCampContext> VisitSpaceCamp => null;

		public static UnlockTriggerCategory<MartialArtContext> TakeMartialArtLesson => null;
	}

	public static class School
	{
		public static UnlockTriggerCategory<GraduationContext> Graduate => null;

		public static UnlockTriggerCategory<JoinSchoolActivityContext> JoinSchoolActivity => null;

		public static UnlockTriggerCategory<ClassmateMischiefContext> MessWith => null;
	}

	public static class FightClub
	{
		public static UnlockTriggerCategory<FightClubMoveContext> BecomeAStriker;

		public static UnlockTriggerCategory<FightClubMoveContext> BecomeASubmissionArtist;

		public static UnlockTriggerCategory<FightClubMoveContext> BecomeAWrestler;

		public static UnlockTriggerCategory<FightClubMoveContext> TrainWithCoach;

		public static UnlockTriggerCategory<SkillShopContext> SkillShop => null;
	}

	private sealed class _003C_003CGetAllTriggers_003Eg__FlattenList_007C15_0_003Ed : IEnumerable<IUnlockTriggerBase>, IEnumerable, IEnumerator<IUnlockTriggerBase>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private IUnlockTriggerBase _003C_003E2__current;

		private int _003C_003El__initialThreadId;

		private IEnumerable<IUnlockTriggerBase> dataSource;

		public IEnumerable<IUnlockTriggerBase> _003C_003E3__dataSource;

		private IEnumerator<IUnlockTriggerBase> _003C_003E7__wrap1;

		private IEnumerator<IUnlockTriggerBase> _003C_003E7__wrap2;

		IUnlockTriggerBase IEnumerator<IUnlockTriggerBase>.Current => null;

		object IEnumerator.Current => null;

		public _003C_003CGetAllTriggers_003Eg__FlattenList_007C15_0_003Ed(int _003C_003E1__state)
		{
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private void _003C_003Em__Finally1()
		{
		}

		private void _003C_003Em__Finally2()
		{
		}

		void IEnumerator.Reset()
		{
		}

		IEnumerator<IUnlockTriggerBase> IEnumerable<IUnlockTriggerBase>.GetEnumerator()
		{
			return null;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return null;
		}
	}

	private sealed class _003CGetAllTriggers_003Ed__15 : IEnumerable<IUnlockTriggerBase>, IEnumerable, IEnumerator<IUnlockTriggerBase>, IEnumerator, IDisposable
	{
		private int _003C_003E1__state;

		private IUnlockTriggerBase _003C_003E2__current;

		private int _003C_003El__initialThreadId;

		private IEnumerator<IUnlockTriggerBase> _003C_003E7__wrap1;

		IUnlockTriggerBase IEnumerator<IUnlockTriggerBase>.Current => null;

		object IEnumerator.Current => null;

		public _003CGetAllTriggers_003Ed__15(int _003C_003E1__state)
		{
		}

		void IDisposable.Dispose()
		{
		}

		private bool MoveNext()
		{
			return false;
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		private void _003C_003Em__Finally1()
		{
		}

		void IEnumerator.Reset()
		{
		}

		IEnumerator<IUnlockTriggerBase> IEnumerable<IUnlockTriggerBase>.GetEnumerator()
		{
			return null;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return null;
		}
	}

	public static UnlockTriggerCategory<EmigrateContext> Emigrate => null;

	public static UnlockTriggerCategory<MemoryTestContext> MemoryTest => null;

	public static UnlockTriggerCategory<WitchDoctorContext> WitchDoctor => null;

	public static IEnumerable<IUnlockTriggerBase> GetAllTriggers()
	{
		return null;
	}

	private static IList<T> GetAllUnlockFieldsAndProperties<T>(Type classType)
	{
		return null;
	}

	internal static IEnumerable<IUnlockTriggerBase> _003CGetAllTriggers_003Eg__FlattenList_007C15_0(IEnumerable<IUnlockTriggerBase> dataSource)
	{
		return null;
	}
}
