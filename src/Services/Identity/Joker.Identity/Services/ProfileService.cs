using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Joker.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Joker.Identity.Services
{
    public class ProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> _claimsFactory;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JokerIdentityDbContext _dbContext;

        public ProfileService(UserManager<ApplicationUser> userManager, 
            IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
            JokerIdentityDbContext dbContext)
        {
            _userManager = userManager;
            _claimsFactory = claimsFactory;
            _dbContext = dbContext;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            if (user == null)
            {
                return;
            }
            
            var principal = await _claimsFactory.CreateAsync(user);
 
            var claims = principal.Claims.ToList();
            claims = claims.Where(claim => context.RequestedClaimTypes.Contains(claim.Type)).ToList();
            claims.Add(new Claim(JwtClaimTypes.GivenName, user.UserName));
 
            if (await _userManager.IsInRoleAsync(user, "PaidUser"))
            {
                var organization = await _dbContext.OrganizationUsers.FirstOrDefaultAsync(x => x.UserId == user.Id);
                if (organization != null)
                {
                    claims.Add(new Claim("organizationId", organization.OrganizationId.ToString()));
                    claims.Add(new Claim("organizationName", organization.OrganizationName));    
                }
                
            }
            context.IssuedClaims = claims;
        }
 
        public async Task IsActiveAsync(IsActiveContext context)
        {
            var sub = context.Subject.GetSubjectId();
            var user = await _userManager.FindByIdAsync(sub);
            context.IsActive = user != null;
        }
    }
}