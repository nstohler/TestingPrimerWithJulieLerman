using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoLib
{
	public class Customer
	{
		private readonly ISocialMediaLookupService _socialMediaLookupService;
		private string _lastName = String.Empty;

		public Customer(ISocialMediaLookupService socialMediaLookupService)
		{
			_socialMediaLookupService = socialMediaLookupService;

		}

		public string LastName
		{
			get { return _lastName;  }
			set
			{
				if(value.Length > 50)
				{
					throw new InvalidOperationException("String too long. Max length is 50");
				}
				_lastName = value;
			}
		}

		public DateTime ModifiedDate { get; set; }

		public IContactDetail ContactDetail { get; set; }

		public DateTime LastUpdated
		{
			get
			{
				if(ContactDetail != null)
				{					
					if(ModifiedDate > ContactDetail.ModifiedDate)
					{						
						return ModifiedDate;
					}
					else
					{
						//System.Threading.Thread.Sleep(3000);
						return ContactDetail.ModifiedDate;
					}					
				}
				return ModifiedDate;
			}
		}
		public DateTime? GetLastSocialMediaActivity()
		{
			if(_socialMediaLookupService != null)
			{
				return _socialMediaLookupService.GetLastSocialMediaActivity();
			}
			return null;
		}		
	}

	public interface ISocialMediaLookupService
	{
		DateTime? GetLastSocialMediaActivity();
	}

	public class SocialMediaLookupService : ISocialMediaLookupService
	{
		public DateTime? GetLastSocialMediaActivity()
		{
			System.Threading.Thread.Sleep(3000);
			return DateTime.Today;
		}
	}

	public interface IContactDetail
	{
		DateTime ModifiedDate { get; set; }
	}
	
	public class ContactDetail : IContactDetail
	{
		private DateTime _ModifiedDate;
		public DateTime ModifiedDate
		{
			get
			{
				System.Threading.Thread.Sleep(3000);	// hit db/service
				return _ModifiedDate;
			}
			set
			{
				_ModifiedDate = value;
			}
		}
	}
}
