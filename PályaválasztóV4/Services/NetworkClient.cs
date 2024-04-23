
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using NetWCEx;
using PalyavalsztoV4.Models.V41;

namespace Lib
{
	internal class NetworkClient : IDisposable
	{
		private readonly string _baseURL;
		private readonly HttpClient _httpClient;

		public void Dispose()
		{ }

		public NetworkClient() : this("https://localhost:7155/api")
		{ }

		public NetworkClient(string BaseURL)
		{
			_baseURL = BaseURL;
			_httpClient = new HttpClient();
		}

		public T Get<T>(string queryString, string? token = null)
		{
			T queryResult;
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, _baseURL + queryString);
			if (token != null)
			{
				request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			HttpResponseMessage response = _httpClient.Send(request);
			if (response.IsSuccessStatusCode)
			{
				string content = response.Content.ReadAsStringAsync().Result;
				APIResponse result = JsonConvert.DeserializeObject<APIResponse>(content);
				if (typeof(T) == typeof(APIResponse))
				{
					return JsonConvert.DeserializeObject<T>(content);
				}
				else
				{
					if (result.Data != null)
					{
						return (T)result.Data;
					}
					return default(T);
				}
			}
			else
			{
				string error = response.Content.ReadAsStringAsync().Result;
				throw new NetworkCommunicationException(response.StatusCode, $"Kommunikációs hiba:\r\nResponse code: {response.StatusCode}\r\nMessage: {error}");
			}
		}

		public async Task<T> GetAsync<T>(string queryString, string? token = null)
		{
			T queryResult;
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, _baseURL + queryString);
			if (token != null)
			{
				request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			HttpResponseMessage response = await _httpClient.SendAsync(request);
			if (response.IsSuccessStatusCode)
			{
				string content = response.Content.ReadAsStringAsync().Result;
				APIResponse result = JsonConvert.DeserializeObject<APIResponse>(content);
				if (typeof(T) == typeof(APIResponse))
				{
					return JsonConvert.DeserializeObject<T>(content);
				}
				else
				{
					if (result.Data != null)
					{
						return JsonConvert.DeserializeObject<T>(result.Data.ToString());
					}
					return default(T);
				}
			}
			else
			{
				string error = await response.Content.ReadAsStringAsync();
				throw new NetworkCommunicationException(response.StatusCode, $"Kommunikációs hiba: {error}");
			}
		}
		public async Task<TResponse> PostAsync<TRequest, TResponse>(string queryString, TRequest data, string? token = null)
		{
			HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, _baseURL + queryString);
			if (token != null)
			{
				request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
			}
			request.Content = JsonContent.Create(data, typeof(TRequest), MediaTypeHeaderValue.Parse("application/json"));
			HttpResponseMessage response = await _httpClient.SendAsync(request);

			if (!response.IsSuccessStatusCode)
			{
				string error = await response.Content.ReadAsStringAsync();
				throw new NetworkCommunicationException(response.StatusCode, $"Státusz kód: {response.StatusCode}" +
					$"\r\nKommunikációs hiba: {error}");
			}
			else
			{
				if (typeof(TResponse) ==  typeof(APIResponse<TResponse>))
                {
                    TResponse rp = await response.Content.ReadFromJsonAsync<TResponse>();
                    return rp;
                }
                else
                {
                    APIResponse<TResponse> rp = await response.Content.ReadFromJsonAsync<APIResponse<TResponse>>();
                    if (rp.Data != null)
                    {
                        return rp.Data;
                    }
                    else
                    {
                        return default(TResponse);
                    }
                }
			}
		}
	}
}
