namespace Blog.Web.Common.RequestData.Categories;

using Microsoft.AspNetCore.Mvc;

public class CategoryUpdateRequestData
{
    [FromRoute]
    public required long Id { get; set; }
    public required string Title { get; set; }
}