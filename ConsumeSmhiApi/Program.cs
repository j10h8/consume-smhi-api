using ConsumeSmhiApi;
using ConsumeSmhiApi.Managers;

SmhiApiInitializer.InitializeClient();

TemperatureDataManager temperatureDataManager = new();
decimal averageTemperature = await temperatureDataManager.GetAverageTemperature();

// Gets the total rainfall in SMHI station 53430/Lund for the last available time period. 
RainFallDataManager rainFallDataManager = new();
decimal totalRainFall = await rainFallDataManager.GetTotalRainFall(53430);
List<DateTime> timePeriod = await rainFallDataManager.GetTimePeriod(53430);

// Prints the average temperature in Sweden for the last hour. 
Console.WriteLine($"The average temperature in Sweden for the last hour was {averageTemperature.ToString("0.000000")} degrees Celsius.");
Console.WriteLine();

// Prints the total rainfall in Lund for the last available time period. 
Console.WriteLine($"Between {timePeriod[0].ToString("yyyy-MM-dd")} and {timePeriod[1].ToString("yyyy-MM-dd")} the total rainfall in Lund was {totalRainFall.ToString("0.0")} mm.");
Console.WriteLine();

// Prints a list of current (once per hour-values) air temperatures in Celsius from SMHI core stations with a 0.1 second pause between each print. 
Console.WriteLine("List of temperatures from SMHI core stations.");
await temperatureDataManager.PrintTemperaturesWithPause();
