using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccessLib
{	
	public class PersonDbContext : DbContext
	{
		public virtual DbSet<Person> PersonSet { get; set; }
		public virtual DbSet<Address> AddressSet { get; set; }

		// public virtual IDbSet<Person> PersonSet { get; set; }	// might work better with NSubstitute http://stackoverflow.com/a/21074664/54159
	}
}
