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
		/// Triggers when an error has occurred (only for TryTransmit and TryPost methods)
		/// </summary>
		event EventHandler<StateErrorEventArgs>? OnError;

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
		/// Performs a safe state change. Use OnError for error handling
		/// </summary>
		/// <typeparam name="TState">Next state</typeparam>
		Task<bool> TryTransmit<TState>();

		/// <summary>
		/// Performs a safe state change. Use OnError for error handling
		/// </summary>
		/// <typeparam name="TState">Next state</typeparam>
		/// <param name="event">An event</param>
		Task<bool> TryTransmit<TState>(StateMachineEvent @event);

		/// <summary>
		/// Posts the event to current state. Just calls the OnTransmitted method
		/// </summary>
		/// <param name="event">An event</param>
		Task Post(StateMachineEvent @event);

		/// <summary>
		/// Safely posts the event to the current state. Just calls the OnTransmitted method. Use OnError for error handling
		/// </summary>
		/// <param name="event">An event</param>
		Task<bool> TryPost(StateMachineEvent @event);
	}
}