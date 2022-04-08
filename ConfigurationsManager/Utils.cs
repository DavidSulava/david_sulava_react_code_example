using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationsManager
{
	public class Utils
	{
		public static string Base64(string input)
		{
			var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(input);
			return (string)System.Convert.ToBase64String(plainTextBytes);
		}
	}
}
