using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
//using EntityFramework.MoqHelper;
using DbAccessLib;
using Moq;
using System.Data.Entity;
using NSubstitute;
using EntityFramework.Testing;
using MockDbContextTests;

namespace NUnitDemo.Tests
{
	[TestFixture]
	public class DbAccessTest
	{
		[Test]
		public void TestAdd()
		{
			// check/extend?:
			// https://github.com/RichardSilveira/EntityFramework.MoqHelper

			// NSubstitute instead of Mock?
			// http://nsubstitute.github.io/help/getting-started/
			// http://www.nogginbox.co.uk/blog/mocking-entity-framework-data-context

			var personSet = new List<Person>();

			//var mockSet = EntityFrameworkMoqHelper.CreateMockForDbSet<Person>()
			//	.SetupForQueryOn(personSet)
			//	.WithAdd(personSet, "PersonId");

			//var mockContext = EntityFrameworkMoqHelper.CreateMockForDbContext<PersonDbContext, Person>(mockSet);
			//mockContext.Setup(x => x.PersonSet).Returns(personSet);

			//var personDbService = new PersonDbService(mockContext.Object);

			var data = new List<Person>
			{
				new Person { FirstName = "Hallo", LastName = "Velo", BirthDate = DateTime.Today, ShoeSizeUS = 0.0 },
				new Person { FirstName = "3232", LastName = "Vel5554o", BirthDate = DateTime.Today, ShoeSizeUS = 10.0 },
				new Person { FirstName = "444", LastName = "Ve2234lo", BirthDate = DateTime.Today, ShoeSizeUS = 20.0 },
			}.AsQueryable();

			var mockSet = new Mock<DbSet<Person>>();			

			mockSet.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(data.Provider);
			mockSet.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(data.Expression);
			mockSet.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(data.ElementType);
			mockSet.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

			mockSet.Setup(set => set.Add(It.IsAny<Person>())).Callback<Person>(personSet.Add);

			var mockContext = new Mock<PersonDbContext>();
			mockContext.Setup(c => c.PersonSet).Returns(mockSet.Object);						

			var personDbService = new PersonDbService(mockContext.Object);
			
			var person = new Person() { FirstName = "Wusel", LastName = "Dusel", BirthDate = DateTime.Today, ShoeSizeUS = 1.0 };

			var p = personDbService.AddPerson(person);

			//Assert.That(p.FirstName, Is.EqualTo("Wusel"));

			Assert.That(personSet, Contains.Item(person));
			mockSet.Verify(m => m.Add(It.IsAny<Person>()), Times.Once());
			mockContext.Verify(m => m.SaveChanges(), Times.Once());

			//personSet.Should().

		}

		[Test]
		public void TestListAll()
		{
			//GetAllPersons

			var data = new List<Person>
			{
				new Person { FirstName = "Hallo", LastName = "Velo", BirthDate = DateTime.Today, ShoeSizeUS = 0.0 },
				new Person { FirstName = "3232", LastName = "Vel5554o", BirthDate = DateTime.Today, ShoeSizeUS = 10.0 },
				new Person { FirstName = "444", LastName = "Ve2234lo", BirthDate = DateTime.Today, ShoeSizeUS = 20.0 },
			}.AsQueryable();

			var mockSet = new Mock<DbSet<Person>>();

			mockSet.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(data.Provider);
			mockSet.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(data.Expression);
			mockSet.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(data.ElementType);
			mockSet.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

			var mockContext = new Mock<PersonDbContext>();
			mockContext.Setup(c => c.PersonSet).Returns(mockSet.Object);

			var personDbService = new PersonDbService(mockContext.Object);

			var resultSet = personDbService.GetAllPersons();

			Assert.That(resultSet.Count(), Is.EqualTo(data.Count()));			
			CollectionAssert.AreEquivalent(resultSet, data);
		}

		[Test]
		public void TestGetLastNameContains()
		{
			//GetLastNameContains
			var data = new List<Person>
			{
				new Person { FirstName = "Hallo", LastName = "Velo", BirthDate = DateTime.Today, ShoeSizeUS = 0.0 },
				new Person { FirstName = "3232", LastName = "Vel5554o", BirthDate = DateTime.Today, ShoeSizeUS = 10.0 },
				new Person { FirstName = "444", LastName = "Ve2234lo", BirthDate = DateTime.Today, ShoeSizeUS = 20.0 },
			}.AsQueryable();

			var mockSet = new Mock<DbSet<Person>>();

			mockSet.As<IQueryable<Person>>().Setup(m => m.Provider).Returns(data.Provider);
			mockSet.As<IQueryable<Person>>().Setup(m => m.Expression).Returns(data.Expression);
			mockSet.As<IQueryable<Person>>().Setup(m => m.ElementType).Returns(data.ElementType);
			mockSet.As<IQueryable<Person>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

			var mockContext = new Mock<PersonDbContext>();
			mockContext.Setup(c => c.PersonSet).Returns(mockSet.Object);

			var personDbService = new PersonDbService(mockContext.Object);

			var resultSet = personDbService.GetLastNameContains("5554");

			Assert.That(resultSet.Count(), Is.EqualTo(1));

			resultSet = personDbService.GetLastNameContains("nothingthatexists!");

			Assert.That(resultSet.Count(), Is.EqualTo(0));

			//CollectionAssert.AreEquivalent(resultSet, data);
		}

