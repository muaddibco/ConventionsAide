namespace ConventionsAide.Users.Contracts
{
    public class CreateUserRequestDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ExternalId { get; set; }
    }
}