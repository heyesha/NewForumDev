﻿namespace Questions.Domain;

public class Answer
{
    public Answer(Guid id, Guid userId, string text, Guid questionId)
    {
        Id = id;
        UserId = userId;
        Text = text;
        QuestionId = questionId;
        Rating = 0;
    }
    
    public Guid Id { get; set; }
    
    public Guid UserId { get; set; }
    
    public string Text { get; set; }
    
    public Guid QuestionId { get; }

    public Question Question { get; set; } = null!;
    
    public List<Guid> Comments { get; set; } = [];
    
    public long Rating { get; set; }
}