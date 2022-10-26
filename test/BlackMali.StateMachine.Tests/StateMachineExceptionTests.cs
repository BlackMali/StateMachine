using Castle.Core.Internal;
using Xunit;
using System;
using System.Linq;

namespace BlackMali.StateMachine.Tests
{
	public class StateMachineExceptionTests
	{
		[Fact]
		public void AttributeTest()
		{
			var attributes = typeof(StateMachineException).GetAttributes<SerializableAttribute>();

			Assert.Single(attributes);			
		}

		[Fact]
		public void ConstructorTest()
		{
			var exception1 = new StateMachineException("gv4ehuio 948t9z84 9ht 9843h984thf9hf");
			Assert.Equal("gv4ehuio 948t9z84 9ht 9843h984thf9hf", exception1.Message);
			Assert.Null(exception1.InnerException);

			var exception2 = new StateMachineException("gv4ehuio 948t9z84 9ht 9843h984thf9hf", new Exception());
			Assert.Equal("gv4ehuio 948t9z84 9ht 9843h984thf9hf", exception2.Message);
			Assert.NotNull(exception2.InnerException);

			var constructors = typeof(StateMachineException).GetConstructors(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
			Assert.Equal(2, constructors.Count());

			var parameterless = constructors.FirstOrDefault(c => c.GetParameters().Length == 0);
			Assert.NotNull(parameterless);

			// Null check with Assert.NotNull
#pragma warning disable CS8602 // Dereference of a possibly null reference.

			parameterless.Invoke(null);

#pragma warning restore CS8602 // Dereference of a possibly null reference.
		}
	}
}