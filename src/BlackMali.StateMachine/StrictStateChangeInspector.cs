using System;
using System.Linq;
using System.Threading.Tasks;

namespace BlackMali.StateMachine
{
	/// <summary>
	/// Inspector with state change validation
	/// </summary>
	public class StrictStateChangeInspector : IStateChangeInspector
	{
		/// <summary>
		/// Inspector with state change validation
		/// </summary>
		public StrictStateChangeInspector()
		{ }

		/// <inheritdoc/>
		public async Task Inspect(IStateMachineContext context, IState nextState)
		{
			if (context is null)
				throw new ArgumentNullException(nameof(context));
			
			if (nextState is null)
				throw new ArgumentNullException(nameof(nextState));

			await Task.CompletedTask;

			var currentStateType = context.State?.GetType();
			var nextStateType = nextState?.GetType();

			var transition = context.Config.Transitions
				.FirstOrDefault(t => t.FromState == currentStateType 
						&& t.ToState == nextStateType);

			if (transition != null)
				return;

			throw new StateMachineException($"No transition for state change [{currentStateType}] -> [{nextState}] found.");
		}
	}
}