using System;
using System.Threading.Tasks;

namespace BlackMali.StateMachine
{
	/// <summary>
	/// Interface for state machines
	/// </summary>
	public interface IStateMachine
	{
		/// <summary>
		/// Current state
		/// </summary>
		public IState? State { get; }

		/// <summary>
		/// Fires for each state method call (OnExit, OnEnter, OnTransmitted)
		/// </summary>
		event EventHandler<StateChangeEventArgs>? OnBeforeStateMethod;

		/// <summary>
		/// Fires for each state method call (OnExit, OnEnter, OnTransmitted)
		/// </summary>
		event EventHandler<StateChangeEventArgs>? OnAfterTransmitted;

		/// <summary>
		/// Performs the state change
		/// </summary>
		/// <exception cref="StateMachineException">If no state was found</exception>
		Task Transmit<TState>();

		/// <summary>
		/// Performs the state change
		/// </summary>
		/// <param name="event">An event</param>
		/// <exception cref="StateMachineException">If no state was found</exception>
		Task Transmit<TState>(StateMachineEvent @event);

		/// <summary>
		/// Posts the event to current state. Just calls the OnTransmitted method
		/// </summary>
		/// <param name="event">An event</param>
		Task Post(StateMachineEvent @event);
	}
}