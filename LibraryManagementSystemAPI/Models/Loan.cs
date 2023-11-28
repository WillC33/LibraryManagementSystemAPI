using Microsoft.VisualBasic;

namespace LibraryManagementSystemAPI.Models;

public class Loan
{
        public int Id { get; set; }
        public Book Item { get; set; }
        public Member Member { get; set; }
        public DateOnly DueDate { get; set; }
        public bool IsReturned { get; set; }
}