using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MSTestExtensions;

namespace DemoLib.Tests
{
	[TestClass]
	public class CustomerTests : BaseTest
	{
		[TestMethod]
		// MS
		//[ExpectedException(typeof(InvalidOperationException))]
		public void EnteringMoreThan50CharactersInLastNameIsNotAllowed()
		{
			var customer = new Customer(null); // { LastName = new string('x', 60) };

			// Extension method
			// http://www.bradoncode.com/blog/2012/02/extending-assert-in-mstest.html
			Assert.Throws(() => customer.LastName = new string('x', 60));
			Assert.Throws(() => customer.LastName = new string('x', 60), "String too long. Max length is 50");
			Assert.Throws(() => customer.LastName = new string('x', 60), "String too long. Max length is 50", ExceptionInheritanceOptions.Inherits);	// ??? 
			
			
			// Julie
			//try
			//{
			//	var customer = new Customer() { LastName = new string('x', 60) };
			//	Assert.Fail("Customer.LastName allowed a string longer than 50");
			//}
			//catch (Exception ex)
			//{
			//	Assert.IsInstanceOfType(ex, typeof(InvalidOperationException));
			//}
		}
	}
}
