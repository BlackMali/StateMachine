using System;

namespace BlackMali.StateMachine
{
	/// <inheritdoc/>
	internal class TransitionBuilder : ITransitionBuilder
	{
		private readonly IStateMachineConfig _config;
		private readonly IState? _currentState;

		/// <inheritdoc/>
		public TransitionBuilder(IStateMachineConfig config, IState? currentState)
		{
			_config = config ?? throw new ArgumentNullException(nameof(config));
			_currentState = currentState;
		}

		/// <inheritdoc/>
		public ITransitionBuilder AddStartTransition()			
		{
			if (_currentState == null)
				throw new StateMachineException("No current state set.");

			_config.Transitions.Add(new StateMachineTransition(null, _currentState.GetType()));

			return this;
		}

		/// <inheritdoc/>
		public ITransitionBuilder AddTransition<TState>() 
			where TState : IState
		{
			_config.Transitions.Add(new StateMachineTransition(_currentState?.GetType(), typeof(TState)));

			return this;
		}

		/// <inheritdoc/>
		public ITransitionBuilder AddInStateTransition()			
		{
			if (_currentState == null)
				throw new StateMachineException("In-State-Transitions not allowed for start states.");

			var currentStateType = _currentState.GetType();
			_config.Transitions.Add(new StateMachineTransition(currentStateType, currentStateType));

			return this;
		}
	}
}
