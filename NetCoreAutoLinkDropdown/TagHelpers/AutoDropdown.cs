using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using NetCoreAutoLinkDropdown.AutoLinkDropdownBehavior;
using NetCoreAutoLinkDropdown.DropdownRegistry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAutoLinkDropdown.TagHelpers
{
	[HtmlTargetElement("auto-dropdown")]
	public class AutoDropdown : SelectTagHelper
	{
		protected IHtmlGenerator generator;
		public List<DropdownItem> Data { get; set; }

		public AutoDropdown(IHtmlGenerator generator) : base(generator)
		{
			this.generator = generator;
		}

		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			Items = Dropdown.From(Data ?? new List<DropdownItem>());
			await base.ProcessAsync(context, output);
			output.TagName = "select";

			string id = output.Attributes.FirstOrDefault(attribute => attribute.Name == "id").Value.ToString();
			AutoLinkDropdownRegistry.Get().AddToDict(id, Data);
		}
	}
}
