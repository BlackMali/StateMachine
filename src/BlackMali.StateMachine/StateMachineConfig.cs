using System;
using System.Collections.Generic;

namespace BlackMali.StateMachine
{
	/// <summary>
	/// Implementation for the state machine configuration
	/// </summary>
	public class StateMachineConfig : IStateMachineConfig
	{
		/// <summary>
		/// Implementation for the state machine configuration
		/// </summary>
		public StateMachineConfig()
		{
			States = new List<Type>();
			Transitions = new List<StateMachineTransition>();
		}

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