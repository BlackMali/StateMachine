using System.Threading.Tasks;

namespace BlackMali.StateMachine
{
	/// <summary>
	/// Checks whether a status change is allowed
	/// </summary>
	public interface IStateChangeInspector
	{
		/// <summary>
		/// Checks whether a status change is allowed
		/// </summary>
		/// <param name="context">The state machine context</param>
		/// <param name="nextState">The next state</param>
		Task Inspect(IStateMachineContext context, IState nextState);

	}
}