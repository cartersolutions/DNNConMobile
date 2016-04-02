using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

using DNNCon.Helpers;
using DNNCon.Models;

namespace DNNCon.Repositories
{
	public class ItemRepository
	{
		public ItemRepository ()
		{
		}

		/// <summary>
		/// Gets the stores.
		/// </summary>
		/// <returns>The stores.</returns>
		public static async Task<List<ItemModel>> GetItemsAsync ()
		{
			string url = "http://dnncon.gotdotnetnukeyet.com/DesktopModules/DnnSpaModule1/API/Item";
			return  await APIHelper.ExecuteAsync<List<ItemModel>> (url);
		}
	}
}

