using Microsoft.AspNetCore.Mvc;
using OneIdentity.Homework.Repository.Abstraction;
using OneIdentity.Homework.Repository.Models.User;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OneIdentity.Homework.Api.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UsersController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    // GET: api/<Users>
    [HttpGet]
    public async Task<ActionResult<User>> GetPaged([FromQuery(Name = "ps")] int pageSize, [FromQuery(Name = "pn")] int pageNumber)
    {
        return Ok(await _userRepository.GetPageOfUsersAsync(pageSize, pageNumber, HttpContext.RequestAborted));
    }

    // GET api/<Users>/5
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetById(Guid id)
    {
        var user = await _userRepository.GetUserByIdAsync(id, HttpContext.RequestAborted);
        if (user is null)
        {
            return NotFound();
        }
        return Ok(user);
    }

    // POST api/<Users>
    [HttpPost]
    public async Task<ActionResult<User>> Post([FromBody] CreateUser createUser)
    {
        var user = await _userRepository.CreateUser(createUser, HttpContext.RequestAborted);
        return CreatedAtAction(nameof(GetById),new { id = user.Id }, user);
    }

    // PUT api/<Users>/5
    [HttpPut("{id}")]
    public async Task<ActionResult<User>> Put(Guid id, [FromBody] UpdateUser updateUser)
    {
        var updatedUser = await _userRepository.UpdateUserAsync(id, updateUser, HttpContext.RequestAborted);
        if (updatedUser is null)
        {
            return NotFound();
        }

        return Ok(updatedUser);
    }

    // DELETE api/<Users>/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<User>> Delete(Guid id)
    {
        var result = await _userRepository.DeleteUserAsync(id);
        if (!result)
        {
            return BadRequest();
        }
        return NoContent();
    }
}
