using AutoMapper;
using webAPI.DTOs;
using webAPI.enums;
using webAPI.Models;

namespace webAPI.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Budget Mappings
            CreateMap<Budget, BudgetDTO>().ReverseMap();

            // Income Mappings
            CreateMap<Income, IncomeDTO>().ReverseMap();

            // Map Expense -> ExpenseDTO (for reading data)
            CreateMap<Expense, ExpenseDTO>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.ToString())) // Enum to String
                .ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.Month)); // Enum to Enum

            // Map ExpenseDTO -> Expense (for creating/updating data)
            CreateMap<ExpenseDTO, Expense>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => ParseExpenseCategory(src.Category))) // String to Enum
                .ForMember(dest => dest.Month, opt => opt.MapFrom(src => src.Month)); // Enum to Enum
        }

        private static ExpenseCategory ParseExpenseCategory(string category)
        {
            if (Enum.TryParse<ExpenseCategory>(category, true, out var parsedCategory))
            {
                return parsedCategory;
            }

            throw new ArgumentException($"Invalid category value: {category}");
        }
    }
}
