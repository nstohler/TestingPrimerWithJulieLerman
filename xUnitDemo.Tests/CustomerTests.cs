using DemoLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;

namespace xUnitDemo.Tests
{
	public class CustomerTests
	{
		[Fact]
		public void TestCustomerModifiedDate()
		{
			var now = DateTime.Now;
			var customer = new Customer(null)
			{
				ModifiedDate = now,
			};
			Assert.Equal(now, customer.LastUpdated);
		}

		[Fact]
		public void TestCustomerModifiedDate2()
		{
			var now = DateTime.Now;
			var modDate = now.AddDays(3);

			var customer = new Customer(null)
			{
				ModifiedDate = now,
				ContactDetail = new ContactDetail() { ModifiedDate = modDate },

			};
			Assert.Equal(modDate, customer.LastUpdated);	// hits simulated db/service
		}

		[Fact]
		public void TestCustomerModifiedDateWithMoq()
		{
			var mock = new Mock<IContactDetail>();
			
			var now = DateTime.Now;
			var modDate = now.AddDays(3);

			mock.Setup(x => x.ModifiedDate).Returns(modDate);

			var customer = new Customer(null)
			{
				ModifiedDate = now,
				ContactDetail = mock.Object,
			};
			
			Assert.Equal(modDate, customer.LastUpdated);	// mock object usage
		}

		[Fact]
		public void TestLongRunningMethodWithMoq()
		{
			var mock = new Mock<ISocialMediaLookupService>();
			mock.Setup(x => x.GetLastSocialMediaActivity()).Returns(DateTime.Today);

			var customer = new Customer(mock.Object);
			
			var date = customer.GetLastSocialMediaActivity();

			Assert.Equal(date, DateTime.Today);

			//Assert.Equal()
		}
	}
}
