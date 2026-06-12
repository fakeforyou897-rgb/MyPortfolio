using MyPortfolio.Models.Entities;
using MyPortfolio.Models.ViewModels;

namespace MyPortfolio.Services.Interfaces
{
    /// <summary>
    /// Service for mapping between entities and view models
    /// </summary>
    public interface IMappingService
    {
        // Project mappings
        Project MapToEntity(ProjectViewModel viewModel);
        ProjectViewModel MapToViewModel(Project entity);
        void UpdateEntity(Project entity, ProjectViewModel viewModel);
        
        // BlogPost mappings
        BlogPost MapToEntity(BlogPostViewModel viewModel);
        BlogPostViewModel MapToViewModel(BlogPost entity);
        void UpdateEntity(BlogPost entity, BlogPostViewModel viewModel);
        
        // ContactMessage mappings
        ContactMessage MapToEntity(ContactMessageViewModel viewModel);
        ContactMessageViewModel MapToViewModel(ContactMessage entity);
    }
}
