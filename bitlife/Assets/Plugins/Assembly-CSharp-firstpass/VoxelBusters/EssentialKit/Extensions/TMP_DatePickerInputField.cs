using System;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace VoxelBusters.EssentialKit.Extensions
{
	public class TMP_DatePickerInputField : Selectable, IPointerClickHandler, IEventSystemHandler, ISubmitHandler, ICancelHandler
	{
		private TextMeshProUGUI m_placeholder;

		private TextMeshProUGUI m_text;

		private DateTimeKind m_kind;

		private DatePickerMode m_mode;

		private string m_displayFormat;

		private UnityEvent m_onValueChange;

		private DateTime? m_minimumDate;

		private DateTime? m_maximumDate;

		private DateTime? m_initialDate;

		private DateTime? m_date;

		private bool m_isSelected;

		public DateTimeKind Kind
		{
			get
			{
				return DateTimeKind.Unspecified;
			}
			set
			{
			}
		}

		public DatePickerMode Mode
		{
			get
			{
				return DatePickerMode.Date;
			}
			set
			{
			}
		}

		public string DisplayFormat
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public DateTime? MinimumDate
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public DateTime? MaximumDate
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public DateTime? InitialDate
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public DateTime? Date
		{
			get
			{
				return null;
			}
			set
			{
			}
		}

		public UnityEvent OnValueChange => null;

		protected override void Awake()
		{
		}

		public void Show()
		{
		}

		private void SetText(string value)
		{
		}

		public virtual void OnPointerClick(PointerEventData eventData)
		{
		}

		public virtual void OnSubmit(BaseEventData eventData)
		{
		}

		public virtual void OnCancel(BaseEventData eventData)
		{
		}

		private void OnDatePickerClose(DatePickerResult data)
		{
		}

		private void OnDatePickerValueChange(DateTime? dateTime)
		{
		}
	}
}
