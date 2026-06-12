using MyPortfolio.Extensions;
using MyPortfolio.Models.Entities;
using MyPortfolio.Models.ViewModels;
using MyPortfolio.Services.Interfaces;

namespace MyPortfolio.Services
{
    public class MappingService : IMappingService
    {
        // Project mappings
        public Project MapToEntity(ProjectViewModel viewModel)
        {
            var slug = !string.IsNullOrWhiteSpace(viewModel.Title) 
                ? viewModel.Title.ToSlug() 
                : Guid.NewGuid().ToString();

            return new Project
            {
                Id = viewModel.Id ?? Guid.NewGuid(),
                Title = viewModel.Title,
                Slug = slug,
                Description = viewModel.Description,
                ShortDescription = viewModel.ShortDescription,
                Technologies = viewModel.Technologies,
                ImageUrl = viewModel.ImageUrl,
                GitHubUrl = viewModel.GitHubUrl,
                LiveDemoUrl = viewModel.LiveDemoUrl,
                Status = viewModel.Status,
                Priority = viewModel.Priority,
                StartDate = viewModel.StartDate,
                EndDate = viewModel.EndDate,
                DisplayOrder = viewModel.DisplayOrder,
                IsFeatured = viewModel.IsFeatured,
                Rating = viewModel.Rating
            };
        }

        public ProjectViewModel MapToViewModel(Project entity)
        {
            return new ProjectViewModel
            {
                Id = entity.Id,
                Title = entity.Title,
                Description = entity.Description,
                ShortDescription = entity.ShortDescription,
                Technologies = entity.Technologies,
                ImageUrl = entity.ImageUrl,
                GitHubUrl = entity.GitHubUrl,
                LiveDemoUrl = entity.LiveDemoUrl,
                Status = entity.Status,
                Priority = entity.Priority,
                StartDate = entity.StartDate,
                EndDate = entity.EndDate,
                DisplayOrder = entity.DisplayOrder,
                IsFeatured = entity.IsFeatured,
                Rating = entity.Rating
            };
        }

        public void UpdateEntity(Project entity, ProjectViewModel viewModel)
        {
            entity.Title = viewModel.Title;
            entity.Slug = viewModel.Title.ToSlug();
            entity.Description = viewModel.Description;
            entity.ShortDescription = viewModel.ShortDescription;
            entity.Technologies = viewModel.Technologies;
            entity.ImageUrl = viewModel.ImageUrl;
            entity.GitHubUrl = viewModel.GitHubUrl;
            entity.LiveDemoUrl = viewModel.LiveDemoUrl;
            entity.Status = viewModel.Status;
            entity.Priority = viewModel.Priority;
            entity.StartDate = viewModel.StartDate;
            entity.EndDate = viewModel.EndDate;
            entity.DisplayOrder = viewModel.DisplayOrder;
            entity.IsFeatured = viewModel.IsFeatured;
            entity.Rating = viewModel.Rating;
        }

        // BlogPost mappings
        public BlogPost MapToEntity(BlogPostViewModel viewModel)
        {
            var slug = !string.IsNullOrWhiteSpace(viewModel.Slug) 
                ? viewModel.Slug.ToSlug() 
                : viewModel.Title.ToSlug();

            return new BlogPost
            {
                Id = viewModel.Id ?? Guid.NewGuid(),
                Title = viewModel.Title,
                Slug = slug,
                Content = viewModel.Content,
                Summary = viewModel.Summary,
                Author = viewModel.Author,
                ImageUrl = viewModel.ImageUrl,
                FeaturedImageUrl = viewModel.FeaturedImageUrl,
                Status = viewModel.Status,
                PublishedAt = viewModel.PublishedAt,
                ScheduledPublishAt = viewModel.ScheduledPublishAt,
                ReadingTimeMinutes = viewModel.ReadingTimeMinutes > 0 
                    ? viewModel.ReadingTimeMinutes 
                    : viewModel.Content.CalculateReadingTime(),
                AllowComments = viewModel.AllowComments,
                IsFeatured = viewModel.IsFeatured,
                MetaTitle = viewModel.MetaTitle,
                MetaDescription = viewModel.MetaDescription,
                MetaKeywords = viewModel.MetaKeywords
            };
        }

        public BlogPostViewModel MapToViewModel(BlogPost entity)
        {
            return new BlogPostViewModel
            {
                Id = entity.Id,
                Title = entity.Title,
                Slug = entity.Slug,
                Content = entity.Content,
                Summary = entity.Summary,
                Author = entity.Author,
                ImageUrl = entity.ImageUrl,
                FeaturedImageUrl = entity.FeaturedImageUrl,
                Status = entity.Status,
                PublishedAt = entity.PublishedAt,
                ScheduledPublishAt = entity.ScheduledPublishAt,
                ReadingTimeMinutes = entity.ReadingTimeMinutes,
                AllowComments = entity.AllowComments,
                IsFeatured = entity.IsFeatured,
                MetaTitle = entity.MetaTitle,
                MetaDescription = entity.MetaDescription,
                MetaKeywords = entity.MetaKeywords
            };
        }

        public void UpdateEntity(BlogPost entity, BlogPostViewModel viewModel)
        {
            entity.Title = viewModel.Title;
            entity.Slug = !string.IsNullOrWhiteSpace(viewModel.Slug) 
                ? viewModel.Slug.ToSlug() 
                : viewModel.Title.ToSlug();
            entity.Content = viewModel.Content;
            entity.Summary = viewModel.Summary;
            entity.Author = viewModel.Author;
            entity.ImageUrl = viewModel.ImageUrl;
            entity.FeaturedImageUrl = viewModel.FeaturedImageUrl;
            entity.Status = viewModel.Status;
            entity.PublishedAt = viewModel.PublishedAt;
            entity.ScheduledPublishAt = viewModel.ScheduledPublishAt;
            entity.ReadingTimeMinutes = viewModel.ReadingTimeMinutes > 0 
                ? viewModel.ReadingTimeMinutes 
                : viewModel.Content.CalculateReadingTime();
            entity.AllowComments = viewModel.AllowComments;
            entity.IsFeatured = viewModel.IsFeatured;
            entity.MetaTitle = viewModel.MetaTitle;
            entity.MetaDescription = viewModel.MetaDescription;
            entity.MetaKeywords = viewModel.MetaKeywords;
        }

        // ContactMessage mappings
        public ContactMessage MapToEntity(ContactMessageViewModel viewModel)
        {
            return new ContactMessage
            {
                Id = Guid.NewGuid(),
                Name = viewModel.Name,
                Email = viewModel.Email,
                Subject = viewModel.Subject,
                Message = viewModel.Message,
                Status = Models.Enums.MessageStatus.New,
                IsRead = false
            };
        }

        public ContactMessageViewModel MapToViewModel(ContactMessage entity)
        {
            return new ContactMessageViewModel
            {
                Name = entity.Name,
                Email = entity.Email,
                Subject = entity.Subject,
                Message = entity.Message
            };
        }
    }
}
