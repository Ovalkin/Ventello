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
    public async Task CreateAsync_CategoryCreated_ReturnRetrievedCategory()
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
    public async Task CreateAsync_CategoryNotCreated_ReturnNull()
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
    public async Task RetrieveAsync_ReturnRetrievedCategoriesDto()
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
    public async Task RetrieveByIdAsync_CategoryFound_ReturnRetrievedCategoryDto()
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
    public async Task RetrieveByIdAsync_CategoryNotFound_ReturnNull()
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

    [Fact]
    public async Task UpdateAsync_CategoryNotFound_ReturnNull()
    {
        var mockRepo = new Mock<ICategoryRepository>();
        var mockMapper = new Mock<IMapper>();

        mockRepo
            .Setup(m => m.RetrieveByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(null as Category);

        CategoryService service = new CategoryService(mockRepo.Object, mockMapper.Object);
        var result = await service.UpdateAsync(1, new UpdatedCategoryDto());
        
        Assert.Null(result);
    }

    [Fact]
    public async Task UpdateAsync_CategoryNotUpdated_ReturnFalse()
    {
        UpdatedCategoryDto updatedCategoryDto = new UpdatedCategoryDto{Name = "Хех"};
        Category category = new Category { Id = 1, Name = "Хух" };
        Category updatedCategory = new Category { Id = 1, Name = "Хeх" };
        
        var mockRepo = new Mock<ICategoryRepository>();
        var mockMapper = new Mock<IMapper>();
        
        mockRepo
            .Setup(m => m.RetrieveByIdAsync(1))
            .ReturnsAsync(category);
        mockMapper
            .Setup(m => m.Map(It.IsAny<UpdatedCategoryDto>(), It.IsAny<Category>()))
            .Returns(updatedCategory);
        mockRepo
            .Setup(m => m.UpdateAsync(1, updatedCategory))
            .ReturnsAsync(null as Category);

        CategoryService service = new CategoryService(mockRepo.Object, mockMapper.Object);

        var result = await service.UpdateAsync(1, updatedCategoryDto);
        
        Assert.False(result);
    }
    
    [Fact]
    public async Task UpdateAsync_CategoryUpdated_ReturnTrue()
    {
        UpdatedCategoryDto updatedCategoryDto = new UpdatedCategoryDto{Name = "Хех"};
        Category category = new Category { Id = 1, Name = "Хух" };
        Category updatedCategory = new Category { Id = 1, Name = "Хeх" };
        
        var mockRepo = new Mock<ICategoryRepository>();
        var mockMapper = new Mock<IMapper>();
        
        mockRepo
            .Setup(m => m.RetrieveByIdAsync(1))
            .ReturnsAsync(category);
        mockMapper
            .Setup(m => m.Map(It.IsAny<UpdatedCategoryDto>(), It.IsAny<Category>()))
            .Returns(updatedCategory);
        mockRepo
            .Setup(m => m.UpdateAsync(1, updatedCategory))
            .ReturnsAsync(new Category());

        CategoryService service = new CategoryService(mockRepo.Object, mockMapper.Object);

        var result = await service.UpdateAsync(1, updatedCategoryDto);
        
        Assert.True(result);
    }

    [Fact]
    public async Task DeleteAsync_CategoryNotFound_ReturnNull()
    {
        var mockRepo = new Mock<ICategoryRepository>();
        var mockMapper = new Mock<IMapper>();

        mockRepo
            .Setup(m => m.RetrieveByIdAsync(1))
            .ReturnsAsync(null as Category);

        CategoryService service = new CategoryService(mockRepo.Object, mockMapper.Object);

        var result = await service.DeleteAsync(1);
        
        Assert.Null(result);
    }
    
    [Fact]
    public async Task DeleteAsync_CategoryNotDeleted_ReturnFalse()
    {
        Category category = new Category { Id = 1, Name = "Хух" };
        var mockRepo = new Mock<ICategoryRepository>();
        var mockMapper = new Mock<IMapper>();

        mockRepo
            .Setup(m => m.RetrieveByIdAsync(1))
            .ReturnsAsync(category);
        mockRepo
            .Setup(m => m.DeleteAsync(category))
            .ReturnsAsync(false);

        CategoryService service = new CategoryService(mockRepo.Object, mockMapper.Object);

        var result = await service.DeleteAsync(1);
        
        Assert.False(result);
    }
    
    [Fact]
    public async Task DeleteAsync_CategoryDeleted_ReturnTrue()
    {
        Category category = new Category { Id = 1, Name = "Хух" };
        var mockRepo = new Mock<ICategoryRepository>();
        var mockMapper = new Mock<IMapper>();

        mockRepo
            .Setup(m => m.RetrieveByIdAsync(1))
            .ReturnsAsync(category);
        mockRepo
            .Setup(m => m.DeleteAsync(category))
            .ReturnsAsync(true);

        CategoryService service = new CategoryService(mockRepo.Object, mockMapper.Object);

        var result = await service.DeleteAsync(1);
        
        Assert.True(result);
    }
}