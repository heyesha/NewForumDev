﻿namespace NewForumDev.Contracts;

public record CreateQuestionDto(string Title, string Text, Guid UserId, Guid[] TagIds);