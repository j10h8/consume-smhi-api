using ConsumeSmhiApi.Models;
using System.Text.Json;

namespace ConsumeSmhiApi.Managers
{
    public class RainFallDataManager
    {
        // Gets the populated RainFallApiModel. 
        public async Task<RainFallApiModel?> GetPopulatedRainFallApiModel(int smhiStationKey)
        {
            SmhiApiCaller smhiApiCaller = new();

            string? response = await smhiApiCaller.GetRainFallJsonData(smhiStationKey);

            if (response != null)
            {
                return JsonSerializer.Deserialize<RainFallApiModel>(response);
            }

            throw new Exception("Error! Could not get populated RainFallApiModel.");
        }

        // Gets the current rain fall data period as a list of 2 DateTime objects, for the specified SMHI station. 
        public async Task<List<DateTime>> GetTimePeriod(int smhiStationKey)
        {
            RainFallApiModel? rainFallApiModel = await GetPopulatedRainFallApiModel(smhiStationKey);
            List<DateTime> timePeriod = new();

            if (rainFallApiModel != null && rainFallApiModel.value != null)
            {
                foreach (Value2? value in rainFallApiModel.value)
                {
                    if (value.from != 0)
                    {
                        DateTime dateTime = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

                        dateTime = dateTime.AddMilliseconds(value.from).ToLocalTime();

                        timePeriod.Add(dateTime);
                    }
                    if (value.to != 0)
                    {
                        DateTime dateTime = new(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

                        dateTime = dateTime.AddMilliseconds(value.to).ToLocalTime();

                        timePeriod.Add(dateTime);
                    }
                }

                if (timePeriod.Count >= 2)
                {
                    timePeriod = timePeriod.OrderBy(x => x).ToList();

                    if (timePeriod.Count == 2)
                    {
                        return timePeriod;
                    }
                    else if (timePeriod.Count > 2)
                    {
                        timePeriod.RemoveRange(1, timePeriod.Count - 2);

                        return timePeriod;
                    }
                }
            }

            throw new Exception("Error! Could not get time period.");
        }

        // Gets the total rain fall in mm for the specified SMHI station. 
        public async Task<decimal> GetTotalRainFall(int smhiStationKey)
        {
            decimal totalRainFall = 0;

            RainFallApiModel? rainFallApiModel = await GetPopulatedRainFallApiModel(smhiStationKey);

            if (rainFallApiModel != null && rainFallApiModel.value != null)
            {
                foreach (Value2? value in rainFallApiModel.value)
                {
                    if (value.value != null)
                    {
                        decimal decimalValue = decimal.Parse(value.value);

                        totalRainFall += decimalValue;
                    }
                }
                return totalRainFall;
            }

            throw new Exception("Error! Could not get average rain fall.");
        }
    }
}
