namespace LibraryManagementSystemAPI.Models;

public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public Author Author { get; set; }
    public Genre[] Genres { get; set; }
    public DateOnly PublicationDate { get; set; }
}