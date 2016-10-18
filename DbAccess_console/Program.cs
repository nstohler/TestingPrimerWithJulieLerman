using DbAccessLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccess_console
{
	class Program
	{
		static void Main(string[] args)
		{
			using(var context = new PersonDbContext())
			{
				PersonDbService dbAccess = new PersonDbService(context);				

				//var p = new Person()
				//{sd
				//	FirstName = "Tina",
				//	LastName = "Körner",
				//	BirthDate = new DateTime(1982, 12, 11),
				//	ShoeSizeUS = 7.0,
				//};

				////context.PersonSet.Add(p);
				////context.SaveChanges();

				//dbAccess.AddPerson(p);

				var personSet = dbAccess.GetPersonsWithAddresses();
				foreach (var person in personSet)
				{
					Console.WriteLine("{0} : ", person.FullName);
					foreach (var address in person.AddressSet)
					{
						Console.WriteLine("    {0} in {1}", address.Street, address.City);
					}
				}

			}
		}
	}
}
