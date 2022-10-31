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
		public IReadOnlyList<Type> States { get; }

		/// <summary>
		/// List with all transitions
		/// </summary>
		public IReadOnlyList<StateMachineTransition> Transitions { get; }

		/// <summary>
		/// Adds a state
		/// </summary>
		/// <param name="type">The type of state</param>
		public void AddState(Type type);

		/// <summary>
		/// Adds a transition
		/// </summary>
		/// <param name="transition">The transition</param>
		public void AddTransition(StateMachineTransition transition);
	}
}
