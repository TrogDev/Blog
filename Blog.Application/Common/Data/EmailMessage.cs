namespace Blog.Application.Common.Data;

public class EmailMessage
{
    public required string To { get; set; }
    public required string Title { get; set; }
    public required string Content { get; set; }
}