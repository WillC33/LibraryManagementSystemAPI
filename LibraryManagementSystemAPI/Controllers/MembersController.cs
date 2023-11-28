using LibraryManagementSystemAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MembersController : ControllerBase
{
    private readonly LibraryDbContext _context;

    public MembersController(LibraryDbContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Fetches a list of all Members
    /// </summary>
    /// <returns></returns>
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
    {
        return await _context.Members.ToListAsync();
    }

    /// <summary>
    /// Fetches an individual Member
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpGet("{id}")]
    public async Task<ActionResult<Member>> GetMember(int id)
    {
        var Member = await _context.Members.FindAsync(id);

        if (Member == null)
        {
            return NotFound();
        }

        return Ok(Member);
    }

    /// <summary>
    /// Posts a new Member record
    /// </summary>
    /// <param name="Member"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<Member>> PostMember(Member Member)
    {
        _context.Members.Add(Member);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetMember", new { id = Member.Id }, Member);
    }
    
    /// <summary>
    /// Updates a Member record
    /// </summary>
    /// <param name="id"></param>
    /// <param name="Member"></param>
    /// <returns></returns>
    [HttpPut("{id}")]
    public async Task<IActionResult> PutMember(int id, Member Member)
    {
        if (id != Member.Id)
        {
            return BadRequest();
        }

        _context.Entry(Member).State = EntityState.Modified;
        await _context.SaveChangesAsync();

        return NoContent();
    }

    /// <summary>
    /// Deletes a Member record
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    [HttpDelete("{id}")]
    public async Task<ActionResult<Member>> DeleteMember(int id)
    {
        var Member = await _context.Members.FindAsync(id);
        if (Member == null)
        {
            return NotFound();
        }

        _context.Members.Remove(Member);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}