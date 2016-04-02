using System;

using Newtonsoft.Json;

namespace DNNCon
{
	public class ItemModel
	{
		public ItemModel ()
		{
		}

		[JsonProperty("id")]
		public string ID { get; set;}

		[JsonProperty("name")]
		public string Name { get; set;}

		[JsonProperty("description")]
		public string Description { get; set;}

		[JsonProperty("assignedUser")]
		public int AssignedUser { get; set;}

		[JsonProperty("editUrl")]
		public string EditUrl { get; set;}
	}
}

