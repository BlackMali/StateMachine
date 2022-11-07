using System;

namespace BlackMali.StateMachine
{
	/// <summary>
	/// EventArgs for state change errors
	/// </summary>
	public class StateErrorEventArgs : EventArgs
	{
		/// <summary>
		/// EventArgs for state change errors
		/// </summary>
		public StateErrorEventArgs(Type? nextStateType, Exception exception)
		{
			NextStateType = nextStateType;
			Exception = exception;
		}

		/// <summary>
		/// Type of next state
		/// </summary>
		public Type? NextStateType { get; }

		/// <summary>
		/// Exception
		/// </summary>
		public Exception Exception { get; }
	}
}
