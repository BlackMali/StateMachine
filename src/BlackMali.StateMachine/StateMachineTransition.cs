using System;

namespace BlackMali.StateMachine
{
	/// <summary>
	/// State machine transition
	/// </summary>
	public class StateMachineTransition
	{
		/// <summary>
		/// State machine transition
		/// </summary>
		public StateMachineTransition(Type? fromState, Type? toState)
		{
			FromState = fromState;
			ToState = toState;
		}

		/// <summary>
		/// Type of first state
		/// </summary>
		public Type? FromState { get; }

		/// <summary>
		/// Type of second state
		/// </summary>
		public Type? ToState { get; }
	}
}