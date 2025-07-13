using LibTreino.Models.DTOs;

namespace LibTreino.Models.Commons
{
    public static class EnumExtensions
    {
        public static EnumDTO ToEnumDto<TEnum>(this TEnum enumValue) where TEnum : Enum
        {
            return new EnumDTO
            {
                Id = Convert.ToString(enumValue),
                Descricao = enumValue.ToString()
            };
        }
    }
}
