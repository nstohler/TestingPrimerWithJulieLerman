using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace DbAccessLib
{
	public class PersonDbService
	{
		private readonly PersonDbContext _personDbContext;

		public PersonDbService(PersonDbContext personDbContext)
		{
			_personDbContext = personDbContext;
		}

		public Person AddPerson(Person p)
		{
			var r = _personDbContext.PersonSet.Add(p);
			_personDbContext.SaveChanges();

			return r;
		}

		public List<Person> GetAllPersons()
		{
			var query = from p in _personDbContext.PersonSet
						orderby p.LastName
						select p;

			return query.ToList();
		}

		public List<Person> GetLastNameContains(string substring)
		{
			return _personDbContext.PersonSet
				.Where(p => p.LastName.Contains(substring))
				.ToList();
		}

		public List<Person> GetPersonsWithAddresses()
		{
			var query = _personDbContext.PersonSet
						.Include(x => x.AddressSet)
						.Select(p => p);

			return query.ToList();
		}

		public void RemoveById(int personId)
		{
			var p = _personDbContext.PersonSet.Single(x => x.PersonId == personId);

			_personDbContext.PersonSet.Remove(p);

			_personDbContext.SaveChanges();
		}
	}
}
