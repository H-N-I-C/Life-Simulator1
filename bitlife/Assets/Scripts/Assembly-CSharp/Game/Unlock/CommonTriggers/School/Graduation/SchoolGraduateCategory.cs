namespace Game.Unlock.CommonTriggers.School.Graduation
{
	public class SchoolGraduateCategory : UnlockTriggerCategory<GraduationContext>
	{
		private readonly string TriggerID__BackingField;

		public override string TriggerID => TriggerID__BackingField;

		protected override IUnlockTriggerWithContext<GraduationContext>[] GetAllContentUnlocked()
		{
			return null;
		}
	}
}
