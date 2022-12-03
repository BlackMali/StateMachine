# StateMachine

[![.NET](https://github.com/blackmali/StateMachine/actions/workflows/ci.yml/badge.svg)](https://github.com/blackmali/StateMachine/actions/workflows/ci.yml)
[![NuGet](https://img.shields.io/nuget/vpre/BlackMali.StateMachine.svg)](https://www.nuget.org/packages/BlackMali.StateMachine)
![GitHub](https://img.shields.io/github/license/blackmali/statemachine)
[![NuGet](https://img.shields.io/nuget/dt/BlackMali.StateMachine.svg)](https://www.nuget.org/packages/BlackMali.StateMachine) 

State machine implementation in C#.

## Advantages:
- DI ready
- Async Pattern
- Safe programming with nullable types
- No dependencies on third-party Nuget packages
- Works without constants and enums
- Builder for state machine creation
- Configuration of states and transitions
- Strict (default) and open transitions
- more than 95% code coverage

### Compatible target frameworks

|Product|Version|
|_|_|
|.NET Framework| `.net 4.6.x` `.net 4.7.x` `.net 4.8.x` |
|.NET Standard| `.net standard 2.x` |
|.NET Core| `.net Core app 2.x` `.net core app 3.x` |
|.NET| `.net 5` `.net 6` `.net 7` |

## NuGet

You should install BlackMali.StateMachine with NuGet:
	
	Install-Package BlackMali.StateMachine.Autofac

Or via the .NET Core command line interface:

	dotnet add package BlackMali.StateMachine.Autofac

## Registration
### Autofac
```csharp
var builder = new ContainerBuilder();
builder.RegisterModule<StateMachineModule>();
```

## Configuration

```csharp
public class SlotMachine
{
	private readonly IStateMachine _stateMachine;

	public SlotMachine(IStateMachineBuilder builder)
	{
		// With registration -> AddState<LockState>()
		builder.AddState<LockState>()
			.AddStartTransition();
			.AddInStateTransition()
			.AddTransition<UnLockState>()
			.AddTransition<EndState>();

		// Without registration -> AddState(new UnLockState())
		builder.AddState(new UnLockState())
			.AddInStateTransition()
			.AddTransition<LockState>()
			.AddTransition<EndState>();

		// Create state machine
		_stateMachine = builder.Build();
	}
}
```

## State change

```csharp

// Perform state change
await machine.Transmit<LockState>();

// Or with Event
await machine.Transmit<LockState>(new StateMachineEvent());

// Or safe with exception handling
machine.OnError += (sender, args) => { };
await machine.TryTransmit<LockState>(new StateMachineEvent());

```

### Event publishing with Post

```csharp

// Posts an event to the current status
await machine.Post(new StateMachineEvent());

// Safe posting
await machine.TryPost(new StateMachineEvent());

```

## State implementation

```csharp

internal class LockState 
	: State // you can also use the interface IState without overrides
{
	// optional:
	public override async Task OnEnter(IStateMachineContext context)
	{
		await base.OnEnter(context);

		Console.WriteLine("Please insert coin");
	}

	// optional:
	public override async Task<IState?> OnTransmitted(IStateMachineContext context, StateMachineEvent @event)
	{
		var unLockEvent = @event as UnLockEvent;
		if (unLockEvent == null)
			return null;

		// No state change...
		if (unLockEvent.Coin == null)
			return null;

		// State change to UnLockState
		return await context.GetState<UnLockState>();
	}

	// optional:
	public override async Task OnExit(IStateMachineContext context)
	{
		// Do something...

		await Task.CompletedTask;
	}
}

```

### Solution Packages

|Package|Dependencies|
|-|-|
|BlackMali.StateMachine|No|
|BlackMali.StateMachine.Autofac|Autofac|

#### Unit-Tests:
- XUnit
- XUnit.Runner.VisualStudio
- Autofac.Extras.Moq
- Moq
- Coverlet.Collector

### IDE
Implemented with Microsoft Visual Studio Community 2022

- [Markdown Editor](https://github.com/MadsKristensen/MarkdownEditor2022)
- [Fine Code Coverage](https://marketplace.visualstudio.com/items?itemName=FortuneNgwenya.FineCodeCoverage)