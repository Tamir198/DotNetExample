using dotNet.Data;
using dotNet.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace dotNet.Controllers
{

    //This is how we annotate that this is not MVC controller 
    [ApiController]
    [Route("api/controller")]
    public class ContactController : Controller
    {   
        //THis will talk to in memory DB    
        private readonly ContactAPIDbContext dbContext;

        public ContactController(ContactAPIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }


        [HttpGet]
        public async Task<IActionResult> GetContacts()
        {
            return Ok(await dbContext.Contacts.ToListAsync());
        }


        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetSingleContact([FromRoute] Guid id) {
           Contact contact = await dbContext.Contacts.FindAsync(id);

            if (contact == null) {
                return NotFound();
            }

            return Ok(contact);
        }


        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteContact([FromRoute] Guid id)
        {
            Contact contact = await dbContext.Contacts.FindAsync(id);

            if (contact != null)
            {   
                dbContext.Contacts.Remove(contact);
                dbContext.SaveChanges();
                return Ok("Item Removed");

            }
            return NotFound();

        }

        [HttpPost]
        public async Task<IActionResult> AddContact(AddContactRequest addContactRequest) {

            Contact contact = new Contact()
            {
                Id = Guid.NewGuid(),
                Email = addContactRequest.Email,
                PhoneNumber = addContactRequest.PhoneNumber,
                Address = addContactRequest.Address
            };

            await dbContext.Contacts.AddAsync(contact);
            await dbContext.SaveChangesAsync();

            return Ok(contact);
        }


        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> UpdateContacts([FromRoute] Guid id, UpdateContactRequest updateContactRequest)
        {
            Contact contact = await dbContext.Contacts.FindAsync(id);

            if (contact != null) { 
                contact.PhoneNumber = updateContactRequest.PhoneNumber;
                contact.Address = updateContactRequest.Address;
                contact.Email = updateContactRequest.Email;

                await dbContext.SaveChangesAsync();

                return Ok(contact);
            } 


            return NotFound();
        }
    }
}
