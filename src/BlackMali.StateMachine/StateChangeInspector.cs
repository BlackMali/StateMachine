using System.Threading.Tasks;

namespace BlackMali.StateMachine
{
	/// <summary>
	/// Inspector without state change validation
	/// </summary>
	public class StateChangeInspector : IStateChangeInspector
	{
		/// <summary>
		/// Inspector without state change validation
		/// </summary>
		public StateChangeInspector()
		{ }

		/// <summary>
		/// Doesn't do any validations
		/// </summary>
		/// <param name="context">The state machine context</param>
		/// <param name="nextState">The next state</param>
		public async Task Inspect(IStateMachineContext context, IState nextState)
		{
			await Task.CompletedTask;
		}
	}
}