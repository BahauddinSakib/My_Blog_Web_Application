namespace WebApplication1.Models.Domain
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string UrlHandle { get; set; }

        public ICollection<BlogPost>BlogPosts { get; set; } //represents rel between cat and blogpost


    }
}
