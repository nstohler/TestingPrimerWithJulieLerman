﻿using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NUnitDemo.Tests
{
	[TestFixture]
	public class EnumTests
	{
		[Test]
		public void TestEnumName()
		{
			var summerName = Season.Summer.DisplayNameOrEnumName();

			Assert.That(summerName, Is.EqualTo("The Dream"));

			var wusel = Season.Wusel.DisplayNameOrEnumName();

			Assert.That(wusel, Is.EqualTo("Wusel"));
		}

		public enum Season
		{
			[Display(Name = "The Autumn")]
			Autumn,

			[Display(Name = "The Weather")]
			Winter,

			[Display(Name = "The Tease")]
			Spring,

			[Display(Name = "The Dream")]
			Summer,

			Wusel,
		}
	}

	static class EnumExtensions
	{
		// http://stackoverflow.com/a/35273581/54159
		// add nuget: Microsoft.Net.Compilers

		/// returns the localized Name, if a [Display(Name="Localised Name")] attribute is applied to the enum member
		/// returns null if there isnt an attribute
		public static string DisplayNameOrEnumName(this Enum value)
		// => value.DisplayNameOrDefault() ?? value.ToString()
		{
			// More efficient form of ^ based on http://stackoverflow.com/a/17034624/11635
			var enumType = value.GetType();
			var enumMemberName = Enum.GetName(enumType, value);
			return enumType
				.GetEnumMemberAttribute<DisplayAttribute>(enumMemberName)
				?.GetName() // Potentially localized
				?? enumMemberName; // Or fall back to the enum name
		}

		///// returns the localized Name, if a [Display] attribute is applied to the enum member
		///// returns null if there is no attribute		
		public static string DisplayNameOrDefault(this Enum value) => 
			value.GetEnumMemberAttribute<DisplayAttribute>()?.GetName();

		static TAttribute GetEnumMemberAttribute<TAttribute>(this Enum value) where TAttribute : Attribute =>
			value.GetType().GetEnumMemberAttribute<TAttribute>(value.ToString());

		static TAttribute GetEnumMemberAttribute<TAttribute>(this Type enumType, string enumMemberName) where TAttribute : Attribute =>
			enumType.GetMember(enumMemberName).Single().GetCustomAttribute<TAttribute>();
	}
}
