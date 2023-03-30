using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using pet_hotel.Models;
using Microsoft.EntityFrameworkCore;

namespace pet_hotel.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PetOwnersController : ControllerBase
    {
        private readonly ApplicationContext _context;
        public PetOwnersController(ApplicationContext context)
        {
            _context = context;
        }

        // This is just a stub for GET / to prevent any weird frontend errors that 
        // occur when the route is missing in this controller
        [HttpGet]
        public IEnumerable<PetOwner> GetOwners()
        {
            return _context.PetOwners;
        }

        [HttpGet("{id}")]
        public PetOwner GetOwner(int id)
        {
            return _context.PetOwners.Find(id);
        }

        [HttpPost]
        public IActionResult Post(PetOwner petOwner)
        {
            _context.Add(petOwner);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetOwner), new { id = petOwner.id }, petOwner);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteOwner(int id) {
            PetOwner petOwner = _context.PetOwners.Find(id);

            _context.PetOwners.Remove(petOwner);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}")]
        public IActionResult UpdateOwner(int id, PetOwner owner){
            owner.id = id;

            _context.Update(owner);
            _context.SaveChanges();
            return Ok();
        }
    }
}
