using System.Collections;

namespace Game
{
	public class DataCultCommunardConversation
	{
		private int Audience__BackingField;

		private CULT_PHILOSOPHY Philosophy__BackingField;

		private string Conversation__BackingField;

		private int ControversyScore__BackingField;

		public int Audience
		{
			get
			{
				return Audience__BackingField;
			}
			set
			{
				Audience__BackingField = value;
			}
		}

		public CULT_PHILOSOPHY Philosophy
		{
			get
			{
				return Philosophy__BackingField;
			}
			set
			{
				Philosophy__BackingField = value;
			}
		}

		public string Conversation
		{
			get
			{
				return Conversation__BackingField;
			}
			set
			{
				Conversation__BackingField = value;
			}
		}

		public int ControversyScore
		{
			get
			{
				return ControversyScore__BackingField;
			}
			set
			{
				ControversyScore__BackingField = value;
			}
		}

		public ArrayList ConversationArrayForAudiencePersonAndOrGender(SimPerson person, int gender)
		{
			return null;
		}

		private string ConversationStringForAudiencePersonAndOrGenderAndIsForNarrative(SimPerson person, int gender, bool narrative)
		{
			return null;
		}
	}
}
