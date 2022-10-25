namespace BlackMali.StateMachine
{
	/// <summary>
	/// Builder for state machines
	/// </summary>
	public interface IStateMachineBuilder
	{
		/// <summary>
		/// Adds a state
		/// </summary>
		/// <param name="state">The state</param>
		/// <returns>The state machine builder for fluent pattern</returns>
		ITransitionBuilder AddState<TState>(TState state)
			where TState : IState;

		/// <summary>
		/// Set the strict mode
		/// </summary>
		/// <returns>The state machine builder for fluent pattern</returns>
		IStateMachineBuilder UseStrictMode();		

		/// <summary>
		/// Creates a state machine
		/// </summary>
		/// <returns>The state machine</returns>
		IStateMachine Build();
	}
}
