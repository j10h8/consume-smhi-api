using ConsumeSmhiApi.Models;
using System.Text.Json;

namespace ConsumeSmhiApi.Managers
{
    public class TemperatureDataManager
    {
        // Populates the TemperatureApiModel.
        public async Task<TemperatureApiModel?> GetPopulatedTemperatureApiModel()
        {
            SmhiApiCaller smhiApiCaller = new();

            string? response = await smhiApiCaller.GetTemperatureJsonData();

            if (response != null)
            {
                return JsonSerializer.Deserialize<TemperatureApiModel>(response);
            }

            throw new Exception("Error! Could not get populated TemperatureApiModel.");
        }

        // Gets the average temperature in Sweden for the last hour. 
        public async Task<decimal> GetAverageTemperature()
        {
            int numberOfStationsWithTemperatureValues = await GetnumberOfStationsWithTemperatureValues();

            if (numberOfStationsWithTemperatureValues > 0)
            {
                decimal averageTemperature = await GetSumOfAllTemperatures() / numberOfStationsWithTemperatureValues;

                return averageTemperature;
            }

            throw new Exception("Error! Could not get average temperature.");
        }

        // Gets the number of stations that have temperature values. 
        public async Task<int> GetnumberOfStationsWithTemperatureValues()
        {
            int numberOfStationsWithTemperatureValues = 0;

            TemperatureApiModel? temperatureApiModel = await GetPopulatedTemperatureApiModel();

            if (temperatureApiModel != null && temperatureApiModel.station != null)
            {
                foreach (Station? station in temperatureApiModel.station)
                {
                    if (station.value != null)
                    {
                        foreach (var value in station.value)
                        {
                            if (value.value != null)
                            {
                                numberOfStationsWithTemperatureValues++;
                            }
                        }
                    }
                }
            }

            return numberOfStationsWithTemperatureValues;
        }

        // Gets the sum of all temperatures. 
        public async Task<decimal> GetSumOfAllTemperatures()
        {
            decimal sumOfAllTemperatures = 0;

            TemperatureApiModel? temperatureApiModel = await GetPopulatedTemperatureApiModel();

            if (temperatureApiModel != null && temperatureApiModel.station != null)
            {
                foreach (Station? station in temperatureApiModel.station)
                {
                    if (station.value != null)
                    {
                        foreach (var value in station.value)
                        {
                            if (value.value != null)
                            {
                                sumOfAllTemperatures += decimal.Parse(value.value);
                            }
                        }
                    }
                }

                return sumOfAllTemperatures;
            }

            throw new Exception("Error! Could not get sum of all temperatures.");
        }

        // Prints a list of current (once per hour-values) air temperatures in Celsius from SMHI core stations with a 0.1 second pause between each print.
        public async Task PrintTemperaturesWithPause()
        {
            TemperatureApiModel? temperatureApiModel = await GetPopulatedTemperatureApiModel();

            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            CancellationToken cancellationToken = cancellationTokenSource.Token;

            if (temperatureApiModel != null && temperatureApiModel.station != null)
            {
                foreach (Station? station in temperatureApiModel.station)
                {
                    if (station.value != null)
                    {
                        foreach (var value in station.value)
                        {
                            if (value.value != null)
                            {
                                Console.WriteLine($"{station.name}: {value.value.ToString()}");
                                await Task.Delay(100, cancellationToken);
                            }
                        }
                    }
                    if (Console.KeyAvailable)
                    {
                        Console.ReadKey(true);
                        cancellationTokenSource.Cancel();
                        break;
                    }
                }

                Console.WriteLine("Press any key to close console...");
                Console.ReadKey(true);
            }
        }
    }
}
