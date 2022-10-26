using System;

namespace BlackMali.StateMachine
{
	/// <summary>
	/// Delegate for state resolving
	/// </summary>
	/// <param name="type">Type to be resolved</param>
	/// <returns>Instance of type</returns>
	public delegate object StateMachineServiceFactory(Type type);	
}