using AutoMapper;
using Vintello.Services;
using Vintello.Common.Repositories;
using Moq;
using Vintello.Common.DTOs;
using Vintello.Common.EntityModel.PostgreSql;

namespace Vintello.Test.Unit;

public class CategoryServiceTests
{
    [Fact]
    public async Task CreateAsync_ShouldReturnRetrievedCategory_WhenCategoryIsCreated()
    {
        var createdCategoryDto = new CreatedCategoryDto { Name = "Говно", Description = "Хуйня" };
        var createdCategory = new Category { Id = 1, Name = "Говно", Description = "Хуйня" };

        var mockRepo = new Mock<ICategoryRepository>();
        var mockMapper = new Mock<IMapper>();

        mockRepo
            .Setup(m => m.CreateAsync(It.IsAny<Category>()))
            .ReturnsAsync(createdCategory);
        mockMapper
            .Setup(m => m.Map<Category>(It.IsAny<CreatedCategoryDto>()))
            .Returns(createdCategory);
        mockMapper
            .Setup(m => m.Map<RetrievedCategoryDto>(It.IsAny<Category>()))
            .Returns(new RetrievedCategoryDto { Id = 1, Name = "Говно", Description = "Хуйня" });

        CategoryService service = new CategoryService(mockRepo.Object, mockMapper.Object);
        var result = await service.CreateAsync(createdCategoryDto);

        Assert.NotNull(result);
        Assert.IsType<RetrievedCategoryDto>(result);
        Assert.Equal("Говно", result.Name);
    }
    
    [Fact]
    public async Task CreateAsync_ShouldReturnNull_WhenCategoryIsNotCreated()
    {
        var createdCategoryDto = new CreatedCategoryDto { Description = "Хуйня" };

        var mockRepo = new Mock<ICategoryRepository>();
        var mockMapper = new Mock<IMapper>();

        mockRepo
            .Setup(m => m.CreateAsync(It.IsAny<Category>()))
            .ReturnsAsync(null as Category);
        mockMapper
            .Setup(m => m.Map<Category>(It.IsAny<CreatedCategoryDto>()))
            .Returns(new Category { Description = "Хуйня" });

        CategoryService service = new CategoryService(mockRepo.Object, mockMapper.Object);
        var result = await service.CreateAsync(createdCategoryDto);

        Assert.Null(result);
    }

    [Fact]
    public async Task RetrieveAsync_ShouldReturnRetrievedCategoriesDto()
    {
        IEnumerable<RetrievedCategoriesDto> retrievedCategoriesDto = new List<RetrievedCategoriesDto>
        {
            new RetrievedCategoriesDto{Id = 1, Name = "Хуйня"},
            new RetrievedCategoriesDto{Id = 2, Name = "Хуйня"},
            new RetrievedCategoriesDto{Id = 3, Name = "Хуйня"}
        };
        IEnumerable<Category> retrievedCategories = new List<Category>
        {
            new Category{Id = 1, Name = "Хуйня"},
            new Category{Id = 2, Name = "Хуйня"},
            new Category{Id = 3, Name = "Хуйня"}
        };
        
        var mockRepo = new Mock<ICategoryRepository>();
        var mockMapper = new Mock<IMapper>();

        mockMapper
            .Setup(m => m.Map<IEnumerable<RetrievedCategoriesDto>>(It.IsAny<IEnumerable<Category>>()))
            .Returns(retrievedCategoriesDto);
        mockRepo
            .Setup(m => m.RetrieveAllAsync())
            .ReturnsAsync(retrievedCategories);

        CategoryService service = new CategoryService(mockRepo.Object, mockMapper.Object);

        var result = await service.RetrieveAsync();

        Assert.NotNull(result);
        Assert.Equal(retrievedCategoriesDto, result);
    }

    [Fact]
    public async Task RetrieveByIdAsync_ShouldReturnRetrievedCategoryDto_WhenCategoryIsFound()
    {
        var retrievedCategoryDto = new RetrievedCategoryDto{Id = 1, Name = "Хуйня"};
        
        var mockRepo = new Mock<ICategoryRepository>();
        var mockMapper = new Mock<IMapper>();

        mockRepo
            .Setup(m => m.RetrieveByIdAsync(1))
            .ReturnsAsync(new Category{Id = 1, Name = "Хуйня"});
        mockMapper
            .Setup(m => m.Map<RetrievedCategoryDto>(It.IsAny<Category>()))
            .Returns(retrievedCategoryDto);

        CategoryService service = new CategoryService(mockRepo.Object, mockMapper.Object);

        var result = await service.RetrieveByIdAsync(1);

        Assert.IsType<RetrievedCategoryDto>(result);
        Assert.NotNull(result);
    }

    [Fact]
    public async Task RetrieveByIdAsync_ShouldReturnNull_WhenCategoryIsNotFound()
    {
        var mockRepo = new Mock<ICategoryRepository>();
        var mockMapper = new Mock<IMapper>();

        mockRepo
            .Setup(m => m.RetrieveByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(null as Category);

        CategoryService service = new CategoryService(mockRepo.Object, mockMapper.Object);

        var result = await service.RetrieveByIdAsync(It.IsAny<int>());
        
        Assert.Null(result);
    }
    
    
}