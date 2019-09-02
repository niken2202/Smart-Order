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
    [RoutePrefix("api/user"), Authorize(Roles = "Admin")]
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
            return CreateHttpResponse(request, ()  =>
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
                        Address = user.Address,
                       Roles = appRoleService.GetRoleByUserID(user.Id)
                    };
                    listUser.Add(u);
                }

                response = request.CreateResponse(HttpStatusCode.OK, listUser);

                return response;
            });
        }

        [Route("getbyid")]
        [HttpGet]
        // [Authorize(Roles = "Admin")]
        public HttpResponseMessage GetByID(HttpRequestMessage request,string id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var user = userManager.Users.SingleOrDefault(x => x.Id.Equals(id));
                ApplicationUserViewModel u = null;
                if (user != null)
                {
                     u = new ApplicationUserViewModel()
                    {
                        Id = user.Id,
                        FullName = user.FullName,
                        UserName = user.UserName,
                        BirthDay = user.BirthDay,
                        PhoneNumber = user.PhoneNumber,
                        Address = user.Address,
                        Roles = appRoleService.GetRoleByUserID(user.Id)
                    };
                }
                response = request.CreateResponse(HttpStatusCode.OK, u);

                return response;
            });
        }
        [Route("changepassword"), HttpPut]
        //[Authorize(Roles = "Admin")]
        public async Task<HttpResponseMessage> ChangePassword(HttpRequestMessage request, ApplicationUserViewModel user)
        {
            if (ModelState.IsValid)
            {
               
                try
                {
                 //   var result1 = await userManager.PasswordValidator.ValidateAsync(user.NewPassword);
                    var result = await userManager.ChangePasswordAsync(user.Id, user.CurrentPassword, user.NewPassword);

                    if (result.Succeeded )
                    {
                        return request.CreateResponse(HttpStatusCode.OK);
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
        [Route("update")]
        //[Authorize(Roles = "Admin")]
        public async Task<HttpResponseMessage> Put(HttpRequestMessage request, ApplicationUserViewModel user)
        {
            if (ModelState.IsValid)
            {
                var newAppUser = new Model.Models.ApplicationUser()
                {
                    FullName = user.FullName,
                    PhoneNumber = user.PhoneNumber,
                    BirthDay = user.BirthDay,
                    Address = user.Address,
                    UserName = user.UserName
                };

                try
                {
                    var oldUser = userManager.Users.FirstOrDefault(x => x.Id == user.Id);
                    oldUser.FullName = user.FullName;
                    oldUser.PhoneNumber = user.PhoneNumber;
                    oldUser.BirthDay = user.BirthDay;
                    oldUser.Address = user.Address;
                    var result = await userManager.UpdateAsync(oldUser);

                    if (result.Succeeded)
                    {
                        

                        return request.CreateResponse(HttpStatusCode.OK, oldUser);
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
                        applicationUserViewModel.Roles = await userManager.GetRolesAsync(newAppUser.Id);
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