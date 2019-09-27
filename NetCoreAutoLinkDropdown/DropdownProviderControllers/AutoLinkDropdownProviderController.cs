using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using NetCoreAutoLinkDropdown.AutoLinkDropdownBehavior;
using NetCoreAutoLinkDropdown.DropdownRegistry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetCoreAutoLinkDropdown.DropdownProviderControllers
{
	public class AutoLinkDropdownProviderController : Controller
	{
		public IActionResult ChildDropdown(string ParentId, string SubDropdownKey, int ParentValue)
		{
			List<DropdownItem> DropdownItems = AutoLinkDropdownRegistry.Get().GetFromDict(ParentId);
			DropdownItem selectedDdlItem = DropdownItems.Where(x => x.DropdownValue() == ParentValue.ToString()).FirstOrDefault();

			if (!typeof(SubDropdownItemProviderFactory).IsAssignableFrom(selectedDdlItem.GetType()))
			{
				throw new InvalidParentDropdownException($"Unable to create SubDropdownFactory from parent dropdown {ParentId} (Class: {selectedDdlItem.GetType().FullName}), make sure {selectedDdlItem.GetType().FullName} implements SubDropdownItemProviderFactory");
			}

			SubDropdownItemProviderFactory subDropdownItemProviderFactory = (SubDropdownItemProviderFactory)selectedDdlItem;
			List<DropdownItem> subDropdownItems = subDropdownItemProviderFactory?.GetSubDropdownItems(SubDropdownKey) ?? new List<DropdownItem>();
			List<SelectListItem> selectListItems = Dropdown.From(subDropdownItems);
			return Json(selectListItems);
		}
			
		public class InvalidParentDropdownException : Exception
		{
			public InvalidParentDropdownException(string ErrMsg) : base(ErrMsg) { }
		}
	}
}
