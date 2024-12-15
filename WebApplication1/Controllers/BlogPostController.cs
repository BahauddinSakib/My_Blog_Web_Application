using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models.Domain;
using WebApplication1.Models.DTO;
using WebApplication1.Repositories.Interface;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostRepository blogPostRepository;

        public BlogPostController(IBlogPostRepository blogPostRepository) {
            this.blogPostRepository = blogPostRepository;
        }

        //{apibaseurl}/api/blogposts
        [HttpPost]
        public async Task<IActionResult> CreateBlogPost([FromBody] CreateBlogPostRequestDto request)

        { 
            

            //convert DTO to Domain
            var blogPost = new BlogPost
            {
                Author = request.Author,
                Content = request.Content,
                FeaturedImageUrl = request.FeaturedImageUrl,
                IsVisible = request.IsVisible,
                PublishedDate = request.PublishedDate,
                ShortDescription = request.ShortDescription,
                Title = request.Title,
                Urlhandle = request.Urlhandle

            };

                 blogPost = await blogPostRepository.CreateAsync(blogPost);

            var response = new BlogPostDto
            {
                Id = blogPost.Id,
                Author = blogPost.Author,
                Content = blogPost.Content,
                FeaturedImageUrl= blogPost.FeaturedImageUrl,
                IsVisible = blogPost.IsVisible,
                PublishedDate = blogPost.PublishedDate,
                ShortDescription = blogPost.ShortDescription,
                Title = blogPost.Title,
                Urlhandle = blogPost.Urlhandle
            };

            return Ok(response);
        }


        //GET: {apibaseurl}/api/BlogPost
        [HttpGet]
        public async Task<IActionResult> GetAllBlogPosts()
        {
            var blogPosts =  await blogPostRepository.GetAllAsync();
            //convert domain model to DTO
            var response = new List<BlogPostDto>();
            foreach(var blogPost in blogPosts)
            {
                response.Add(new BlogPostDto
                {
                    Id = blogPost.Id,
                    Author = blogPost.Author,
                    Content = blogPost.Content,
                    FeaturedImageUrl = blogPost.FeaturedImageUrl,
                    IsVisible = blogPost.IsVisible,
                    PublishedDate = blogPost.PublishedDate,
                    ShortDescription = blogPost.ShortDescription,
                    Title = blogPost.Title,
                    Urlhandle = blogPost.Urlhandle

                });
            }
            return Ok(response);
        }



    }
}
