namespace BlackMali.StateMachine.Tests.Login
{
	internal class EnterUserNameEvent : StateMachineEvent
	{
		public EnterUserNameEvent(string userName)
		{
			UserName = userName;	
		}

		public string UserName { get; }

	}
}