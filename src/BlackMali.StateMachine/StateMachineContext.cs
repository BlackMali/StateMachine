using System.Threading.Tasks;

namespace BlackMali.StateMachine
{
	/// <summary>
	/// State machine context
	/// </summary>
	internal class StateMachineContext : IStateMachineContext
	{
		private readonly IStateProvider _stateProvider;

		/// <summary>
		/// State machine context
		/// </summary>
		public StateMachineContext(IStateMachineConfig config, IStateProvider stateProvider)
		{
			Config = config;
			_stateProvider = stateProvider;
		}

		/// <inheritdoc/>
		public IStateMachineConfig Config { get; set; }

		/// <inheritdoc/>
		public IState? LastState { get; set; }

		/// <inheritdoc/>
		public IState? State { get; set; }

		/// <inheritdoc/>
		public IState? NextState { get; set; }		
		
		/// <inheritdoc/>
		public async Task<IState> GetState<TState>()
		{
			var type =typeof(TState);
			var state = await _stateProvider.GetState(type);
			if (state == null)
				throw new StateMachineException($"No state for type [{type}] found.");

			return state;
		}
	}
}
