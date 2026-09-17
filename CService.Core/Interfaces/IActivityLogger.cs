using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CService.Core.Interfaces
{
    public interface IActivityLogger
    {
        Task LogAsync(string action, string? targetUserId = null, string? targetEmail = null, string? details = null);
    }
}
