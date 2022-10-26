# Dependency Injection (DI)

Only Autofac can currently be used for DI. If you need additional DI support, just get in touch.

## BlackMali.StateMachine.Autofac

### How to use

- Install Autofac
- Register StateMachineModule
- Resolve IStateMachineBuilder
- Build your StateMachine

#### Example

```csharp
// (1) Build an AutoFac container
var builder = new ContainerBuilder();
builder.RegisterModule<StateMachineModule>();

// (2) Register your states
builder.RegisterType<TestState1>().AsSelf();
// builder.RegisterType<TestState2>().AsSelf();

var container = builder.Build();

// (3) Resolve the StateMachineBuilder with Resolve<>() or constructor
var stateMachineBuilder = container.Resolve<IStateMachineBuilder>();

// (4) AddState<>() can only be used with registration (2)
stateMachineBuilder.AddState<TestState1>()
	.AddStartTransition()
	.AddTransition<TestState2>();

// (5) Without state registration (2) you should use this
stateMachineBuilder.AddState(new TestState2())
	.AddTransition<TestState1>();

// Build the machine
var machine = stateMachineBuilder.Build();

// Transmit with IStateMachine.Transmit<>() or in-state
machine.Transmit<TestState1>();
machine.Transmit<TestState2>();
machine.Transmit<TestState1>();
```