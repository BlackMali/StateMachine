using Autofac.Extras.Moq;
using BlackMali.StateMachine.Tests.Login;
using Xunit;
using System;
using System.Threading.Tasks;

namespace BlackMali.StateMachine.Tests
{
    public class StateMachineTests : IDisposable
	{
		private readonly AutoMock _container;

		public StateMachineTests()
		{
			_container = AutoMock.GetLoose();
		}

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

		[Fact]
		public void ConstructorTest()
		{
			var inspector = new StateChangeInspector();
			var context = new StateMachineContext(new StateMachineConfig(), new StateProvider());

			Assert.Throws<ArgumentNullException>(() => new StateMachine(null, inspector));
			Assert.Throws<ArgumentNullException>(() => new StateMachine(context, null));

			var machine = new StateMachine(context, inspector);
			Assert.NotNull(machine);
		}

		[Fact]
		public async Task TransmitWithEventExceptionTest()
		{
			var builder = new StateMachineBuilder(new StateMachineConfig(), new StateProvider(), ServiceFactory.GetServiceFactory);
			builder.AddState(new UserNameState());

			var machine = builder.Build();

			await Assert.ThrowsAsync<ArgumentNullException>(() => machine.Transmit<UserNameState>(null));
		}

		[Fact]
		public async Task TransmitExceptionTest1()
		{
			var builder = new StateMachineBuilder(new StateMachineConfig(), new StateProvider(), ServiceFactory.GetServiceFactory);
			builder.AddState(new UserNameState());
			
			var machine = builder.Build();

			await machine.Transmit<UserNameState>();

			await Assert.ThrowsAsync<StateMachineException>(() => machine.Transmit<PasswordState>(null));
		}

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

		[Fact]
		public async Task TransmitExceptionTest2()
		{
			var machine = _container.Create<StateMachine>();

			await Assert.ThrowsAsync<StateMachineException>(() => machine.Transmit<PasswordState>(new StateMachineEvent()));
		}

		[Fact]
		public async Task PostExceptionTest2()
		{
			var machine = _container.Create<StateMachine>();

			await Assert.ThrowsAsync<StateMachineException>(() => machine.Post(new StateMachineEvent()));
		}

		public void Dispose()
		{
			_container?.Dispose();
		}
	}
}