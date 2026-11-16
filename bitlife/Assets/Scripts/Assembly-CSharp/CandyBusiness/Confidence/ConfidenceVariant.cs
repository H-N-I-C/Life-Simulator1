using System;
using CandyBusiness.Confidence.Attributes;
using CandyBusiness.Confidence.Flags;

namespace CandyBusiness.Confidence
{
	public abstract class ConfidenceVariant : IDisposable
	{
		private Action _onUpdated;

		private bool ResolvedStatus__BackingField;

		public bool ResolvedStatus
		{
			get
			{
				return ResolvedStatus__BackingField;
			}
			private set
			{
				ResolvedStatus__BackingField = value;
			}
		}

		public abstract ConfidenceFlag Flag { get; }

		public bool IsArchived => false;

		public ArchivedAttribute ArchiveInfo => null;

		public event Action OnUpdated
		{
			add
			{
			}
			remove
			{
			}
		}

		public virtual void Update()
		{
		}

		public void Dispose()
		{
		}
	}
}
