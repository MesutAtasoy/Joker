using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Joker.BackOffice.Controllers;

[Authorize]
public class BaseController : Controller
{
    
}