using System;
using System.Collections.Generic;
using System.Text;

namespace NetCoreAutoLinkDropdown.AutoLinkDropdown
{
	public interface DropdownItem
	{
		string DropdownValue();
		string DropdownText();
		bool DropdownItemEnabled();
	}
}
