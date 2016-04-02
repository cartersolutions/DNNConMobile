using System;
using System.Threading.Tasks;

using Newtonsoft.Json;

using DNNCon.Helpers;
using DNNCon.Models;

namespace DNNCon.Security
{
	public class Authorization
	{
		public Authorization ()
		{			
		}
	
		public static async Task<JWTModel> GetTokenAsync(string username, string password)
		{
			var login = new LoginModel{ Username = username, Password = password };

			string url = "http://dnncon.gotdotnetnukeyet.com/DesktopModules/JwtAuth/API/mobile/login";
			return await APIHelper.PostEncodedData<JWTModel> (url, JsonConvert.SerializeObject(login).ToString());
		}
	}
}

