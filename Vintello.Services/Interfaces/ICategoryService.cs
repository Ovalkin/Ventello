using Vintello.Common.DTOs;

namespace Vintello.Services;

public interface ICategoryService
{
    Task<RetriveCategoryDto?> RetriveByIdAsync(int id);
}