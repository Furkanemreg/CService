using CService.Core.Entities;
using CService.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CService.Web.Controllers;

[Authorize(Roles = "Admin")]
public class LogsController : Controller
{
    private readonly IBaseService<ActivityLog> _logService;

    public LogsController(IBaseService<ActivityLog> logService)
    {
        _logService = logService;
    }

    public async Task<IActionResult> Index()
    {
        var logs = await _logService.GetAllAsync();
        return View(logs.OrderByDescending(x => x.CreatedDate).ThenBy(i => i.ActorEmail).ToList());
    }
}