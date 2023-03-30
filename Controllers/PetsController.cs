using System.Net.NetworkInformation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetsController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public PetsController(ApplicationContext context)
        {
            _context = context;
        }

        // This is just a stub for GET / to prevent any weird frontend errors that 
        // occur when the route is missing in this controller
        [HttpGet]
        public IEnumerable<Pet> GetAllPets()
        {
            return _context.Pets.Include(pet => pet.petOwner);
        }

        [HttpGet("{id}")]
        public Pet GetPetById(int id)
        {
            return _context.Pets.Find(id);
        }

        [HttpPost]
        public IActionResult PostPet(Pet pet)
        {
            pet.petOwner = _context.PetOwners.Find(pet.petOwnerid);
            _context.Add(pet);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetPetById), new { id = pet.id }, pet);
        }

        // [HttpGet]
        // [Route("test")]
        // public IEnumerable<Pet> GetPets() {
        //     PetOwner blaine = new PetOwner{
        //         name = "Blaine"
        //     };

        //     Pet newPet1 = new Pet {
        //         name = "Big Dog",
        //         petOwner = blaine,
        //         color = PetColorType.Black,
        //         breed = PetBreedType.Poodle,
        //     };

        //     Pet newPet2 = new Pet {
        //         name = "Little Dog",
        //         petOwner = blaine,
        //         color = PetColorType.Golden,
        //         breed = PetBreedType.Labrador,
        //     };

        //     return new List<Pet>{ newPet1, newPet2};
        // }

        [HttpPut("{id}")]
        public IActionResult UpdatePet(Pet pet, int id)
        {
            pet.id = id;
            _context.Update(pet);
            _context.SaveChanges();
            return Ok(pet);
        }

        [HttpPut("{id}/checkin")]
        public IActionResult CheckIn(int id)
        {
            Pet pet = _context.Pets.Find(id);
            pet.checkedInAt = DateTime.Now.ToString();
            _context.Update(pet);
            _context.SaveChanges();
            return Ok(pet);
        }

        [HttpPut("{id}/checkout")]
        public IActionResult CheckOut(int id)
        {
            Pet pet = _context.Pets.Find(id);
            pet.checkedInAt = null;
            _context.Update(pet);
            _context.SaveChanges();
            return Ok(pet);
        }

        [HttpDelete("{id}")]
        public IActionResult DeletePet(int id)
        {
            Pet pet = _context.Pets.Find(id);
            _context.Pets.Remove(pet);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
