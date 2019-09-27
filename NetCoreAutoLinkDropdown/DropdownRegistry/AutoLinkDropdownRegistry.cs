using NetCoreAutoLinkDropdown.AutoLinkDropdownBehavior;
using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreAutoLinkDropdown.DropdownRegistry
{
	public class AutoLinkDropdownRegistry
	{
		public static AutoLinkDropdownRegistry Registry { get; set; }
		public Dictionary<string, List<DropdownItem>> DropdownItemsDict { get; private set; } = new Dictionary<string, List<DropdownItem>>();

		public static AutoLinkDropdownRegistry Get()
		{
			if (Registry == null) Registry = new AutoLinkDropdownRegistry();
			return Registry;
		}

		public void AddToDict(string ProviderDdlId, List<DropdownItem> Data)
		{
			if (DropdownItemsDict.ContainsKey(ProviderDdlId))
			{
				DropdownItemsDict.Remove(ProviderDdlId);
			}
			DropdownItemsDict.Add(ProviderDdlId, Data);
		}

		public List<DropdownItem> GetFromDict(string ProviderDdlId)
		{
			List<DropdownItem> dropdowns = new List<DropdownItem>();
			DropdownItemsDict.TryGetValue(ProviderDdlId, out dropdowns);
			return dropdowns;
		}
	}
}
