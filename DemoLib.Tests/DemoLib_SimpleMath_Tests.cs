using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DemoLib.Tests
{
	[TestClass]
	public class DemoLib_SimpleMath_Tests
	{
		[TestMethod]
		public void AddAddsArguments()
		{
			// Arrange
			int a = 1;
			int b = 2;
			
			// Acct
			var result = SimpleMath.Add(a, b);

			// Assert
			Assert.AreEqual(result, a + b);
		}
	}
}
