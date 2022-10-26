using System;

namespace BlackMali.StateMachine.Tests
{
	internal class ServiceFactory
	{
		public static object GetStrictServiceFactory(Type serviceType)
		{
			if (serviceType == typeof(IStateChangeInspector))
				return new StrictStateChangeInspector();

			// Only for unit testing...
#pragma warning disable CS8603 // Possible null reference return.
			return null;
#pragma warning restore CS8603 // Possible null reference return.
		}

		public static object GetServiceFactory(Type serviceType)
		{
			if (serviceType == typeof(IStateChangeInspector))
				return new StateChangeInspector();

			// Only for unit testing...
#pragma warning disable CS8603 // Possible null reference return.
			return null;
#pragma warning restore CS8603 // Possible null reference return.
		}

	}
}
