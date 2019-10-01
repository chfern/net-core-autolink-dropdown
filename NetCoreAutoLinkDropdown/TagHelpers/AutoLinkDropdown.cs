using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAutoLinkDropdown.TagHelpers
{
	[HtmlTargetElement("autolink-dropdown")]
	public class AutoLinkDropdown : TagHelper
	{
		private readonly IHttpContextAccessor contextAccessor;
		protected IHtmlGenerator generator;

		public string Provider { get; set; }
		public string ProvideFor { get; set; }
		public string SubDropdownKey { get; set; }

		public AutoLinkDropdown(IHtmlGenerator generator, IHttpContextAccessor contextAccessor)
		{
			this.generator = generator;
			this.contextAccessor = contextAccessor;
		}

		public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
		{
			output.TagName = "script";

			string scripts = BuildScript();
			output.Content.SetHtmlContent(scripts);
		}

		private string BuildScript()
		{
			List<string> SubscriberDropdowns = ProvideFor.Split(';').ToList();
			StringBuilder scriptStringBuilder = new StringBuilder();
			/**
			 * Add
			 */
			SubscriberDropdowns.ForEach(subscriberDropdown =>
			{
				scriptStringBuilder.Append($@"
					document.getElementById('{Provider}').ondropdownchanged = async function(event){{
						const selected = this.value;
						const autolinkSubdropdownItemsRes = await fetch(`{contextAccessor.HttpContext.Request.Scheme}://{contextAccessor.HttpContext.Request.Host}/AutoLinkDropdownProvider/ChildDropdown?ParentId={Provider}&SubDropdownKey={SubDropdownKey}&ParentValue=${{selected}}`, {{headers: {{
								  'Content-Type': 'application/json'
								}}
							}})
						const autolinkSubdropdownItems = await autolinkSubdropdownItemsRes.json()

						
						let targetDropdown = document.getElementById('{subscriberDropdown}')
						targetDropdown.innerHTML = ''
						autolinkSubdropdownItems.forEach(selectListItem => {{
							const el = document.createElement('option');

							el.textContent = selectListItem.text;
							el.value = selectListItem.value;
							targetDropdown.options.add(el);
						}})
					}}

					if(document.getElementById('{Provider}').ondropdownchanged)
						document.getElementById('{Provider}').removeEventListener('change', document.getElementById('{Provider}').ondropdownchanged)

					document.getElementById('{Provider}').addEventListener('change', document.getElementById('{Provider}').ondropdownchanged);
					let changeEvent = new Event('change')
					document.getElementById('{Provider}').dispatchEvent(changeEvent)
				");
			});
			return scriptStringBuilder.ToString();
		}
	}
}
