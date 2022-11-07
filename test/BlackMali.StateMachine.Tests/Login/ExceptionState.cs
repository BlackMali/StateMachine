using System.Threading.Tasks;

namespace BlackMali.StateMachine.Tests.Login
{
	internal class ExceptionState : State
	{

		public override async Task<IState?> OnTransmitted(IStateMachineContext context, StateMachineEvent @event)
		{
			await Task.CompletedTask;

			if (@event.IsHandled)
				return null;
				
			throw new StateMachineException("Test");
		}
	}
}