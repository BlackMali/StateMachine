namespace BlackMali.StateMachine.Tests.Login
{
	internal class EnterPasswordNameEvent : StateMachineEvent
	{
		public EnterPasswordNameEvent(string password)
		{
			Password = password;
		}

		public string Password { get; }
	}
}
