using IdentityModel;
using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using YoutubeCrawlDotnet.Server.Models;

namespace YoutubeCrawlDotnet.Server.Helpers
{
    public class IdentityProfileService : IProfileService
    {
        private readonly IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory;
        private readonly UserManager<ApplicationUser> userManager;

        public IdentityProfileService(IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
            UserManager<ApplicationUser> userManager)
        {
            this.claimsFactory = claimsFactory;
            this.userManager = userManager;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var userId = context.Subject.GetSubjectId();
            var user = await userManager.FindByIdAsync(userId);
            var claimsPrincipal = await claimsFactory.CreateAsync(user);
            var claims = claimsPrincipal.Claims.ToList();

            var claimsDB = await userManager.GetClaimsAsync(user);

            var mappedClaims = new List<Claim>();

            foreach (var claim in claimsDB)
            {
                if (claim.Type == ClaimTypes.Role)
                {
                    mappedClaims.Add(new Claim(JwtClaimTypes.Role, claim.Value));
                }
                else
                {
                    mappedClaims.Add(claim);
                }
            }

            claims.AddRange(mappedClaims);

            context.IssuedClaims = claims;
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var userId = context.Subject.GetSubjectId();
            var user = await userManager.FindByIdAsync(userId);
            context.IsActive = user != null;
        }
    }
}
