using System;
using System.Runtime.Serialization;

namespace BlackMali.StateMachine
{
	/// <summary>
	/// State machine exception
	/// </summary>
	[Serializable]
	public class StateMachineException : Exception
	{
		/// <summary>
		/// State machine exception
		/// </summary>
		protected StateMachineException()
		{ }

		/// <summary>
		/// State machine exception
		/// </summary>
		public StateMachineException(string message)
			: base(message)
		{ }

		/// <summary>
		/// State machine exception
		/// </summary>
		public StateMachineException(string message, Exception innerException)
			:base(message, innerException)
		{ }

		/// <summary>
		/// State machine exception
		/// </summary>
		protected StateMachineException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{ }
	}
}