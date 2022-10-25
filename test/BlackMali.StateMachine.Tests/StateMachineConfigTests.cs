using BlackMali.StateMachine.Tests.Login;
using Xunit;

namespace BlackMali.StateMachine.Tests
{
	public class StateMachineConfigTests
	{
		[Fact]
		public void ConfigTest()
		{
			var config = new StateMachineConfig();
			
			config.States.Add(typeof(UserNameState));
			config.States.Add(typeof(PasswordState));

			config.Transitions.Add(new StateMachineTransition(null, typeof(UserNameState)));
			config.Transitions.Add(new StateMachineTransition(typeof(UserNameState), typeof(PasswordState)));
			config.Transitions.Add(new StateMachineTransition(typeof(PasswordState), typeof(UserNameState)));

			Assert.Equal(2, config.States.Count);
			Assert.Equal(3, config.Transitions.Count);			
		}
	}
}