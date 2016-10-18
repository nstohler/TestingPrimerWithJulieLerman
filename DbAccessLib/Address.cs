using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccessLib
{
	public class Address
	{
		public int AddressId { get; set; }
		public int PersonId { get; set; }
		public string Street { get; set; }
		public string Zip { get; set; }
		public string City { get; set; }

		//public virtual Person Person { get; set; }
		public Person Person { get; set; }
	}
}
