using MovieReservationSystem.Data.Entities;

namespace MovieReservationSystem.Infrastructure.Seeding
{
    public static class SeedingData
    {
        public static List<Genre> LoadDefaultGenres()
        {
            return new List<Genre>()
            {
                new Genre { GenreId = 1, Name = "Action" },
                new Genre { GenreId = 2, Name = "Adventure" },
                new Genre { GenreId = 3, Name = "Comedy" },
                new Genre { GenreId = 4, Name = "Drama" },
                new Genre { GenreId = 5, Name = "Fantasy" },
                new Genre { GenreId = 6, Name = "Horror" },
                new Genre { GenreId = 7, Name = "Mystery" },
                new Genre { GenreId = 8, Name = "Romance" },
                new Genre { GenreId = 9, Name = "Sci-Fi" },
                new Genre { GenreId = 10, Name = "Thriller" },
                new Genre { GenreId = 11, Name = "Documentary" },
                new Genre { GenreId = 12, Name = "Animation" },
                new Genre { GenreId = 13, Name = "Musical" },
                new Genre { GenreId = 14, Name = "Western" }
            };
        }
        public static List<SeatType> LoadDefaultSeatTypes()
        {
            return new List<SeatType>()
            {
                new SeatType { SeatTypeId = 1, SeatTypePrice = 0m, TypeName = "Regular" },
                new SeatType { SeatTypeId = 2, SeatTypePrice = 0m, TypeName = "VIP" },
            };
        }
    }
}
