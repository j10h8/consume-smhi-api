namespace ConsumeSmhiApi
{
    public static class SmhiApiInitializer
    {
        public static HttpClient HttpClient { get; set; } = new();

        // Initializes the HttpClient with the base address for the SMHI API. 
        public static void InitializeClient()
        {
            HttpClient.BaseAddress = new Uri("https://opendata-download-metobs.smhi.se/api/");
        }
    }
}
