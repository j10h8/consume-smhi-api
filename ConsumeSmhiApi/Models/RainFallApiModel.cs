namespace ConsumeSmhiApi.Models
{
    public class RainFallApiModel
    {
        public long updated { get; set; }
        public Parameter2? parameter { get; set; }
        public Station2? station { get; set; }
        public Period2? period { get; set; }
        public Position[]? position { get; set; }
        public Link2[]? link { get; set; }
        public Value2[]? value { get; set; }
    }

    public class Parameter2
    {
        public string? key { get; set; }
        public string? name { get; set; }
        public string? summary { get; set; }
        public string? unit { get; set; }
    }

    public class Station2
    {
        public string? key { get; set; }
        public string? name { get; set; }
        public string? owner { get; set; }
        public string? ownerCategory { get; set; }
        public string? measuringStations { get; set; }
        public float height { get; set; }
    }

    public class Period2
    {
        public string? key { get; set; }
        public long from { get; set; }
        public long to { get; set; }
        public string? summary { get; set; }
        public string? sampling { get; set; }
    }

    public class Position
    {
        public long from { get; set; }
        public long to { get; set; }
        public float height { get; set; }
        public float latitude { get; set; }
        public float longitude { get; set; }
    }

    public class Link2
    {
        public string? href { get; set; }
        public string? rel { get; set; }
        public string? type { get; set; }
    }

    public class Value2
    {
        public long from { get; set; }
        public long to { get; set; }
        public string? _ref { get; set; }
        public string? value { get; set; }
        public string? quality { get; set; }
    }
}
