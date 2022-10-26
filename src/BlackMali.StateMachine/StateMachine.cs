﻿using System;
using System.Threading.Tasks;

namespace BlackMali.StateMachine
{
	/// <summary>
	/// State machine
	/// </summary>
	internal class StateMachine : IStateMachine
	{
		private StateMachineContext _context;
		private readonly IStateChangeInspector _inspector;

		/// <summary>
		/// State machine
		/// </summary>
		public StateMachine(StateMachineContext context, IStateChangeInspector inspector)
		{
			_context = context ?? throw new ArgumentNullException(nameof(context));
			_inspector = inspector ?? throw new ArgumentNullException(nameof(inspector));
		}

		/// <inheritdoc/>
		public IState? State => _context.State;

		/// <inheritdoc/>
		public event EventHandler<StateChangeEventArgs>? OnBeforeStateMethod;

		/// <inheritdoc/>
		public event EventHandler<StateChangeEventArgs>? OnAfterTransmitted;

		/// <inheritdoc/>
		public async Task Transmit<TState>()
		{
			await Transmit<TState>(new StateMachineEvent());
		}

		/// <inheritdoc/>
		public async Task Transmit<TState>(StateMachineEvent @event)
		{
			var state = await _context.GetState<TState>();

			await Transmit(state, @event);
		}

		/// <inheritdoc/>
		protected async Task Transmit(IState state, StateMachineEvent @event)
		{
			if (state is null)
				throw new ArgumentNullException(nameof(state));
			
			if (@event is null)
				throw new ArgumentNullException(nameof(@event));

			await _inspector.Inspect(_context, state);

			_context.NextState = state;

			if (_context.State != null)
			{
				if (OnBeforeStateMethod != null)
					OnBeforeStateMethod(this, new StateChangeEventArgs(_context, _context.State.GetType(), nameof(IState.OnExit)));

				await _context.State.OnExit(_context);
			}

			if (OnBeforeStateMethod != null)
				OnBeforeStateMethod(this, new StateChangeEventArgs(_context, _context.NextState.GetType(), nameof(IState.OnEnter)));

			await _context.NextState.OnEnter(_context);

			_context.LastState = _context.State;
			_context.State = state;
			_context.NextState = null;

			await CallOnTransmitMethod(@event);
		}

		public async Task Post(StateMachineEvent @event)
		{
			await CallOnTransmitMethod(@event);
		}

		protected async Task CallOnTransmitMethod(StateMachineEvent @event)
		{
			if (_context.State == null)
				throw new StateMachineException("No status set in the status machine");

			if (OnBeforeStateMethod != null)
				OnBeforeStateMethod(this, new StateChangeEventArgs(_context, _context.State.GetType(), nameof(IState.OnTransmitted)));

			var nextState = await _context.State.OnTransmitted(_context, @event);

			if (OnAfterTransmitted != null)
				OnAfterTransmitted(this, new StateChangeEventArgs(_context, _context.State.GetType(), nameof(IState.OnTransmitted)));

			if (nextState == null)
				return;

			await Transmit(nextState, @event);
		}
	}
}