namespace Vintello.Common.DTOs;

public class CreateCategoryDto
{
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}

public class RetriveCategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
}