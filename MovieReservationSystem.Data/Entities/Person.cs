namespace MovieReservationSystem.Data.Entities
{
    public class Person
    {
        public int PersonId { get; set; }
        public string FirstName { get; set; } = default!;
        public string LastName { get; set; } = default!;
        public string? ImageURL { get; set; }
        public DateOnly BirthDate { get; set; }
        public string Bio { get; set; } = default!;
        public Actor? Actor { get; set; }
        public Director? Director { get; set; }
    }
}
