using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreAutoLinkDropdown.AutoLinkDropdown
{
	public interface SubDropdownItemProviderFactory
	{
		List<DropdownItem> GetSubDropdownItems(string Key);
	}
}
