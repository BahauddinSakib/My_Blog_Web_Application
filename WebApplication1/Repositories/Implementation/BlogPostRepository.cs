﻿using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models.Domain;
using WebApplication1.Repositories.Interface;

namespace WebApplication1.Repositories.Implementation
{
    public class BlogPostRepository : IBlogPostRepository
    {
        private readonly ApplicationDbContext dbContext;

        public BlogPostRepository(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public async Task<BlogPost> CreateAsync(BlogPost blogPost)
        {
            await dbContext.BlogPosts.AddAsync(blogPost);
            await dbContext.SaveChangesAsync();
            return blogPost;
        }

        public async Task<BlogPost?> DeleteAsync(Guid id)
        {
            var existingBlogPost = await dbContext.BlogPosts.FirstOrDefaultAsync(x => x.Id == id);
            if (existingBlogPost != null)
            {
                dbContext.BlogPosts.Remove(existingBlogPost);
                await dbContext.SaveChangesAsync();
                return existingBlogPost;
            }
                return null;
        }

        public async Task<IEnumerable<BlogPost>> GetAllAsync()
        {
            return await dbContext.BlogPosts.Include(x=>x.Categories).ToListAsync();//will get all blogpost along categories
        }

        public async Task<BlogPost?> GetByIdAsync(Guid id)
        {
            return await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x=> x.Id==id);
        }

        public async Task<BlogPost?> GetByUrlHandleAsync(string urlHandle)
        {
            

            return await dbContext.BlogPosts.Include(x => x.Categories).FirstOrDefaultAsync(x => x.UrlHandle == urlHandle);
        }

        public async Task<BlogPost?> UpdateAsync(BlogPost blogPost)
        {


           var existingBlogPost =  await dbContext.BlogPosts.Include(x=> x.Categories).
                FirstOrDefaultAsync(x=> x.Id==blogPost.Id);

            if (existingBlogPost == null)
            {
                return null;
            }
            //update blogpost
            dbContext.Entry(existingBlogPost).CurrentValues.SetValues(blogPost);

            //update Categories
            existingBlogPost.Categories= blogPost.Categories;

            await dbContext.SaveChangesAsync();

            return blogPost;
        }
    }
}
