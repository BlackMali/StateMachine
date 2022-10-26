using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace BlackMali.StateMachine
{
	/// <inheritdoc/>
	public class StateProvider : IStateProvider
	{
		private readonly ConcurrentDictionary<Type, IState> _states;

		/// <inheritdoc/>
		public StateProvider()
		{
			_states = new ConcurrentDictionary<Type, IState>();
		}

		/// <inheritdoc/>
		public void SetState<TState>(TState state)
			where TState : IState
		{
			if (state is null)
				throw new ArgumentNullException(nameof(state));

			if (!_states.TryAdd(state.GetType(), state))
				throw new StateMachineException($"State couldn't inserted");			
		}

		/// <inheritdoc/>
		public async Task<IState> GetState(Type type)
		{
			if (type is null)
				throw new ArgumentNullException(nameof(type));

			IState? state;
			if (!_states.TryGetValue(type, out state))
				throw new StateMachineException($"No state for Type [{type}] found.");

			if (state == null)
				throw new StateMachineException($"No instance for Type [{type}] found.");

			await Task.CompletedTask;

			return state;
		}
	}
}