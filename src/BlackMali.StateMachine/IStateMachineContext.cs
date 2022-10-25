using System.Threading.Tasks;

namespace BlackMali.StateMachine
{
	/// <summary>
	/// State machine context
	/// </summary>
	public interface IStateMachineContext
	{
		/// <summary>
		/// The state machine configuration
		/// </summary>
		IStateMachineConfig Config { get; }
		
		/// <summary>
		/// Last state
		/// </summary>
		IState? LastState { get; }
		
		/// <summary>
		/// Current state
		/// </summary>
		IState? State { get; }

		/// <summary>
		/// Next state
		/// </summary>
		IState? NextState { get; }

		/// <summary>
		/// Gets a state by the type
		/// </summary>
		/// <returns>The state</returns>
		/// <exception cref="StateMachineException">If no state was found</exception>
		Task<IState> GetState<TState>();
	}
}