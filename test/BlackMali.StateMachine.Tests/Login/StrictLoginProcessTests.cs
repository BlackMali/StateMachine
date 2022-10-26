using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BlackMali.StateMachine.Tests.Login
{
	public class StrictLoginProcessTests
	{
		[Fact]
		public async Task LoginProcessUseStrictModeTest()
		{
			var builder = new StateMachineBuilder(new StateMachineConfig(), new StateProvider(), ServiceFactory.GetStrictServiceFactory);

			builder.AddState(new UserNameState())
				.AddStartTransition()
				.AddInStateTransition()
				.AddTransition<PasswordState>();

			builder.AddState(new PasswordState())
				.AddInStateTransition()
				.AddTransition<OpenAccountState>();

			builder.AddState(new OpenAccountState());

			var stateMachine = builder.Build();

			await Assert.ThrowsAsync<StateMachineException>(() => stateMachine.Transmit<OpenAccountState>());

			var methods = new List<string>();
			stateMachine.OnBeforeStateMethod += (sender, args) => {
				methods.Add($"{args.StateType.Name}.{args.MethodName}");
			};

			await stateMachine.Transmit<UserNameState>();
			Assert.Equal(1, methods.Count(m => m.Equals($"{nameof(UserNameState)}.{nameof(IState.OnEnter)}")));
			Assert.Equal(1, methods.Count(m => m.Equals($"{nameof(UserNameState)}.{nameof(IState.OnTransmitted)}")));

			Assert.Equal(typeof(UserNameState), stateMachine.State?.GetType());

			await Assert.ThrowsAsync<StateMachineException>(() => stateMachine.Transmit<OpenAccountState>());

			await stateMachine.Transmit<UserNameState>(new EnterUserNameEvent("admin"));
			Assert.Equal(8, methods.Count);
			Assert.Equal(2, methods.Count(m => m.Equals($"{nameof(UserNameState)}.{nameof(IState.OnEnter)}")));
			Assert.Equal(2, methods.Count(m => m.Equals($"{nameof(UserNameState)}.{nameof(IState.OnTransmitted)}")));
			Assert.Equal(2, methods.Count(m => m.Equals($"{nameof(UserNameState)}.{nameof(IState.OnExit)}")));
			Assert.Equal(1, methods.Count(m => m.Equals($"{nameof(PasswordState)}.{nameof(IState.OnEnter)}")));
			Assert.Equal(1, methods.Count(m => m.Equals($"{nameof(PasswordState)}.{nameof(IState.OnTransmitted)}")));

			Assert.Equal(typeof(PasswordState), stateMachine.State?.GetType());
			await stateMachine.Transmit<PasswordState>(new EnterPasswordNameEvent("password123"));
			Assert.Equal(14, methods.Count);
			Assert.Equal(2, methods.Count(m => m.Equals($"{nameof(PasswordState)}.{nameof(IState.OnEnter)}")));
			Assert.Equal(2, methods.Count(m => m.Equals($"{nameof(PasswordState)}.{nameof(IState.OnTransmitted)}")));
			Assert.Equal(2, methods.Count(m => m.Equals($"{nameof(PasswordState)}.{nameof(IState.OnExit)}")));
			Assert.Equal(1, methods.Count(m => m.Equals($"{nameof(OpenAccountState)}.{nameof(IState.OnEnter)}")));
			Assert.Equal(1, methods.Count(m => m.Equals($"{nameof(OpenAccountState)}.{nameof(IState.OnTransmitted)}")));

			Assert.Equal(typeof(OpenAccountState), stateMachine.State?.GetType());
			await Assert.ThrowsAsync<StateMachineException>(() => stateMachine.Transmit<UserNameState>());
		}
	}
}