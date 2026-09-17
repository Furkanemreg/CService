using CService.Core.Entities;
using CService.Core.Interfaces;

namespace CService.Core.Services;

public class ActivityLogger : IActivityLogger
{
    private readonly IBaseService<ActivityLog> _logService;
    private readonly ICurrentUserService _currentUserService;

    public ActivityLogger(IBaseService<ActivityLog> logService, ICurrentUserService currentUserService)
    {
        _logService = logService;
        _currentUserService = currentUserService;
    }

    public Task LogAsync(string action, string? targetUserId = null, string? targetEmail = null, string? details = null)
    {
        var log = new ActivityLog
        {
            ActorEmail = _currentUserService.Email,
            Action = action,
            TargetUserId = targetUserId,
            TargetEmail = targetEmail,
            Details = details
        };

        return _logService.CreateAsync(log);
    }
}