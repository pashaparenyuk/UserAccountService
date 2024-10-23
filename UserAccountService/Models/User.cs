namespace UserAccountService.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string? LastName { get; set; }
        public string? FirstName { get; set; }
        public string? MiddleName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? PassportNumber { get; set; }
        public string? PlaceOfBirth { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? RegistrationAddress { get; set; }
        public string? ResidentialAddress { get; set; }
    }
}