		// http://nsubstitute.github.io/
		// https://github.com/scott-xu/EntityFramework.Testing
		// 
		// mainly: http://stackoverflow.com/a/21074664/54159
		[Test]
		public void TestGetLastNameContains_NSubstitute()
		{
			//GetLastNameContains
			var data = new List<Person>
			{
				new Person { FirstName = "Hallo", LastName = "Velo", BirthDate = DateTime.Today, ShoeSizeUS = 0.0 },
				new Person { FirstName = "3232", LastName = "Vel5554o", BirthDate = DateTime.Today, ShoeSizeUS = 10.0 },
				new Person { FirstName = "444", LastName = "Ve2234lo", BirthDate = DateTime.Today, ShoeSizeUS = 20.0 },
			}.AsQueryable();

			//---------------
			// Version A
			//---------------

			////var nsubSet = Substitute.For<DbSet<Person>, IQueryable<Person>>();						

			////((IQueryable<Person>)nsubSet).Provider.Returns(data.Provider);
			////((IQueryable<Person>)nsubSet).Expression.Returns(data.Expression);
			////((IQueryable<Person>)nsubSet).ElementType.Returns(data.ElementType);
			////((IQueryable<Person>)nsubSet).GetEnumerator().Returns(data.GetEnumerator());

			////var nsubContext = Substitute.For<PersonDbContext>();
			////nsubContext.PersonSet.Returns(nsubSet);

			//---------------
			// Version B
			//---------------

			//var nsubSet = Substitute.For<IDbSet<Person>, DbSet<Person>>();

			//nsubSet.Provider.Returns(data.Provider);
			//nsubSet.Expression.Returns(data.Expression);
			//nsubSet.ElementType.Returns(data.ElementType);
			//nsubSet.GetEnumerator().Returns(data.GetEnumerator());

			//var nsubContext = Substitute.For<PersonDbContext>();
			//nsubContext.PersonSet.Returns(nsubSet);

			//---------------
			// Version C
			//---------------

			// arrange
			var nsubSet = Substitute.For<IDbSet<Person>, DbSet<Person>>().Initialize(data);			

			var nsubContext = Substitute.For<PersonDbContext>();
			nsubContext.PersonSet.Returns(nsubSet);

			var personDbService = new PersonDbService(nsubContext);

			var resultSet = personDbService.GetLastNameContains("5554");

			Assert.That(resultSet.Count(), Is.EqualTo(1));

			resultSet = personDbService.GetLastNameContains("nothingthatexists!");

			Assert.That(resultSet.Count(), Is.EqualTo(0));

			//CollectionAssert.AreEquivalent(resultSet, data);
		}

		// shortest & easiest!
		// using modules from https://github.com/IgorWolbers/DbContextMockForUnitTests
		[Test]
		public void TestGetLastNameContains_MockDbContextTests()
		{
			//GetLastNameContains
			var data = new List<Person>
			{
				new Person { FirstName = "Hallo", LastName = "Velo", BirthDate = DateTime.Today, ShoeSizeUS = 0.0 },
				new Person { FirstName = "3232", LastName = "Vel5554o", BirthDate = DateTime.Today, ShoeSizeUS = 10.0 },
				new Person { FirstName = "444", LastName = "Ve2234lo", BirthDate = DateTime.Today, ShoeSizeUS = 20.0 },
			};		// .AsQueryable(); not needed here!

			// arrange
			var mockSubSet = data.GenerateMockDbSet();

			var mockContext = Substitute.For<PersonDbContext>();
			mockContext.PersonSet.Returns(mockSubSet);

			var personDbService = new PersonDbService(mockContext);

			// act
			var resultSet = personDbService.GetLastNameContains("5554");

			// assert
			Assert.That(resultSet.Count(), Is.EqualTo(1));

			resultSet = personDbService.GetLastNameContains("nothingthatexists!");

			Assert.That(resultSet.Count(), Is.EqualTo(0));

			//CollectionAssert.AreEquivalent(resultSet, data);
		}
	}

	public static class ExtentionMethods
	{
		public static IDbSet<T> Initialize<T>(this IDbSet<T> dbSet, IQueryable<T> data) where T : class
		{
			dbSet.Provider.Returns(data.Provider);
			dbSet.Expression.Returns(data.Expression);
			dbSet.ElementType.Returns(data.ElementType);
			dbSet.GetEnumerator().Returns(data.GetEnumerator());
			return dbSet;
		}
	}
}
