using BlackMali.StateMachine.Tests.Login;
using System;
using Xunit;

namespace BlackMali.StateMachine.Tests
{
	public class StateMachineBuilderTests
	{

#pragma warning disable CS8603 // Possible null reference return.

		private object NullServiceFactory(Type type)
		{
			return null;
		}

#pragma warning restore CS8603 // Possible null reference return.

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

		[Fact]
		public void ExceptionTest()
		{
			Assert.Throws<ArgumentNullException>(() => new StateMachineBuilder(null, new StateProvider(), ServiceFactory.GetStrictServiceFactory));

			Assert.Throws<ArgumentNullException>(() => new StateMachineBuilder(new StateMachineConfig(), null, ServiceFactory.GetStrictServiceFactory));
			Assert.Throws<ArgumentNullException>(() => new StateMachineBuilder(new StateMachineConfig(), new StateProvider(), null));

			var builder = new StateMachineBuilder(new StateMachineConfig(), new StateProvider(), ServiceFactory.GetStrictServiceFactory);

			Assert.Throws<ArgumentNullException>(() => builder.AddState<IState>(null));
		}

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

		[Fact]
		public void AddStateExceptionTest()
		{
			var builder = new StateMachineBuilder(new StateMachineConfig(), new StateProvider(), ServiceFactory.GetStrictServiceFactory);

			Assert.Throws<StateMachineException>(() => builder.AddState<UserNameState>());
		}

		[Fact]
		public void BuildExceptionTest()
		{
			var builder = new StateMachineBuilder(new StateMachineConfig(), new StateProvider(), NullServiceFactory);
			builder.AddState(new UserNameState())
				.AddStartTransition()
				.AddTransition<PasswordState>();

			builder.AddState(new PasswordState())
				.AddTransition<OpenAccountState>();

			builder.AddState(new OpenAccountState());

			Assert.Throws<StateMachineException>(() => builder.Build());
		}

		[Fact]
		public void BuildTest()
		{
			var builder = new StateMachineBuilder(new StateMachineConfig(), new StateProvider(), ServiceFactory.GetStrictServiceFactory);

			builder.AddState(new UserNameState())
				.AddStartTransition()
				.AddTransition<PasswordState>();

			builder.AddState(new PasswordState())
				.AddTransition<OpenAccountState>();

			builder.AddState(new OpenAccountState());

			var machine = builder.Build();
			Assert.NotNull(machine);			
		}
	}
}