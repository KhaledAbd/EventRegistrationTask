using System;

namespace RAG.EventRegistrationTask.Events.ValueObjects
{
    // Value Object: Location
    public class Location
    {
        public string Government { get; }
        public string City { get; }
        public string Street { get; }

        // Constructor to ensure that Location values are provided
        public Location(string government, string city, string street)
        {
            if (string.IsNullOrWhiteSpace(government))
            {
                throw new ArgumentException("Government cannot be null or empty.", nameof(government));
            }

            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentException("City cannot be null or empty.", nameof(city));
            }

            if (string.IsNullOrWhiteSpace(street))
            {
                throw new ArgumentException("Street cannot be null or empty.", nameof(street));
            }

            Government = government;
            City = city;
            Street = street;
        }

        // Overriding Equals and GetHashCode for value object equality comparison
        public override bool Equals(object obj)
        {
            if (obj is Location other)
            {
                return Government == other.Government &&
                       City == other.City &&
                       Street == other.Street;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Government, City, Street);
        }

        // Implicit conversion from Location to string (formatted)
        public static implicit operator string(Location location)
        {
            return $"{location.Government}, {location.City}, {location.Street}";
        }

        // Implicit conversion from string to Location (parsing the formatted string)
        public static implicit operator Location(string location)
        {
            var parts = location.Split(", ");
            if (parts.Length != 3)
                throw new ArgumentException("Invalid location format. Expected 'Government, City, Street'.");

            return new Location(parts[0], parts[1], parts[2]);
        }
    }
}
