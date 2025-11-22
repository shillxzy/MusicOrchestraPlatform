namespace MusicOrchestraPlatform.Aggregator.DTOs
{
    public class PerformerDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } = default!;
        public string Instrument { get; set; } = default!;
    }

    public class InstrumentImageDto
    {
        public int Id { get; set; }
        public string Url { get; set; } = default!;
    }

    public class InstrumentDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public List<InstrumentImageDto> Images { get; set; } = new();
    }

    public class CompositionDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public List<PerformerDto> Performers { get; set; } = new();
    }

    public class ConcertProgramDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public List<CompositionDto> Compositions { get; set; } = new();
    }

    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public decimal Price { get; set; }
    }
}
