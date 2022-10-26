using Autofac.Extras.Moq;
using BlackMali.StateMachine.Tests.Login;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace BlackMali.StateMachine.Tests
{
    public class StateMachineContextTests : IDisposable
	{
		private AutoMock _container;

		public StateMachineContextTests()
		{
			_container = AutoMock.GetLoose();
		}

		[Fact]
		public async Task GetStateTest()
		{
			var states = new List<Type>();
			states.Add(typeof(UserNameState));

			var config = _container.Mock<IStateMachineConfig>();
			config.Setup(c => c.States)
				.Returns(states);

			var context = _container.Create<StateMachineContext>();

			await Assert.ThrowsAsync<StateMachineException>(() => context.GetState<OpenAccountState>());			
		}

		public void Dispose()
		{
			_container?.Dispose();
		}
	}
}
