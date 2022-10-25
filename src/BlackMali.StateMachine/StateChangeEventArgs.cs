using System;

namespace BlackMali.StateMachine
{
	/// <summary>
	/// EventArgs for state change event
	/// </summary>
	public class StateChangeEventArgs : EventArgs
	{
		/// <inheritdoc/>
		public StateChangeEventArgs(IStateMachineContext context, Type stateType, string methodName)
		{
			LastState = context.LastState;
			CurrentState = context.State;
			NextState = context.NextState;
			Context = context;
			StateType = stateType;
			MethodName = methodName;
		}

		/// <summary>
		/// Last state
		/// </summary>
		public IState? LastState { get; }

		/// <summary>
		/// Current state
		/// Attention: The current state within the OnEnter method corresponds to the previous status
		/// </summary>
		public IState? CurrentState { get; }

		/// <summary>
		/// Next state
		/// </summary>
		public IState? NextState { get; }

		/// <summary>
		/// State machine context
		/// </summary>
		public IStateMachineContext Context { get; }

		/// <summary>
		/// State type of the called method <see cref="MethodName"/>
		/// </summary>
		public Type StateType { get; }

		/// <summary>
		/// Name of state method
		/// </summary>
		public string MethodName { get; }
	}
}