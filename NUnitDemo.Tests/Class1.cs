using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace NUnitDemo.Tests
{
	[TestFixture]
	public class Class1
	{
		// http://www.infragistics.com/community/blogs/dhananjay_kumar/archive/2015/07/27/getting-started-with-net-unit-testing-using-nunit.aspx
		// http://templecoding.com/blog/2016/02/29/running-tests-in-parallel-with-nunit3/

		[Test]
		public void PassingTest()
		{
			//Assert.AreEqual(4, Add(2, 2));
			Assert.That(Add(2, 2), Is.EqualTo(4));
		}

		
		[Test]
		[Ignore("fails")]
		public void FailingTest()
		{			
			Assert.AreEqual(5, Add(2, 2));
			//Assert.NotEmpty
		}

		private int Add(int x, int y)
		{
			return x + y;
		}

		[TestCase(3, ExpectedResult = true)]
		[TestCase(5, ExpectedResult = true)]
		[TestCase(6, ExpectedResult = false)]
		public bool MyFirstTheory(int value)
		{
			return IsOdd(value);
		}

		private bool IsOdd(int value)
		{
			return value % 2 == 1;
		}

		[Test]
		public void TestException()
		{
			// http://hadihariri.com/2008/10/17/testing-exceptions-with-xunit/
			Exception ex = Assert.Throws<ApplicationException>(() => ThrowsException());
			Assert.That(ex.Message, Is.EqualTo("this is what i want!"));
		}


		private void ThrowsException()
		{
			throw new ApplicationException("this is what i want!");
		}
	}
}
