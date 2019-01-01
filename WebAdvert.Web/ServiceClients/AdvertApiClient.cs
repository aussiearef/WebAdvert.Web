using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AdvertApi.Models;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace WebAdvert.Web.ServiceClients
{
    public class AdvertApiClient : IAdvertApiClient
    {
        private readonly HttpClient _client;
        private readonly IMapper _mapper;

        public AdvertApiClient(IConfiguration configuration, HttpClient client, IMapper mapper)
        {
            _client = client;
            _mapper = mapper;

            var createUrl = configuration.GetSection("AdvertApi").GetValue<string>("BaseUrl");
            _client.BaseAddress = new Uri(createUrl);
        }

        public async Task<AdvertResponse> Create(CreateAdvertModel model)
        {
            var advertApiModel = _mapper.Map<AdvertModel>(model);

            var jsonModel = JsonConvert.SerializeObject(advertApiModel);
            var response = await _client.PostAsync(new Uri($"{_client.BaseAddress}/create"),
                new StringContent(jsonModel, Encoding.UTF8, "application/json")).ConfigureAwait(false);
            var responseJson = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var createAdvertResponse = JsonConvert.DeserializeObject<CreateAdvertResponse>(responseJson);

            var advertResponse = _mapper.Map<AdvertResponse>(createAdvertResponse);

            return advertResponse;
        }

        public async Task<bool> Confirm(ConfirmAdvertRequest model)
        {
            var advertModel = _mapper.Map<ConfirmAdvertModel>(model);
            var jsonModel = JsonConvert.SerializeObject(advertModel);
            var response = await _client
                .PutAsync(new Uri($"{_client.BaseAddress}/confirm"),
                    new StringContent(jsonModel, Encoding.UTF8, "application/json"))
                .ConfigureAwait(false);
            return response.StatusCode == HttpStatusCode.OK;
        }
    }
}
