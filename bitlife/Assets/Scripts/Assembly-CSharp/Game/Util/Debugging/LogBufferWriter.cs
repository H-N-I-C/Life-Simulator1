using System;
using System.IO;
using System.Text;

namespace Game.Util.Debugging
{
	internal sealed class LogBufferWriter : IDisposable
	{
		private readonly object _gate;

		private readonly StringBuilder _pending;

		private StreamWriter _writer;

		private bool _disposed;

		public bool IsOpen => false;

		public bool Open(string currentPath, string prevPath, string header)
		{
			return false;
		}

		public void Enqueue(string line)
		{
		}

		public void Flush()
		{
		}

		public void Dispose()
		{
		}
	}
}
