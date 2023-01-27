using ImageGallery.API.Services;
using Microsoft.AspNetCore.Authorization;

namespace ImageGallery.API.Authorization
{
    public class MustOwnImageHandler : AuthorizationHandler<MustOwnImageRequirement>
    {
        private readonly IHttpContextAccessor httpContextAccessor;
        private readonly IGalleryRepository galleryRepository;

        public MustOwnImageHandler(IHttpContextAccessor httpContextAccessor, IGalleryRepository galleryRepository)
        {
            this.httpContextAccessor = httpContextAccessor;
            this.galleryRepository = galleryRepository;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MustOwnImageRequirement requirement)
        {
            var imageId = this.httpContextAccessor
                              .HttpContext?
                              .GetRouteValue("id")?
                              .ToString();
            if (imageId == null)
            {
                context.Fail();
                return;
            }

            if(!Guid.TryParse(imageId,out Guid imageGuid))
            {
                context.Fail();
                return;
            }

            var subject = context.User.Claims.FirstOrDefault(p=>p.Type=="sub")?.Value;
            if (subject == null)
            {
                context.Fail();
                return;
            }

            if (!await galleryRepository.IsImageOwnerAsync(imageGuid, subject))
            {
                context.Fail();
                return;
            }
            context.Succeed(requirement);
        }
    }
}
