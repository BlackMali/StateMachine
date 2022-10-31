using BlackMali.StateMachine.Tests.Login;
using System;
using Xunit;

namespace BlackMali.StateMachine.Tests
{
	public class StateMachineConfigTests
	{
		[Fact]
		public void ConfigTest()
		{
			var config = new StateMachineConfig();
			
			config.AddState(typeof(UserNameState));
			config.AddState(typeof(PasswordState));

			config.AddTransition(new StateMachineTransition(null, typeof(UserNameState)));
			config.AddTransition(new StateMachineTransition(typeof(UserNameState), typeof(PasswordState)));
			config.AddTransition(new StateMachineTransition(typeof(PasswordState), typeof(UserNameState)));

			Assert.Equal(2, config.States.Count);
			Assert.Equal(3, config.Transitions.Count);

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

			Assert.Throws<ArgumentNullException>(() => config.AddState(null));
			Assert.Throws<ArgumentNullException>(() => config.AddTransition(null));

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

		}
	}
}