namespace ConsumeSmhiApi
{
    public class SmhiApiCaller
    {
        // Gets the temperature JSON data from SMHI core stations. 
        public async Task<string?> GetTemperatureJsonData()
        {
            HttpResponseMessage response = await SmhiApiInitializer.HttpClient.GetAsync("version/1.0/parameter/1/station-set/all/period/latest-hour/data.json?measuringStations=core");

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                return responseContent;
            }

            throw new Exception("Error! Could not get temperature JSON data.");
        }

        // Gets the rain fall JSON data from the specified SMHI station. 
        public async Task<string?> GetRainFallJsonData(int smhiStationKey)
        {
            HttpResponseMessage response = await SmhiApiInitializer.HttpClient.GetAsync($"version/1.0/parameter/23/station/{smhiStationKey}/period/latest-months/data.json");

            if (response.IsSuccessStatusCode)
            {
                string responseContent = await response.Content.ReadAsStringAsync();

                return responseContent;
            }

            throw new Exception("Error! Could not get rain fall JSON data.");
        }
    }
}
