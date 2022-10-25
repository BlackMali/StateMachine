using BlackMali.StateMachine.Tests.Login;
using Xunit;

namespace BlackMali.StateMachine.Tests
{
	public class StateMachineBuilderTests
	{
		[Fact]
		public void ExceptionTest()
		{
			Assert.Throws<ArgumentNullException>(() => new StateMachineBuilder(new StateMachineConfig(), null));
			Assert.Throws<ArgumentNullException>(() => new StateMachineBuilder(null, new StateProvider()));

			var builder = new StateMachineBuilder(new StateMachineConfig(), new StateProvider());

			Assert.Throws<ArgumentNullException>(() => builder.AddState<IState>(null));
		}

		[Fact]
		public void BuildTest()
		{
			var builder = new StateMachineBuilder(new StateMachineConfig(), new StateProvider())
				.UseStrictMode();

			builder.AddState(new UserNameState())
				.AddTransition<PasswordState>();

			builder.AddState(new PasswordState())
				.AddTransition<OpenAccountState>();

			builder.AddState(new OpenAccountState());

			var machine = builder.Build();
			Assert.NotNull(machine);			
		}
	}
}