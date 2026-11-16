using System;
using System.Reflection;

namespace CandyBusiness.Confidence.Actions.ActionTypes
{
	public class DirectAction : ConfidenceAction
	{
		private class CallValue
		{
			public object Target;

			public Type Type;
		}

		public DirectAction(ConfidenceActionSchema schema)
			: base(null)
		{
		}

		public override void Execute()
		{
		}

		private bool DotTypeRecursive(object currentTarget, Type currentType, string[] parts, int partIndex)
		{
			return false;
		}

		private MethodInfo FindInvocableMethod(object target, Type type, string memberName)
		{
			return null;
		}

		private CallValue InvokeParameterlessMemberWithReturnObject(object target, Type type, MemberInfo member)
		{
			return null;
		}

		private MemberInfo FindParameterlessMemberOfName(object target, Type type, string memberName)
		{
			return null;
		}
	}
}
