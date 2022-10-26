using Autofac.Extras.Moq;
using BlackMali.StateMachine.Tests.Login;
using Xunit;
using System;
using System.Collections.Generic;

namespace BlackMali.StateMachine.Tests
{
	public class TransitionBuilderTests : IDisposable
	{
		private readonly AutoMock _container;

		public TransitionBuilderTests()
		{
			_container = AutoMock.GetLoose();
		}


		[Fact]
		public void AddStartTransitionExceptionTest()
		{
			var transitions = new List<StateMachineTransition>();
			var config = _container.Mock<IStateMachineConfig>();
			config.Setup(c => c.Transitions)
				.Returns(transitions);

			var builder = new TransitionBuilder(config.Object, null);

			Assert.Throws<StateMachineException>(() => builder.AddStartTransition());
		}

		[Fact]
		public void AddInStateTransitionTest()
		{
			var transitions = new List<StateMachineTransition>();
			var config = _container.Mock<IStateMachineConfig>();
			config.Setup(c => c.Transitions)
				.Returns(transitions);

			var builder = new TransitionBuilder(config.Object, new UserNameState())
				.AddInStateTransition();

			var singleTransition = Assert.Single(transitions);
			Assert.Equal(typeof(UserNameState), singleTransition.FromState);
			Assert.Equal(typeof(UserNameState), singleTransition.ToState);			
		}

		[Fact]
		public void AddTransitionTest()
		{
			var transitions = new List<StateMachineTransition>();
			var config = _container.Mock<IStateMachineConfig>();
			config.Setup(c => c.Transitions)
				.Returns(transitions);

			var builder = new TransitionBuilder(config.Object, new UserNameState())
				.AddTransition<PasswordState>();

			var singleTransition = Assert.Single(transitions);
			Assert.Equal(typeof(UserNameState), singleTransition.FromState);
			Assert.Equal(typeof(PasswordState), singleTransition.ToState);

			var builder2 = new TransitionBuilder(config.Object, null);
			builder.AddTransition<UserNameState>();
		}

		[Fact]
		public void AddTransitionStartTest()
		{
			var transitions = new List<StateMachineTransition>();
			var config = _container.Mock<IStateMachineConfig>();
			config.Setup(c => c.Transitions)
				.Returns(transitions);

			var builder = new TransitionBuilder(config.Object, null);

			Assert.Throws<StateMachineException>(() => builder.AddInStateTransition());
			Assert.Empty(transitions);

			builder.AddTransition<UserNameState>();

			var singleTransition = Assert.Single(transitions);
			Assert.Null(singleTransition.FromState);
			Assert.Equal(typeof(UserNameState), singleTransition.ToState);			
		}

		public void Dispose()
		{
			_container?.Dispose();
		}
	}
}
