using System;

namespace BlackMali.StateMachine
{
	/// <summary>
	/// Base state machine event
	/// </summary>
	public class StateMachineEvent
	{
		/// <summary>
		/// Base state machine event
		/// </summary>
		public StateMachineEvent()
			: this (DateTimeOffset.UtcNow)
		{ }

		/// <summary>
		/// Base state machine event
		/// </summary>
		/// <param name="timestamp">Timestamp when the event occurred</param>
		public StateMachineEvent(DateTimeOffset timestamp)
		{
			Timestamp = timestamp;
			IsHandled = false;
		}

		/// <summary>
		/// Timestamp when the event occurred 
		/// </summary>
		public DateTimeOffset Timestamp { get; protected set; }

		/// <summary>
		/// Indicates whether the event has already been processed
		/// </summary>
		public bool IsHandled { get; set; }

	}
}