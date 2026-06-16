// Copyright (c) Microsoft. All rights reserved.
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.AI;

namespace Microsoft.Agents.AI.Workflows.Supervisor;

/// <summary>
/// Request for human review of a proposed plan.
/// </summary>
/// <param name="Plan">The proposed plan.</param>
/// <param name="CurrentProgress">The current progress ledger, if available. During the initial plan review,
/// this will be <see langword="null"/>. In subsequent reviews after replanning (due to stalls), this will
/// contain the latest progress ledger that determined that no progress has been made or the workflow was in
/// a loop.</param>
/// <param name="IsStalled">Whether the workflow is currently stalled.</param>
public record SupervisorPlanReviewRequest(ChatMessage Plan, SupervisorProgressLedger? CurrentProgress, bool IsStalled)
{
    /// <summary>
    /// Create an approving <see cref="SupervisorPlanReviewResponse"/>.
    /// </summary>
    /// <returns></returns>
    public SupervisorPlanReviewResponse Approve() => new([]);

    /// <summary>
    /// Create a <see cref="SupervisorPlanReviewResponse"/> with revisions.
    /// </summary>
    /// <returns></returns>
    public SupervisorPlanReviewResponse Revise(string message) => new([new(ChatRole.User, message)]);

    /// <summary>
    /// Create a <see cref="SupervisorPlanReviewResponse"/> with revisions.
    /// </summary>
    /// <returns></returns>
    public SupervisorPlanReviewResponse Revise(ChatMessage message) => new([message]);

    /// <summary>
    /// Create a <see cref="SupervisorPlanReviewResponse"/> with revisions.
    /// </summary>
    /// <returns></returns>
    public SupervisorPlanReviewResponse Revise(IEnumerable<ChatMessage> messages)
        => new(messages is List<ChatMessage> messageList ? messageList : messages.ToList());
}
