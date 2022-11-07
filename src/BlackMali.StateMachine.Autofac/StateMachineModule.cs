using Autofac;

namespace BlackMali.StateMachine.AutoFac
{
	/// <summary>
	/// Autofac Module for state machines
	/// </summary>
	public class StateMachineModule : Module
	{
		/// <summary>
		/// Behavior for state change validation
		/// 
		/// <code>true</code> = <see cref="StrictStateChangeInspector"/> and <code>false</code> = <see cref="StateChangeInspector"/>
		/// </summary>
		public bool UseStrictMode { get; set; } = true;

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

			RegisterInspector(builder);

			builder.Register<StateMachineServiceFactory>(outerContext =>
				{
					var innerContext = outerContext.Resolve<IComponentContext>();

					return serviceType => innerContext.Resolve(serviceType);
				});
		}

		/// <summary>
		/// Register the configured state change inspector
		/// </summary>
		/// <param name="builder">Autofac Container Builder</param>
		private void RegisterInspector(ContainerBuilder builder)
		{
			if (UseStrictMode)
			{
				builder.RegisterType<StrictStateChangeInspector>()
					.As<IStateChangeInspector>();

				return;
			}

			builder.RegisterType<StateChangeInspector>()
					.As<IStateChangeInspector>();
		}
	}
}