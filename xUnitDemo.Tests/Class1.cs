using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace xUnitDemo.Tests
{
	public class Class1
	{
		[Fact]
		public void PassingTest()
		{
			Assert.Equal(4, Add(2, 2));
		}

		[Fact]
		public void FailingTest()
		{
			Assert.Equal(5, Add(2, 2));
			//Assert.NotEmpty
		}

		private int Add(int x, int y)
		{
			return x + y;
		}

		[Theory]
		[InlineData(3)]
		[InlineData(5)]
		[InlineData(6)]
		public void MyFirstTheory(int value)
		{
			Assert.True(IsOdd(value));
		}

		private bool IsOdd(int value)
		{
			return value % 2 == 1;
		}

		[Fact]
		public void TestException()
		{
			// http://hadihariri.com/2008/10/17/testing-exceptions-with-xunit/
			Exception ex = Assert.Throws<ApplicationException>(() => ThrowsException());
			Assert.Equal("this is what i want!", ex.Message);
		}


		private void ThrowsException()
		{
			throw new ApplicationException("this is what i want!");
		}
	}
}
