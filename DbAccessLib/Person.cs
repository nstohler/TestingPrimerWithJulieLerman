using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccessLib
{
	public class Person
	{
		public virtual int PersonId { get; set; }
		public virtual string FirstName { get; set; }
		public string LastName { get; set; }
		public DateTime BirthDate { get; set; }
		public double ShoeSizeUS { get; set; }

		public string FullName
		{
			get
			{
				return string.Format("{0} {1}", FirstName.Trim(), LastName.Trim());
			}
		}

		//public virtual ICollection<Address> AddressSet { get; set; }
		public ICollection<Address> AddressSet { get; set; }
	}
}
