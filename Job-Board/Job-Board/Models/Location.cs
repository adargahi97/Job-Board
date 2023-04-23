using System;

namespace Job_Board.Models
{
    public class Location
    {
        /// <summary>
        /// Location's Id
        /// </summary>
        /// <example>0AB667E2-3936-48EC-B1DA-E7614239B788</example>
        public Guid Id { get; set; }
        /// <summary>
        /// Location's Street Address
        /// </summary>
        /// <example>987 Mason Dr</example>
        public string StreetAddress { get; set; }
        /// <summary>
        /// Location's City
        /// </summary>
        /// <example>Portland</example>
        public string City { get; set; }
        /// <summary>
        /// Location's State
        /// </summary>
        /// <example>OR</example>
        public string State { get; set; }
        /// <summary>
        /// Location's Zip Code
        /// </summary>
        /// <example>38332</example>
        public int Zip { get; set; }
        /// <summary>
        /// Building Location
        /// </summary>
        /// <example>Tango</example>
        public string Building { get; set; }

    }
    public class LocationRequest
    {
        /// <summary>
        /// Location's Street Address
        /// </summary>
        /// <example>987 Mason Dr</example>
        public string StreetAddress { get; set; }
        /// <summary>
        /// Location's Street Address
        /// </summary>
        /// <example>987 Mason Dr</example>
        public string City { get; set; }
        /// <summary>
        /// Location's State
        /// </summary>
        /// <example>OR</example>
        public string State { get; set; }
        /// <summary>
        /// Location's Zip Code
        /// </summary>
        /// <example>38223</example>
        public int Zip { get; set; }
        /// <summary>
        /// Building Location
        /// </summary>
        /// <example>Tango</example>
        public string Building { get; set; }

    }
}
