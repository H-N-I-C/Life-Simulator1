using System;
using System.Collections.Generic;
using CandyBusiness.Confidence.Actions.ActionTypes;

namespace CandyBusiness.Confidence.Actions
{
	public class ConfidenceActionMapping
	{
		private static readonly Dictionary<string, Action<MappedAction>> ActionMappings;

		public static void InvokeAction(string mappingKey, MappedAction mappedAction)
		{
		}
	}
}
