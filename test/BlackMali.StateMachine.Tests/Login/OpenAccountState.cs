using System;
using System.Threading.Tasks;

namespace BlackMali.StateMachine.Tests.Login
{
    internal class OpenAccountState : State
	{
		public override async Task<IState?> OnTransmitted(IStateMachineContext context, StateMachineEvent @event)
		{
			Console.WriteLine("Welcome!");

			await base.OnTransmitted(context, @event);

			return null;
		}	
	}
}
