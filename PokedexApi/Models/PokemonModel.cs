namespace PokedexApi.Models
{
    public class PokemonModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Height { get; set; }
        public int Weight { get; set; }
        public string ImageUrl { get; set; } = string.Empty;
    }
}
