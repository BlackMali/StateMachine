using Autofac.Extras.Moq;
using BlackMali.StateMachine.Tests.Login;
using Xunit;

namespace BlackMali.StateMachine.Tests
{
	public class StrictStateChangeInspectorTests : IDisposable
	{
		private readonly AutoMock _container;

		public StrictStateChangeInspectorTests()
		{
			_container = AutoMock.GetLoose();
		}

		[Fact]
		public async Task InspectExceptionTest1()
		{
			var context = _container.Mock<IStateMachineContext>();

			var inspector = new StrictStateChangeInspector();

			await Assert.ThrowsAsync<ArgumentNullException>(() => inspector.Inspect(null, new UserNameState()));
			await Assert.ThrowsAsync<ArgumentNullException>(() => inspector.Inspect(context.Object, null));
		}

		[Fact]
		public async Task InspectTest()
		{
			var config = _container.Mock<IStateMachineConfig>();
			config.Setup(c => c.Transitions)
				.Returns(new[] { new StateMachineTransition(typeof(UserNameState), typeof(PasswordState)) });

			var context = _container.Mock<IStateMachineContext>();
			context.Setup(c => c.Config)
				.Returns(config.Object);

			context.Setup(c => c.State)
				.Returns(new UserNameState());

			var inspector = new StrictStateChangeInspector();

			await inspector.Inspect(context.Object, new PasswordState());

		}
		[Fact]
		public async Task InspectExceptionTest2()
		{
			var config = _container.Mock<IStateMachineConfig>();
			config.Setup(c => c.Transitions)
				.Returns(new[] { new StateMachineTransition(typeof(UserNameState), typeof(PasswordState)) });

			var context = _container.Mock<IStateMachineContext>();
			context.Setup(c => c.Config)
				.Returns(config.Object);

			context.Setup(c => c.State)
				.Returns(new PasswordState());

			var inspector = new StrictStateChangeInspector();

			await Assert.ThrowsAsync<StateMachineException>(() => inspector.Inspect(context.Object, new UserNameState()));
		}

		public void Dispose()
		{
			_container?.Dispose();
		}
	}
}
