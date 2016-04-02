using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Newtonsoft.Json;

namespace DNNCon.Helpers
{
	public class APIHelper
	{
		public APIHelper ()
		{
		}

		/// <summary>
		/// Executes the async.
		/// </summary>
		/// <returns>The async.</returns>
		/// <param name="url">URL.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static async Task<T> ExecuteAsync<T>(string url)
		{
			T returnValue = default(T);

			try {				
				using (var handler = new HttpClientHandler { UseDefaultCredentials = true })
				using (var client = new HttpClient (handler)) {
					client.DefaultRequestHeaders.Add ("Accept", "application/json");				
					client.DefaultRequestHeaders.Add ("Cache-Control", "no-cache");	
					client.DefaultRequestHeaders.Add ("Authorization", "Bearer " + App.BEARER_TOKEN);
					var response = await Task.Run(() =>
						{
							var cancelSource = new CancellationTokenSource();
							var requestTask = client.GetStringAsync(url);

							if(Task.WaitAny(new Task[] {requestTask }, 150000) < 0)
							{
								cancelSource.Cancel();
								throw new Exception("The network request timed out. Please check your network connection.");
							}

							return requestTask.GetAwaiter().GetResult();
						});

					returnValue = JsonConvert.DeserializeObject<T> (response);
				}
			} catch (Exception ex) {
				throw ex;
			}

			return returnValue;
		}

		/// <summary>
		/// Posts the encoded data.
		/// </summary>
		/// <returns>The encoded data.</returns>
		/// <param name="url">URL.</param>
		/// <param name="content">Content.</param>
		/// <typeparam name="T">The 1st type parameter.</typeparam>
		public static async Task<T> PostEncodedData<T>(string url, string content)
		{
			string retVal = "";
			using (var handler = new HttpClientHandler ())
			using (var client = new HttpClient (handler)) {
				client.DefaultRequestHeaders.Add ("Accept", "application/json");				
				client.DefaultRequestHeaders.Add ("Cache-Control", "no-cache");	
				retVal = await Task.Run (() => {
					var cancelSource = new CancellationTokenSource ();
					var requestTask = client.PostAsync (url, new StringContent(content, Encoding.UTF8 ,"application/json"));

					if (Task.WaitAny (new Task[] { requestTask }, 150000) < 0) {
						cancelSource.Cancel ();
						throw new Exception ("The network request timed out. Please check your network connection.");
					}

					var responseTask = requestTask.GetAwaiter ().GetResult ().Content.ReadAsStringAsync ();

					if (Task.WaitAny (new Task[] { responseTask }, 150000) < 0) {
						cancelSource.Cancel ();
						throw new Exception ("The network request timed out. Please check your network connection.");
					}

					return responseTask.GetAwaiter ().GetResult ();
				});

			}
			return JsonConvert.DeserializeObject<T> (retVal);

		}
	}
}

