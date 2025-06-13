namespace DomainLayer.Models.IdentityModule
{
    public class Address
    {
        public int id {  get; set; } //pk
        public string Country { get; set; } = default!;
        public string City { get; set; } = default!;
        public string Street { get; set; } = default!;
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public ApplicationUser User { get; set; } = default!;
        public string UserId { get; set; } = default!; //fk

    }
}