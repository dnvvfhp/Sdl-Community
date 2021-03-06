﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Sdl.Community.GSVersionFetch.Helpers;
using Sdl.Community.GSVersionFetch.Model;

namespace Sdl.Community.GSVersionFetch.Service
{
	public class ProjectService
	{
		public async Task<ProjectResponse> GetGsProjects()
		{
			try
			{
				using (var httpClient = new HttpClient())
				{
					var request = new HttpRequestMessage(HttpMethod.Get, new Uri(ApiUrl.GetProjects()));
					ApiUrl.AddRequestHeaders(httpClient, request);

					var responseMessage = await httpClient.SendAsync(request);
					var projectsResponse = await responseMessage.Content.ReadAsStringAsync();
					if (responseMessage.StatusCode == HttpStatusCode.OK)
					{
						return JsonConvert.DeserializeObject<ProjectResponse>(projectsResponse);
					}
				}
			}
			catch (Exception e)
			{
				// here we'll add loging
			}
			return new ProjectResponse();
		}

		public async Task<List<GsFile>> GetProjectFiles(string projectId)
		{
			try
			{
				using (var httpClient = new HttpClient())
				{
					var request = new HttpRequestMessage(HttpMethod.Get, new Uri(ApiUrl.GetProjectFiles(projectId)));
					ApiUrl.AddRequestHeaders(httpClient, request);

					var responseMessage = await httpClient.SendAsync(request);
					var filesResponse = await responseMessage.Content.ReadAsStringAsync();
					if (responseMessage.StatusCode == HttpStatusCode.OK)
					{
						return JsonConvert.DeserializeObject<List<GsFile>>(filesResponse);
					}
				}
			}
			catch (Exception e)
			{
				// here we'll add loging
			}
			return new List<GsFile>();
		}

		public async Task<List<GsFileVersion>> GetFileVersion(string languageFileId)
		{
			try
			{
				using (var httpClient = new HttpClient())
				{
					var request = new HttpRequestMessage(HttpMethod.Get, new Uri(ApiUrl.GetFileVersions(languageFileId)));
					ApiUrl.AddRequestHeaders(httpClient, request);

					var responseMessage = await httpClient.SendAsync(request);
					var filesResponse = await responseMessage.Content.ReadAsStringAsync();
					if (responseMessage.StatusCode == HttpStatusCode.OK)
					{
						return JsonConvert.DeserializeObject<List<GsFileVersion>>(filesResponse);
					}
				}
			}
			catch (Exception e)
			{
				// here we'll add loging
			}
			return new List<GsFileVersion>();
		}

		public async Task<byte[]> DownloadFileVersion(string projectId, string languageFileId, int version)
		{
			using (var httpClient = new HttpClient())
			{
				var request = new HttpRequestMessage(HttpMethod.Get, new Uri(ApiUrl.DownloadFileVersion(projectId,languageFileId,version)));
				ApiUrl.AddRequestHeaders(httpClient, request);

				var responseMessage = await httpClient.SendAsync(request);
				var fileResponse = await responseMessage.Content.ReadAsByteArrayAsync();
				if (responseMessage.StatusCode == HttpStatusCode.OK)
				{
					return fileResponse;
				}
				throw new Exception(responseMessage.StatusCode.ToString());
			}
		}
	}
}
