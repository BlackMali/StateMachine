using Autofac;
using BlackMali.StateMachine.AutoFac;
using Xunit;

namespace BlackMali.StateMachine.Autofac.Test
{
	public class StateMachineModuleTests
	{
		internal class TestState1 : State
		{ }

		internal class TestState2 : State
		{ }

		[Fact]
		public void ModuleTest()
		{
			var builder = new ContainerBuilder();
			builder.RegisterModule<StateMachineModule>();
			builder.RegisterType<TestState1>().AsSelf();
			builder.RegisterType<TestState2>().AsSelf();

			var container = builder.Build();

			var stateMachineBuilder = container.Resolve<IStateMachineBuilder>();
			stateMachineBuilder.AddState<TestState1>()
				.AddStartTransition()
				.AddTransition<TestState2>();

			stateMachineBuilder.AddState<TestState2>()
				.AddTransition<TestState1>();

			var machine = stateMachineBuilder.Build();

			machine.Transmit<TestState1>();
			machine.Transmit<TestState2>();
			machine.Transmit<TestState1>();
		}

		[Fact]
		public void ModuleWithoutRegistrationTest()
		{
			var builder = new ContainerBuilder();
			builder.RegisterModule<StateMachineModule>();
			builder.RegisterType<TestState1>().AsSelf();
			
			var container = builder.Build();

			var stateMachineBuilder = container.Resolve<IStateMachineBuilder>();
			stateMachineBuilder.AddState<TestState1>()
				.AddStartTransition()
				.AddTransition<TestState2>();

			stateMachineBuilder.AddState(new TestState2())
				.AddTransition<TestState1>();

			var machine = stateMachineBuilder.Build();

			machine.Transmit<TestState1>();
			machine.Transmit<TestState2>();
			machine.Transmit<TestState1>();
		}

		[Fact]
		public void ServiceFactoryTest()
		{
			var builder = new ContainerBuilder();
			builder.RegisterModule<StateMachineModule>();

			var container = builder.Build();

			var factory = container.Resolve<StateMachineServiceFactory>();

			var inspector = factory(typeof(IStateChangeInspector));
			Assert.IsType<StrictStateChangeInspector>(inspector);
			Assert.IsNotType<StateChangeInspector>(inspector);
		}

		[Fact]
		public void StateProviderTest()
		{
			var builder = new ContainerBuilder();
			builder.RegisterModule<StateMachineModule>();

			var container = builder.Build();

			var provider = container.Resolve<IStateProvider>();

			Assert.IsType<StateProvider>(provider);			
		}

		[Fact]
		public void StateMachineConfigTest()
		{
			var builder = new ContainerBuilder();
			builder.RegisterModule<StateMachineModule>();

			var container = builder.Build();

			var config = container.Resolve<IStateMachineConfig>();

			Assert.IsType<StateMachineConfig>(config);
		}		
	}
}