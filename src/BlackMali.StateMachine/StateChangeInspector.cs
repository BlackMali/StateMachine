using System.Threading.Tasks;

namespace BlackMali.StateMachine
{
	internal class StateChangeInspector : IStateChangeInspector
	{
		public async Task Inspect(IStateMachineContext context, IState nextState)
		{
			await Task.CompletedTask;
		}
	}
}