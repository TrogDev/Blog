namespace Blog.Application.Common.DTO;

public class PostDTO
{
    public required long Id { get; init; }
    public required string Title { get; init; }
    public required string Content { get; init; }
    public string? PreviewImage { get; init; }
    public required int LikesCount { get; init; }
    public required DateTime CreatedAt { get; set; }
    public required CategoryDTO Category { get; init; }
    public required IEnumerable<TagDTO> Tags { get; init; } = new List<TagDTO>();
}
