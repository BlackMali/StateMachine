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
		private readonly StateMachineServiceFactory _serviceFactory;

		/// <summary>
		/// Builder for state machines
		/// </summary>
		public StateMachineBuilder(IStateMachineConfig machineConfig, IStateProvider stateProvider, StateMachineServiceFactory serviceFactory)
		{
			_machineConfig = machineConfig ?? throw new ArgumentNullException(nameof(machineConfig));
			_stateProvider = stateProvider ?? throw new ArgumentNullException(nameof(stateProvider));
			_serviceFactory = serviceFactory ?? throw new ArgumentNullException(nameof(serviceFactory));
		}

		/// <inheritdoc/>
		public ITransitionBuilder AddState<TState>() where TState : class, IState
		{
			var type = typeof(TState);
			var state = _serviceFactory(type) as TState;
			if (state == null)
				throw new StateMachineException($"The Type [{type}] could not be resolved.");

			return AddState(state);
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
		public IStateMachine Build()
		{
			var inspector = _serviceFactory(typeof(IStateChangeInspector)) as IStateChangeInspector;
			if (inspector == null)
				throw new StateMachineException($"[{nameof(IStateChangeInspector)} could not be resolved.");

			var context = new StateMachineContext(_machineConfig, _stateProvider);

			return new StateMachine(context, inspector);
		}		
	}
}