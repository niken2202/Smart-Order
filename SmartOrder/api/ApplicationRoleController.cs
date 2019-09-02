using Model.Models;
using Service;
using SmartOrder.Infrastructure;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartOrder.api
{
    [RoutePrefix("api/role"),Authorize(Roles ="Admin")]
    public class ApplicationRoleController : ApiControllerBase
    {
        private IApplicationRoleService appRoleService;

        public ApplicationRoleController(IApplicationRoleService appRoleService, IErrorService errorService, IHistoryService _historyService) : base(errorService, _historyService)
        {
            this.appRoleService = appRoleService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                var model = appRoleService.GetAll();
                response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }

        [Route("add")]
        [HttpPost]
        public HttpResponseMessage GetAdd(HttpRequestMessage request,string roleName)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;
                ApplicationRole role = new ApplicationRole()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = roleName,
                };
                var model = appRoleService.Add(role);
                appRoleService.SaveChanges();
                response = request.CreateResponse(HttpStatusCode.OK, model);
                return response;
            });
        }
    }
}