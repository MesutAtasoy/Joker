using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Joker.BackOffice.Controllers;

[Authorize(Roles = "PaidUser")]
public class BaseController : Controller
{
    
}