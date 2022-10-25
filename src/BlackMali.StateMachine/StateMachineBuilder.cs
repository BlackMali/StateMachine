using System;

namespace BlackMali.StateMachine
{
	/// <summary>
	/// Builder for state machines
	/// </summary>
	public class StateMachineBuilder : IStateMachineBuilder
	{
		private readonly IStateMachineConfig _machineConfig;
		private readonly IStateProvider _stateProvider;
		private bool _useStrictMode;

		/// <summary>
		/// Builder for state machines
		/// </summary>
		public StateMachineBuilder()
			: this (new StateMachineConfig(), new StateProvider())
		{ }

		/// <summary>
		/// Builder for state machines
		/// </summary>
		public StateMachineBuilder(IStateMachineConfig machineConfig, IStateProvider stateProvider)
		{
			_machineConfig = machineConfig ?? throw new ArgumentNullException(nameof(machineConfig));
			_stateProvider = stateProvider ?? throw new ArgumentNullException(nameof(stateProvider));
		}

		/// <inheritdoc/>
		public ITransitionBuilder AddState<TState>(TState state)
			where TState : IState
		{
			if (state is null)
				throw new ArgumentNullException(nameof(state));

			_machineConfig.States.Add(state.GetType());

			_stateProvider.SetState(state);

			return new TransitionBuilder(_machineConfig, state);
		}

		/// <inheritdoc/>
		public IStateMachineBuilder UseStrictMode()
		{
			_useStrictMode = true;

			return this;
		}

		/// <inheritdoc/>
		public IStateMachine Build()
		{
			IStateChangeInspector inspector = new StateChangeInspector();
			if (_useStrictMode)
				inspector = new StrictStateChangeInspector();

			var context = new StateMachineContext(_machineConfig, _stateProvider);

			return new StateMachine(context, inspector);
		}		
	}
}