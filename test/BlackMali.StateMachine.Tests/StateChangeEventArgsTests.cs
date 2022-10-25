using Autofac.Extras.Moq;
using BlackMali.StateMachine.Tests.Login;
using Xunit;

namespace BlackMali.StateMachine.Tests
{
	public class StateChangeEventArgsTests : IDisposable
	{
		private AutoMock _container;

		public StateChangeEventArgsTests()
		{
			_container = AutoMock.GetLoose();
		}

		[Fact]
		public void PropertiesTest()
		{
			var lastState = _container.Create<UserNameState>();
			var state = _container.Create<PasswordState>();
			var nextState = _container.Create<OpenAccountState>();
			var config = _container.Create<StateMachineConfig>();

			var context = _container.Create<StateMachineContext>();

			context.Config = config;
			context.LastState = lastState;
			context.State = state;
			context.NextState = nextState;

			var eventArgs = new StateChangeEventArgs(context, typeof(StateChangeEventArgsTests), "test2345798zt4e3t");

			Assert.NotNull(eventArgs.LastState);
			Assert.NotNull(eventArgs.CurrentState);
			Assert.NotNull(eventArgs.NextState);

			Assert.Equal(typeof(UserNameState), eventArgs.LastState?.GetType());
			Assert.Equal(typeof(PasswordState), eventArgs.CurrentState?.GetType());
			Assert.Equal(typeof(OpenAccountState), eventArgs.NextState?.GetType());
			Assert.Equal(typeof(StateChangeEventArgsTests), eventArgs.StateType);
			Assert.Equal("test2345798zt4e3t", eventArgs.MethodName);

		}

		public void Dispose()
		{
			_container?.Dispose();
		}
	}
}
