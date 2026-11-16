using System;
using UnityEngine;

namespace Game.Util.Debugging
{
	[Serializable]
	public class LoggerConfig
	{
		public bool enabled;

		public bool overrideColor;

		public Color color;

		public bool logToFile;

		public string fileName;
	}
}
