using Xunit;

namespace BlackMali.StateMachine.Tests.Login
{
	public class LoginProcessTests
	{
		[Fact]
		public async Task LoginProcessTest1()
		{
			var builder = new StateMachineBuilder(new StateMachineConfig(), new StateProvider());
			builder.AddState(new UserNameState());
			builder.AddState(new PasswordState());
			builder.AddState(new OpenAccountState());

			var stateMachine = builder.Build();

			await stateMachine.Transmit<UserNameState>();

			var methods = new List<string>();
			stateMachine.OnBeforeStateMethod += (sender, args) => {
				methods.Add($"{args.StateType.Name}.{args.MethodName}"); };

			Assert.Equal(typeof(UserNameState), stateMachine.State?.GetType());

			await stateMachine.Post(new EnterUserNameEvent("admin"));
			Assert.Equal(4, methods.Count);
			Assert.Equal(1, methods.Count(m => m.Equals($"{nameof(UserNameState)}.{nameof(IState.OnTransmitted)}")));
			Assert.Equal(1, methods.Count(m => m.Equals($"{nameof(UserNameState)}.{nameof(IState.OnExit)}")));
			Assert.Equal(1, methods.Count(m => m.Equals($"{nameof(PasswordState)}.{nameof(IState.OnEnter)}")));
			Assert.Equal(1, methods.Count(m => m.Equals($"{nameof(PasswordState)}.{nameof(IState.OnTransmitted)}")));

			Assert.Equal(typeof(PasswordState), stateMachine.State?.GetType());
			await stateMachine.Post(new EnterPasswordNameEvent("password123"));
			Assert.Equal(8, methods.Count);
			Assert.Equal(2, methods.Count(m => m.Equals($"{nameof(PasswordState)}.{nameof(IState.OnTransmitted)}")));
			Assert.Equal(1, methods.Count(m => m.Equals($"{nameof(PasswordState)}.{nameof(IState.OnExit)}")));
			Assert.Equal(1, methods.Count(m => m.Equals($"{nameof(OpenAccountState)}.{nameof(IState.OnEnter)}")));
			Assert.Equal(1, methods.Count(m => m.Equals($"{nameof(OpenAccountState)}.{nameof(IState.OnTransmitted)}")));

			Assert.Equal(typeof(OpenAccountState), stateMachine.State?.GetType());
		}

		[Fact]
		public async Task LoginProcessTest2()
		{
			var builder = new StateMachineBuilder(new StateMachineConfig(), new StateProvider());
			builder.AddState(new UserNameState());
			builder.AddState(new PasswordState());
			builder.AddState(new OpenAccountState());

			var stateMachine = builder.Build();

			await stateMachine.Transmit<UserNameState>();

			var transmits = 0;
			var methods = new List<string>();
			stateMachine.OnBeforeStateMethod += (sender, args) => {
				methods.Add($"{args.StateType.Name}.{args.MethodName}");
			};

			stateMachine.OnAfterTransmitted += (sender, args) => { transmits++; };

			Assert.Equal(typeof(UserNameState), stateMachine.State?.GetType());

			var userNameEvent = new EnterUserNameEvent("admin");
			Assert.False(userNameEvent.IsHandled);

			await stateMachine.Transmit<UserNameState>(userNameEvent);
			Assert.Equal(6, methods.Count);
			Assert.Equal(1, methods.Count(m => m.Equals($"{nameof(UserNameState)}.{nameof(IState.OnEnter)}")));
			Assert.Equal(1, methods.Count(m => m.Equals($"{nameof(UserNameState)}.{nameof(IState.OnTransmitted)}")));
			Assert.Equal(2, methods.Count(m => m.Equals($"{nameof(UserNameState)}.{nameof(IState.OnExit)}")));
			Assert.Equal(1, methods.Count(m => m.Equals($"{nameof(PasswordState)}.{nameof(IState.OnEnter)}")));
			Assert.Equal(1, methods.Count(m => m.Equals($"{nameof(PasswordState)}.{nameof(IState.OnTransmitted)}")));

			Assert.Equal(2, transmits);
			Assert.True(userNameEvent.IsHandled);

			Assert.Equal(typeof(PasswordState), stateMachine.State?.GetType());
			await stateMachine.Transmit<PasswordState>(new EnterPasswordNameEvent("password123"));
			Assert.Equal(12, methods.Count);
			Assert.Equal(2, methods.Count(m => m.Equals($"{nameof(PasswordState)}.{nameof(IState.OnEnter)}")));
			Assert.Equal(2, methods.Count(m => m.Equals($"{nameof(PasswordState)}.{nameof(IState.OnTransmitted)}")));
			Assert.Equal(2, methods.Count(m => m.Equals($"{nameof(PasswordState)}.{nameof(IState.OnExit)}")));
			Assert.Equal(1, methods.Count(m => m.Equals($"{nameof(OpenAccountState)}.{nameof(IState.OnEnter)}")));
			Assert.Equal(1, methods.Count(m => m.Equals($"{nameof(OpenAccountState)}.{nameof(IState.OnTransmitted)}")));
			Assert.Equal(4, transmits);

			Assert.Equal(typeof(OpenAccountState), stateMachine.State?.GetType());
			await stateMachine.Transmit<OpenAccountState>();
			Assert.Equal(15, methods.Count);
			Assert.Equal(2, methods.Count(m => m.Equals($"{nameof(OpenAccountState)}.{nameof(IState.OnEnter)}")));
			Assert.Equal(2, methods.Count(m => m.Equals($"{nameof(OpenAccountState)}.{nameof(IState.OnTransmitted)}")));
			Assert.Equal(1, methods.Count(m => m.Equals($"{nameof(OpenAccountState)}.{nameof(IState.OnExit)}")));
		}

		[Fact]
		public async Task LoginProcessUseStrictModeTest()
		{
			var builder = new StateMachineBuilder(new StateMachineConfig(), new StateProvider())
				.UseStrictMode();

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
