using System;
using System.Collections.Generic;

namespace BlackMali.StateMachine
{
	/// <summary>
	/// Configuration for state machines
	/// </summary>
	public interface IStateMachineConfig
	{
		/// <summary>
		/// Dictionary with all states
		/// </summary>
		public IList<Type> States { get; }

		/// <summary>
		/// List with all transitions
		/// </summary>
		public IList<StateMachineTransition> Transitions { get; }
	}
}