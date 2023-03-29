using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System;

namespace pet_hotel
{
    public enum PetBreedType { }
    public enum PetColorType { }
    public class Pet
    {
        public int id { get; set; }

        [Required]
        public string name { get; set; }

        // [Required]
        // //breed, color, checked in
        // public string 
    }
}
