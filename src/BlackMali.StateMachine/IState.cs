using System.Threading.Tasks;

namespace BlackMali.StateMachine
{
	/// <summary>
	/// Interface for States
	/// </summary>
	public interface IState
	{
		/// <summary>
		/// Method is called before the state change
		/// </summary>
		/// <param name="context">The state machine context</param>
		Task OnEnter(IStateMachineContext context);

		/// <summary>
		/// Method is called after the state change
		/// </summary>
		/// <param name="context">The state machine context</param>
		/// <param name="event">The state machine event</param>
		/// <returns>The next state (optional)</returns>
		Task<IState?> OnTransmitted(IStateMachineContext context, StateMachineEvent @event);

		/// <summary>
		/// Method is called before the next state change
		/// </summary>
		/// <param name="context">The state machine context</param>
		Task OnExit(IStateMachineContext context);

	}
}