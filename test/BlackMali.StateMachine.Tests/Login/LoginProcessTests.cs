﻿using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace BlackMali.StateMachine.Tests.Login
{
	public class LoginProcessTests
	{
		[Fact]
		public async Task LoginProcessTest1()
		{
			var builder = new StateMachineBuilder(new StateMachineConfig(), new StateProvider(), ServiceFactory.GetServiceFactory);
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
			var builder = new StateMachineBuilder(new StateMachineConfig(), new StateProvider(), ServiceFactory.GetServiceFactory);
			builder.AddState(new UserNameState());
			builder.AddState(new PasswordState());
			builder.AddState(new OpenAccountState());

			var stateMachine = builder.Build();

			await stateMachine.Transmit<UserNameState>();

			var transmits = 0;
			var methods = new List<string>();
			stateMachine.OnBeforeStateMethod += (sender, args) =>
			{
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
		public async Task LoginProcessTest3()
		{
			var builder = new StateMachineBuilder(new StateMachineConfig(), new StateProvider(), ServiceFactory.GetServiceFactory);
			builder.AddState(new UserNameState());
			builder.AddState(new ExceptionState());

			var stateMachine = builder.Build();

			var errors = new List<StateErrorEventArgs>();
			stateMachine.OnError += (sender, args) => 
			{
				errors.Add(args);
			};

			Assert.Null(stateMachine.State);

			var userNameEvent = new EnterUserNameEvent("admin");
			Assert.False(userNameEvent.IsHandled);

			var exStateResult = await stateMachine.TryTransmit<ExceptionState>(userNameEvent);
			var lastError = Assert.Single(errors);
			Assert.False(exStateResult);
			Assert.Null(stateMachine.State);
			Assert.NotNull(lastError.Exception);
			Assert.Equal(typeof(ExceptionState), lastError.NextStateType);

			exStateResult = await stateMachine.TryPost(userNameEvent);
			Assert.Equal(2, errors.Count);
			Assert.False(exStateResult);
			Assert.Null(stateMachine.State);

			lastError = errors.Last();
			Assert.NotNull(lastError.Exception);
			Assert.Null(lastError.NextStateType);

			var userStateResult = await stateMachine.TryTransmit<UserNameState>();
			Assert.True(userStateResult);
			Assert.Equal(2, errors.Count);
			Assert.NotNull(stateMachine.State);
			Assert.IsType<UserNameState>(stateMachine.State);

			userStateResult = await stateMachine.TryPost(new StateMachineEvent());
			Assert.True(userStateResult);
			Assert.Equal(2, errors.Count);

			var transmits = 0;
			var methods = new List<string>();
			stateMachine.OnBeforeStateMethod += (sender, args) =>
			{
				methods.Add($"{args.StateType.Name}.{args.MethodName}");
			};

			stateMachine.OnAfterTransmitted += (sender, args) => { transmits++; };

			Assert.Equal(typeof(UserNameState), stateMachine.State?.GetType());

			await Assert.ThrowsAsync<StateMachineException>(() => stateMachine.Transmit<ExceptionState>(userNameEvent));
			Assert.Equal(2, errors.Count);
			Assert.False(userNameEvent.IsHandled);

			exStateResult = await stateMachine.TryTransmit<ExceptionState>(userNameEvent);
			Assert.False(exStateResult);
			Assert.Equal(3, errors.Count);

			userNameEvent.IsHandled = true;
			exStateResult = await stateMachine.TryTransmit<ExceptionState>(userNameEvent);
			Assert.Equal(3, errors.Count);
			Assert.True(exStateResult);
			Assert.Equal(typeof(ExceptionState), stateMachine.State?.GetType());

			userNameEvent.IsHandled = false;
			exStateResult = await stateMachine.TryPost(userNameEvent);
			Assert.False(exStateResult);
			Assert.Equal(4, errors.Count);
		}
	}
}