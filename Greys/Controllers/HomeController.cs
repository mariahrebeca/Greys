using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Greys.Models;
using Greys.Services;

namespace Greys.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IGreyService _greyService;

    public HomeController(ILogger<HomeController> logger, IGreyService greyService)
    {
        _logger = logger;
        _greyService = greyService;
    }

    public IActionResult Index(string tipo)
    {
        var grey = _greyService.GetGreyDto();
        ViewData["filter"] = string.IsNullOrEmpty(tipo) ? "all" : tipo;
        return View(grey);

    }

    public IActionResult Details(int Numero)
    {
        var grey = _greyService.GetDetailedGrey(Numero);
        return View(grey);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
