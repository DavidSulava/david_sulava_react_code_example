using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigurationsManager
{
	internal static class Configuration
	{
		internal static string ForgeClientId => Program.Configuration["Forge:ClientId"].ToString();
		
		internal static string ForgeClientSecret => Program.Configuration["Forge:ClientSecret"].ToString();

		internal static string ForgeRegion => Program.Configuration["Forge:Region"].ToString();

	}
}
