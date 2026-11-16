using System;
using System.Collections;
using System.Collections.Generic;
using Genesis.Entities;

[Serializable]
public class GodModePersonConfig
{
	[Serializable]
	private sealed class _003C_003Ec
	{
		public static readonly _003C_003Ec _003C_003E9;

		public static Func<string, string> _003C_003E9__59_0;

		internal string _003CSetupAttributeOrderArrayAlphabetically_003Eb__59_0(string str)
		{
			return null;
		}
	}

	private const int DynamicAttributeIdBase = 1000;

	private int Type__BackingField;

	private SimPerson editPerson;

	private string FirstName__BackingField;

	private string LastName__BackingField;

	private PersonEmojiScript EmojiScript__BackingField;

	private Dictionary<string, string> AttributeDictionary__BackingField;

	private Dictionary<int, string> _dynamicAttributeKeys;

	private Dictionary<int, string> _dynamicAttributeLabels;

	private Dictionary<int, string> _dynamicAttributeEmojis;

	private ArrayList _AttributeOrderArray__BackingField;

	private int RoyalRankIndexZI__BackingField;

	private bool Edited__BackingField;

	private bool EditedAttributes__BackingField;

	private int SpecialTalent__BackingField;

	private double PiggyBankAmount__BackingField;

	public int Type
	{
		get
		{
			return Type__BackingField;
		}
		set
		{
			Type__BackingField = value;
		}
	}

	public SimPerson EditPerson
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public string FirstName
	{
		get
		{
			return FirstName__BackingField;
		}
		set
		{
			FirstName__BackingField = value;
		}
	}

	public string LastName
	{
		get
		{
			return LastName__BackingField;
		}
		set
		{
			LastName__BackingField = value;
		}
	}

	public PersonEmojiScript EmojiScript
	{
		get
		{
			return EmojiScript__BackingField;
		}
		set
		{
			EmojiScript__BackingField = value;
		}
	}

	private Dictionary<string, string> AttributeDictionary
	{
		get
		{
			return AttributeDictionary__BackingField;
		}
		set
		{
			AttributeDictionary__BackingField = value;
		}
	}

	private ArrayList _AttributeOrderArray
	{
		get
		{
			return _AttributeOrderArray__BackingField;
		}
		set
		{
			_AttributeOrderArray__BackingField = value;
		}
	}

	public ArrayList AttributeOrderArray
	{
		get
		{
			return null;
		}
		set
		{
		}
	}

	public int RoyalRankIndexZI
	{
		get
		{
			return RoyalRankIndexZI__BackingField;
		}
		set
		{
			RoyalRankIndexZI__BackingField = value;
		}
	}

	public bool Edited
	{
		get
		{
			return Edited__BackingField;
		}
		set
		{
			Edited__BackingField = value;
		}
	}

	public bool EditedAttributes
	{
		get
		{
			return EditedAttributes__BackingField;
		}
		set
		{
			EditedAttributes__BackingField = value;
		}
	}

	public int SpecialTalent
	{
		get
		{
			return SpecialTalent__BackingField;
		}
		set
		{
			SpecialTalent__BackingField = value;
		}
	}

	public double PiggyBankAmount
	{
		get
		{
			return PiggyBankAmount__BackingField;
		}
		set
		{
			PiggyBankAmount__BackingField = value;
		}
	}

	public GodModePersonConfig(string theFirstName, string theLastName)
	{
	}

	public GodModePersonConfig(SimPerson thePerson)
	{
	}

	private void AddDynamicEntityAttributes(SimEntity entity, GenesisEntityDefinition definition)
	{
	}

	private static void ReplaceHealthWithEssence(Dictionary<string, string> attributeDictionary, float essence)
	{
	}

	public void SetupAttributeOrderArrayAlphabetically()
	{
	}

	public string RelationshipStatusStringForEditPersonInPointOfView(int pointOfView)
	{
		return null;
	}

	private string GetMovieActorRoleString()
	{
		return null;
	}

	private static string NonCurrentActorRelationship(GenesisEntityDefinition definition)
	{
		return null;
	}

	public string StringForEditMainBody()
	{
		return null;
	}

	public string StringForEditNameBody()
	{
		return null;
	}

	public string StringForEditAppearanceBody()
	{
		return null;
	}

	public string StringForEditAttributesBody()
	{
		return null;
	}

	public virtual void SetAttributeValueForAttribute(int value, int attribute)
	{
	}

	public virtual int GetValueForAttribute(int attribute)
	{
		return 0;
	}

	public static int ConvertToGameValueFromSliderValue(int sliderValue)
	{
		return 0;
	}

	public static int ConvertToSliderValueFromGameValue(int gameValue)
	{
		return 0;
	}

	protected void ApplyNameChanges()
	{
	}

	protected void ApplyEmojiChanges()
	{
	}

	public virtual void SaveChangesAfterEditingPerson()
	{
	}

	public virtual string EmojiForGodModeAttribute(int attribute)
	{
		return null;
	}

	public virtual string GetNameForAttribute(int attribute)
	{
		return null;
	}

	public int GetAttributeForName(string name)
	{
		return 0;
	}

	public bool IsEditPersonEligibleForSpecialTalentConfig()
	{
		return false;
	}

	public bool IsEditPersonEligibleForSpecialTalentConfig(SimPerson personBeingEdited)
	{
		return false;
	}

	private bool _003CRelationshipStatusStringForEditPersonInPointOfView_003Eb__60_0(SimDealerStreetPerson x)
	{
		return false;
	}

	private bool _003CRelationshipStatusStringForEditPersonInPointOfView_003Eb__60_1(SimDealerStreetPerson x)
	{
		return false;
	}
}
