using Common.Exceptions;
using Service;
using SmartOrder.App_Start;
using SmartOrder.Infrastructure;
using SmartOrder.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace SmartOrder.api
{
    [RoutePrefix("api/user")]
    public class ApplicationUserController : ApiControllerBase
    {
        private ApplicationUserManager userManager;
        private IApplicationRoleService appRoleService;

        public ApplicationUserController(IApplicationRoleService appRoleService,
            ApplicationUserManager userManager,
            IErrorService errorService,
            IHistoryService _historyService)
            : base(errorService, _historyService)
        {
            this.userManager = userManager;
            this.appRoleService = appRoleService;
        }

        [Route("getall")]
        [HttpGet]
        // [Authorize(Roles = "Admin")]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                List<ApplicationUserViewModel> listUser = new List<ApplicationUserViewModel>();
                foreach (var user in userManager.Users)
                {
                    ApplicationUserViewModel u = new ApplicationUserViewModel()
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        UserName = user.UserName,
                        BirthDay = user.BirthDay,
                        PhoneNumber = user.PhoneNumber,
                        //  Roles = user.Roles
                    };
                    listUser.Add(u);
                }

                response = request.CreateResponse(HttpStatusCode.OK, listUser);

                return response;
            });
        }

        [HttpPost]
        [Route("add")]
        //[Authorize(Roles = "Admin")]
        public async Task<HttpResponseMessage> Create(HttpRequestMessage request, ApplicationUserViewModel applicationUserViewModel)
        {
            if (ModelState.IsValid)
            {
                var newAppUser = new Model.Models.ApplicationUser()
                {
                    FullName = applicationUserViewModel.FullName,
                    PhoneNumber = applicationUserViewModel.PhoneNumber,
                    BirthDay = applicationUserViewModel.BirthDay,
                    Address = applicationUserViewModel.Address,
                    UserName = applicationUserViewModel.UserName
                };

                try
                {
                    newAppUser.Id = Guid.NewGuid().ToString();
                    var result = await userManager.CreateAsync(newAppUser, applicationUserViewModel.PassWord);
                    if (result.Succeeded)
                    {
                        //add role to user
                        var listRole = appRoleService.GetAll().Select(x => x.Name);
                        foreach (var role in applicationUserViewModel.Roles)
                        {
                            if (listRole.Contains(role))
                            {
                                await userManager.RemoveFromRoleAsync(newAppUser.Id, role);
                                await userManager.AddToRoleAsync(newAppUser.Id, role);
                            }
                        }

                        return request.CreateResponse(HttpStatusCode.OK, applicationUserViewModel);
                    }
                    else
                        return request.CreateErrorResponse(HttpStatusCode.BadRequest, string.Join(",", result.Errors));
                }
                catch (NameDuplicatedException dex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, dex.Message);
                }
                catch (Exception ex)
                {
                    return request.CreateErrorResponse(HttpStatusCode.BadRequest, ex.Message);
                }
            }
            else
            {
                return request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
    }
}