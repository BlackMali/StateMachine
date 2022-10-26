using System;
using System.Threading.Tasks;

namespace BlackMali.StateMachine.Tests.Login
{
    internal class PasswordState : State
	{
		public string Name => nameof(PasswordState);

		public override async Task OnEnter(IStateMachineContext context)
		{
			await base.OnEnter(context);

			Console.WriteLine("Please enter your password");			
		}

		public override async Task<IState?> OnTransmitted(IStateMachineContext context, StateMachineEvent @event)
		{
			var userNameEvent = @event as EnterPasswordNameEvent;
			if (userNameEvent == null)
				return null;

			if (string.IsNullOrEmpty(userNameEvent.Password))
				return null;

			return await context.GetState<OpenAccountState>();
		}		
	}
}
