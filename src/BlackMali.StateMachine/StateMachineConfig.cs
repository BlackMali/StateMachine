using System;
using System.Collections.Generic;
using System.Linq;

namespace BlackMali.StateMachine
{
	/// <summary>
	/// Implementation for the state machine configuration
	/// </summary>
	public class StateMachineConfig : IStateMachineConfig
	{
		private readonly IList<Type> _states;
		private readonly IList<StateMachineTransition> _transitions;

		/// <summary>
		/// Implementation for the state machine configuration
		/// </summary>
		public StateMachineConfig()
		{
			_states = new List<Type>();
			_transitions = new List<StateMachineTransition>();
		}

		/// <summary>
		/// Dictionary with all states
		/// </summary>
		public IReadOnlyList<Type> States { get { return _states.ToList(); } }

		/// <summary>
		/// List with all transitions
		/// </summary>
		public IReadOnlyList<StateMachineTransition> Transitions { get { return _transitions.ToList(); } }

		/// <inheritdoc/>
		public void AddState(Type type)
		{
			if (type is null)
				throw new ArgumentNullException(nameof(type));
			
			_states.Add(type);
		}

		/// <inheritdoc/>
		public void AddTransition(StateMachineTransition transition)
		{
			if (transition is null)
				throw new ArgumentNullException(nameof(transition));

			_transitions.Add(transition);
		}
	}
}