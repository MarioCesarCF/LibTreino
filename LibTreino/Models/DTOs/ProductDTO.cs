namespace LibTreino.Models.DTOs
{
    public class ProductDTO
    {
        public string? Id { get; set; }
        public string? Name { get; set; }
        public int Amount { get; set; }
        public EnumDTO Unity { get; set; }
        public EnumDTO Situation { get; set; }
    }
}
