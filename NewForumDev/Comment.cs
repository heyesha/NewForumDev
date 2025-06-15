namespace NewForumDev.Domain.Comments;

public class Comment
{
    public Guid Id { get; set; }
    
    public required Guid UserId { get; set; }
    
    public Comment? Parent { get; set; }
    
    public required Guid EntityId { get; set; }
    
    public required string EntityType { get; set; }
    
    public List<Comment> Children { get; set; } = [];
}