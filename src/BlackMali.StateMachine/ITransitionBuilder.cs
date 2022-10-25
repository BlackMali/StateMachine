namespace BlackMali.StateMachine
{
	/// <summary>
	/// Builder for state machine transitions
	/// </summary>
	public interface ITransitionBuilder
	{
		/// <summary>
		/// Adds a start transition (for strict mode)
		/// </summary>
		/// <returns>The transition builder for fluent pattern</returns>
		ITransitionBuilder AddStartTransition();

		/// <summary>
		/// Adds a transition (for strict mode)
		/// </summary>
		/// <returns>The transition builder for fluent pattern</returns>
		ITransitionBuilder AddTransition<TState>() where TState : IState;

		/// <summary>
		/// Adds a in state transition (for strict mode)
		/// </summary>
		/// <returns>The transition builder for fluent pattern</returns>
		ITransitionBuilder AddInStateTransition();

	}
}