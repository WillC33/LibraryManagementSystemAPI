using System.Data;
using LibraryManagementSystemAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystemAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class LoansController : ControllerBase
{
    private LibraryDbContext _context;

    public LoansController(LibraryDbContext context)
    {
        _context = context;
    }
    
    /// <summary>
    /// Endpoint to load a book to a member
    /// </summary>
    /// <param name="itemId"></param>
    /// <param name="memberId"></param>
    /// <returns></returns>
    /// <exception cref="DataException"></exception>
    [HttpPost("Loan")]
    public async Task<IActionResult> LoanItem(int itemId, int memberId)
    {
        try
        {
            Book item;
            Member member;

            try
            {
                item = await _context.Books
                    .SingleAsync(b => b.Id == itemId);
            }
            catch
            {
                throw new DataException($"The book with {itemId} could not be found");
            }

            try
            {
                member = await _context.Members
                    .SingleAsync(m => m.Id == memberId);
            }
            catch
            {
                throw new DataException($"The member with {memberId} could not be found");
            }
            
            var outstandingLoans = await _context.Loans.AnyAsync
            (l => l.Member.Id == memberId
                  && !l.IsReturned && l.DueDate < DateOnly.FromDateTime(DateTime.Now));
            if (outstandingLoans)
                return Forbid("Member has outstanding loans.");

            Loan loan = new()
            {
                Item = item,
                Member = member,
                DueDate = DateOnly.FromDateTime(DateTime.Now).AddDays(14),
                IsReturned = false
            };

            var res = await _context.Loans.AddAsync(loan);
            await _context.SaveChangesAsync();

            return Ok(loan);
        }
        catch(Exception e)
        {
            return BadRequest(e);
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="itemId"></param>
    /// <returns></returns>
    [HttpPost("Return")]
    public async Task<IActionResult> ReturnItem(int itemId)
    {
        try
        {
            Loan loan;
            try
            {
                loan = await _context.Loans.FirstAsync(l => l.Item.Id == itemId
                                                            && !l.IsReturned);
            }
            catch
            {
                throw new DataException($"An active loan for {itemId} could not be found");
            }

            loan.IsReturned = true;

            _context.Loans.Update(loan);
            await _context.SaveChangesAsync();
            return Ok($"{loan.Item.Title} returned");
        }
        catch (Exception e)
        {
            return BadRequest(e);
        }
    }

}