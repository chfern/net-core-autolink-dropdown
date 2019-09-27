using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreAutoLinkDropdown.AutoLinkDropdownBehavior
{
	public interface SubDropdownItemProviderFactory
	{
		List<DropdownItem> GetSubDropdownItems(string Key);
	}
}
