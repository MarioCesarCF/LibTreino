using Microsoft.OpenApi.Extensions;

namespace LibTreino.Models.DTOs
{
    public class EnumDTO
    {
        public string? Id { get; set; }
        public string Descricao { get; set; }

        public static List<EnumDTO> ToList<TEnum>() where TEnum : Enum
        {
            return Enum.GetValues(typeof(TEnum))
                .Cast<TEnum>()
                .Select(e => new EnumDTO
                {
                    Id = Convert.ToString(e),
                    Descricao = ((Enum)(object)e).GetDisplayName()
                })
                .ToList();
        }
    }
}
