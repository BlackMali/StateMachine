using System;
using System.Threading.Tasks;

namespace BlackMali.StateMachine.Tests.Login
{
    internal class UserNameState : State
	{
		public string Name => nameof(UserNameState);

		public override async Task OnEnter(IStateMachineContext context)
		{
			await base.OnEnter(context);

			Console.WriteLine("Please enter your name");
		}

		public override async Task<IState?> OnTransmitted(IStateMachineContext context, StateMachineEvent @event)
		{
			var userNameEvent = @event as EnterUserNameEvent;
			if (userNameEvent == null)
				return null;

			try
			{

				if (string.IsNullOrEmpty(userNameEvent.UserName))
					return null;

				return await context.GetState<PasswordState>();
			}
			finally
			{
				userNameEvent.IsHandled = true;
			}
		}
	}
}
