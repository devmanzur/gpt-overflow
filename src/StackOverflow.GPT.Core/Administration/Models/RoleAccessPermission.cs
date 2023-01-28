﻿using StackOverflow.GPT.Core.Shared.Contracts;

namespace StackOverflow.GPT.Core.Administration.Models;

public class RoleAccessPermission : IAuditable
{
    public Guid RoleId { get; set; }
    public Guid AccessPermissionId { get; set; }
    public DateTime? CreatedAt { get; set; }
    public string? CreatedBy { get; set; }
    public DateTime? LastUpdatedAt { get; set; }
    public string? LastUpdatedBy { get; set; }
}