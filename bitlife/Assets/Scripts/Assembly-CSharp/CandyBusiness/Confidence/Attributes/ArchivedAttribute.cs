using System;

namespace CandyBusiness.Confidence.Attributes
{
	public sealed class ArchivedAttribute : Attribute
	{
		private readonly string Reason__BackingField;

		private string Date__BackingField;

		private string Ticket__BackingField;

		public string Reason => Reason__BackingField;

		public string Date
		{
			get
			{
				return Date__BackingField;
			}
			set
			{
				Date__BackingField = value;
			}
		}

		public string Ticket
		{
			get
			{
				return Ticket__BackingField;
			}
			set
			{
				Ticket__BackingField = value;
			}
		}

		public ArchivedAttribute(string reason = null)
		{
		}

		public override string ToString()
		{
			return null;
		}
	}
}
