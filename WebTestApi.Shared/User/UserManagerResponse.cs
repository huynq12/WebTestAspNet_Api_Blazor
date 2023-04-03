namespace WebTestApi.User
{
    public class UserManagerResponse
    {
        public string Message { get; set; }
        public string Token { get; set; }
        public bool IsSuccess { get; set; }
        public string Errors { get; set; }

        public DateTime Expired { get; set; }
    }
}
