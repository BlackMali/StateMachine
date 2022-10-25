using BlackMali.StateMachine;
using BlackMali.StateMachine.Tests.Login;
using Xunit;

namespace BlackMali.StateMachine.Tests
{
    public class StateProviderTests
	{
		[Fact]
		public async Task GetStateTest()
		{
			var provider = new StateProvider();

			await Assert.ThrowsAsync<ArgumentNullException>(() => provider.GetState(null));
			await Assert.ThrowsAsync<StateMachineException>(() => provider.GetState(typeof(PasswordState)));
		}

		[Fact]
		public void SetStateTest()
		{
			var provider = new StateProvider();

			Assert.Throws<ArgumentNullException>(() => provider.SetState<IState>(null));
		}
	}
}