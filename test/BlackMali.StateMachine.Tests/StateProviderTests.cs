using BlackMali.StateMachine.Tests.Login;
using Xunit;
using System;
using System.Threading.Tasks;

namespace BlackMali.StateMachine.Tests
{
    public class StateProviderTests
	{

#pragma warning disable CS8625 // Cannot convert null literal to non-nullable reference type.

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

#pragma warning restore CS8625 // Cannot convert null literal to non-nullable reference type.

	}
}