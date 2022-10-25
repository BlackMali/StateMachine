using System;
using System.Threading.Tasks;

namespace BlackMali.StateMachine
{
	/// <summary>
	/// Provider for state instances
	/// </summary>
	public interface IStateProvider
	{
		/// <summary>
		/// Register a state instance
		/// </summary>
		/// <typeparam name="TState">Type of state</typeparam>
		/// <param name="state">State instance</param>
		void SetState<TState>(TState state)
			where TState : IState;

		/// <summary>
		/// Gets a state instance
		/// </summary>
		/// <param name="type">Type of state instance</param>
		/// <returns>A state instance</returns>
		/// <exception cref="StateMachineException">When no instance found.</exception>
		Task<IState> GetState(Type type);

	}
}