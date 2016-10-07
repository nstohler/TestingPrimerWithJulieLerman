using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbAccessLib
{
	public class Person
	{
		public int PersonId { get; set; }
		public string FirstName { get; set; }
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
	}
}
