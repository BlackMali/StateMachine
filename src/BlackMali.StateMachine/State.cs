using System.Threading.Tasks;

namespace BlackMali.StateMachine
{
    /// <summary>
    /// Abstract base class for states
    /// </summary>
    public abstract class State : IState
    {
        /// <inheritdoc/>
        public virtual async Task OnEnter(IStateMachineContext context)
        {
            await Task.CompletedTask;
        }

		/// <inheritdoc/>
		public virtual async Task<IState?> OnTransmitted(IStateMachineContext context, StateMachineEvent @event)
        {
            await Task.CompletedTask;

            return null;
        }

		/// <inheritdoc/>
		public virtual async Task OnExit(IStateMachineContext context)
        {
            await Task.CompletedTask;
        }
    }
}