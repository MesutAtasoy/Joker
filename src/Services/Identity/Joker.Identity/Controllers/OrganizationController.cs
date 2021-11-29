using IdentityServer4;
using IdentityServer4.Extensions;
using Joker.Identity.Models;
using Joker.Identity.Models.Entities;
using Joker.Identity.ViewModels.Organization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Joker.Identity.Controllers;

[Route("api/Organizations")]
[Authorize(IdentityServerConstants.LocalApi.PolicyName)]
public class OrganizationController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly JokerIdentityDbContext _dbContext;
    private readonly IHttpContextAccessor _contextAccessor;

    public OrganizationController(UserManager<ApplicationUser> userManager,
        JokerIdentityDbContext dbContext, 
        IHttpContextAccessor contextAccessor)
    {
        _userManager = userManager;
        _dbContext = dbContext;
        _contextAccessor = contextAccessor;
    }
    
    [HttpPost]
    public async Task< IActionResult> CreateOrganization([FromBody] CreateOrganizationCreateRequestModel requestModel)
    {
        var userId = _contextAccessor?.HttpContext?.User.GetSubjectId();
        
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null)
        {
            return BadRequest();
        }

        var isInRole = await _userManager.IsInRoleAsync(user, "PaidUser");
        if (!isInRole)
        {
            await _userManager.AddToRoleAsync(user, "PaidUser");

            var organization = new Organization
            {
                Name = requestModel.Name,
                OrganizationUsers = new List<OrganizationUser> { new() { UserId = user.Id } }
            };
                
            var newOrganization  = await _dbContext.Organizations.AddAsync(organization);

            await _dbContext.SaveChangesAsync();

            return Ok(new CreateOrganizationCreateResponseModel
            {
                OrganizationId = newOrganization.Entity.Id
            });
        }

        return BadRequest();
    }   
}