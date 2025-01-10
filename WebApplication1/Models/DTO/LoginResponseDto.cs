namespace WebApplication1.Models.DTO
{
    public class LoginResponseDto
    {
        //when everything was successful
        public string Email {  get; set; }
        public string Token { get; set; }

        public List<string> Roles { get; set; }
    }
}
