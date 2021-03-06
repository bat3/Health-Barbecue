﻿using MetadataDatabase.Controllers;
using MetadataDatabase.Convertor;
using MetadataDatabase.Data;
using MetadataDatabase.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.IO.Compression;
using System.Diagnostics;

namespace MetadataDatabase.Services
{
    public class PacsService: IPacsService
    {
        private readonly HttpClient client;
        private readonly PacsConfiguration settings;

        public PacsService(IOptions<PacsConfiguration> settings)
        {
            this.settings = settings.Value;
            this.client = new HttpClient();
        }

        public IEnumerable<SeriesDto> GetSeriesList()
        {
            Task<IEnumerable<QidoSeries>> task = this.GetSeriesListAsync();
            return task.Result?.ToDto();
        }

        public SeriesDto GetMetadataSeries(SeriesDto series)
        {
            Task<QidoSeries> fetchSeriesMetadataTask = this.GetSeriesMetadataAsync(
                series.StudyInstanceUID,
                series.SeriesInstanceUID);
            fetchSeriesMetadataTask.Wait();
            return fetchSeriesMetadataTask.Result?.ToDto();
        }

        public string DownloadSeries(string seriesUID)
        {
            string zipFilzName = $"{seriesUID}.zip";
            
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, $"{this.settings.Protocol}://{this.settings.Host}:{this.settings.Port}/tools/find");
            OrthancFindRequest orthancFindRequest = new OrthancFindRequest() {
                Level = "series",
                Limit = 2,
                Query = new Dictionary<string, string>()
            };
            orthancFindRequest.Query.Add("SeriesInstanceUID", $"{seriesUID}");
            var jsonString = JsonSerializer.Serialize(orthancFindRequest);
            httpRequestMessage.Content = new StringContent(jsonString);
            var task = this.client.SendAsync(httpRequestMessage);
            var res = task.Result.Content.ReadAsStringAsync().Result;
            var listOfSeries = JsonSerializer.Deserialize<List<string>>(res);
            if(listOfSeries.Count == 0) { throw new Exception($"No SeriesInstanceUID {seriesUID} matching in Orthanc database."); }
            var orthancSeriesId = listOfSeries[0];
            var task2 = this.client.GetByteArrayAsync($"{this.settings.Protocol}://{this.settings.Host}:{this.settings.Port}/series/{orthancSeriesId}/archive");
            File.WriteAllBytes(zipFilzName, task2.Result);
            return zipFilzName;
        }

        private async Task<IEnumerable<QidoSeries>> GetSeriesListAsync()
        {
            var streamtask = client.GetStreamAsync($"{this.settings.Protocol}://{this.settings.Host}:{this.settings.Port}/{this.settings.Path}/series");
            var pacsSeriesList = await JsonSerializer.DeserializeAsync<IEnumerable<QidoSeries>>(await streamtask);
            return pacsSeriesList;
        }

        private async Task<QidoSeries> GetSeriesMetadataAsync(string studiesUid, string seriesUid)
        {
            var streamtask = client.GetStreamAsync(
                $"{this.settings.Protocol}://{this.settings.Host}:{this.settings.Port}/{this.settings.Path}/studies/{studiesUid}/series/{seriesUid}/metadata");
            var seriesMetadata = await JsonSerializer.DeserializeAsync<IEnumerable<QidoSeries>>(await streamtask);
            return seriesMetadata.FirstOrDefault();
        }
    }

    public class OrthancFindRequest
    {
        public string Level { get; set; }
        public int Limit { get; set; }
        public Dictionary<string, string> Query { get; set; }
    }
}
