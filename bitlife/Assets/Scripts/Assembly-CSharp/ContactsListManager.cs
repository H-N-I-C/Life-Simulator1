using System.Collections;
using UnityEngine;
using VoxelBusters.CoreLibrary;
using VoxelBusters.EssentialKit;

public class ContactsListManager : MonoBehaviour
{
	public static ContactsListManager SharedContactsListManager;

	public ArrayList FirstNames;

	public ArrayList LastNames;

	private AddressBookContactsAccessStatus status;

	private ReadContactsOptions options;

	private IAddressBookContact[] allContacts;

	private void Start()
	{
	}

	public void ReadContacts()
	{
	}

	private void OnReadContactsFinish(AddressBookReadContactsResult result, Error error)
	{
	}
}
