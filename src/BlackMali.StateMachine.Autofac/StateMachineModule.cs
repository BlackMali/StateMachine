using Autofac;

namespace BlackMali.StateMachine.AutoFac
{
	/// <summary>
	/// Autofac Module for state machines
	/// </summary>
	public class StateMachineModule : Module
	{
		/// <inheritdoc/>
		protected override void Load(ContainerBuilder builder)
		{
			base.Load(builder);

			builder.RegisterType<StateMachineConfig>()
				.As<IStateMachineConfig>();
			builder.RegisterType<StateProvider>()
				.As<IStateProvider>();
			builder.RegisterType<StateMachineBuilder>()
				.As<IStateMachineBuilder>();
			builder.RegisterType<StrictStateChangeInspector>()
				.As<IStateChangeInspector>();

			builder.Register<StateMachineServiceFactory>(outerContext =>
				{
					var innerContext = outerContext.Resolve<IComponentContext>();

					return serviceType => innerContext.Resolve(serviceType);
				});
		}
	}
}