using System;
using System.Collections.Generic;
using System.Text;
using UnifiedAIChat.Application.Common.Models.Messege;
using UnifiedAIChat.Domain.Entities;

namespace UnifiedAIChat.Application.Services.Messege
{
    public interface IMessegeService
    {
        Task<string> SendMessageAsync(SendMessegeCommand messegeCommand, CancellationToken ct);
    }
}
