﻿using DbAccessLib;
using NUnit.Framework;
using Ploeh.AutoFixture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitDemo.Tests
{
	[TestFixture]
	public class TestFirst
	{
		[Test]
		public void PersonFirstNameLastNameConcatenationReturnsCorrectResult()
		{
			var fixture = new Fixture();

			fixture.Customize<Person>(c => c.Without(p => p.AddressSet));

			//var person = new Person() { FirstName = "Nicolas", LastName = "Stohler", BirthDate = DateTime.Today, ShoeSizeUS = 1.0 };
			var person = fixture.Build<Person>()
				.With(x => x.FirstName, "Nicolas")
				.With(x => x.LastName, "Stohler")
				// .Without(x => x.AddressSet)
				.Create();

			Assert.That(person.FullName, Is.EqualTo("Nicolas Stohler"));

			person = fixture.Build<Person>()
				.With(x => x.FirstName, "Hans    ")
				.With(x => x.LastName, "  Wurst")
				// .Without(x => x.AddressSet)
				.Create();

			Assert.That(person.FullName, Is.EqualTo("Hans Wurst"));

			person = fixture.Create<Person>();
		
			Assert.That(person.FullName, Is.EqualTo($"{person.FirstName.Trim()} {person.LastName.Trim()}"));
		}
	}
}
