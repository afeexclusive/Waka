
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Waka.ViewModels;

namespace Waka.Services
{
    public class APIHelper
    {
        private HttpClient apiClient;

        public APIHelper()
        {
            InitializeClient();
        }

        private void InitializeClient()
        {
            //string api = ConfigurationManager.AppSettings["api"];
            apiClient = new HttpClient();
            apiClient.BaseAddress = new Uri("https://maps.googleapis.com/maps/api/place/findplacefromtext/");
            apiClient.DefaultRequestHeaders.Accept.Clear();
            apiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<PublicPlacesVM> GetFullAddress(string placeName)
        {
            var url = $"json?input={placeName}&inputtype=textquery&fields=formatted_address,name,geometry&key=AIzaSyCZoaDRV1zeWwVqppLxkRM8itTSyzP_jj0";

            try
            {
               
                using (var httpResponse = await apiClient.GetAsync(url, HttpCompletionOption.ResponseHeadersRead))  //ConfigureAwait(false)
                {
                    if (httpResponse == null)
                    {

                        throw new Exception("Could not retrieve location");
                    }
                    var content = await httpResponse.Content.ReadAsStringAsync();
                    var tasks = JsonConvert.DeserializeObject<PublicPlacesVM>(content);

                    return tasks;
                }
            }
            catch (Exception ex)
            {
                var e = ex;
                throw;

            }

        }

    }
}
