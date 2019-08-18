using Model.Models;
using Service;
using SmartOrder.Infrastructure;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SmartOrder.api
{
    [RoutePrefix("api/material"), Authorize]
    public class MaterialController : ApiControllerBase
    {
        IMaterialService materialService;
        public MaterialController(IErrorService errorService, IMaterialService materialService, IHistoryService historyService) : base(errorService, historyService)
        {
            this.materialService = materialService;
        }
        [Route("add")]
        public HttpResponseMessage Post(HttpRequestMessage request, Material material)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var result = materialService.Add(material);
                    materialService.SaveChanges();
                    SaveHistory("Thêm nguyên liệu: " + result.Name);
                    SaveHistory("Đã thêm nguyên liệu mới có ID: " + result.ID);
                    response = request.CreateResponse(HttpStatusCode.Created, result);
                }
                return response;
            });
        }

        [Route("getall")]
        public HttpResponseMessage Get(HttpRequestMessage request)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listDish = materialService.GetAll();
                    response = request.CreateResponse(HttpStatusCode.OK, listDish);
                }
                return response;
            });
        }

        [Route("getbyid")]
        public HttpResponseMessage Get(HttpRequestMessage request,int materialId)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listDish = materialService.GetById(materialId);
                    response = request.CreateResponse(HttpStatusCode.OK, listDish);
                }
                return response;
            });
        }

        [Route("getbydishid")]
        public HttpResponseMessage GetAllByDishID(HttpRequestMessage request, int dishID)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    var listMaterial = materialService.GetAllByDishID(dishID);
                    response = request.CreateResponse(HttpStatusCode.OK, listMaterial);
                }
                return response;
            });
        }

        [Route("update")]
        public HttpResponseMessage Put(HttpRequestMessage request, Material material)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    materialService.Update(material);
                    SaveHistory("Cập nhật nguyên liệu " + material.Name);
                    SaveHistory("Đã cập nhật nguyên liệu mới có ID: " + material.ID);


                    response = request.CreateResponse(HttpStatusCode.OK);
                }
                return response;
            });
        }

        [Route("delete")]
        public HttpResponseMessage Delete(HttpRequestMessage request, int id)
        {
            return CreateHttpResponse(request, () =>
            {
                HttpResponseMessage response = null;

                if (!ModelState.IsValid)
                {
                    response = request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                else
                {
                    Material material = materialService.Delete(id);
                    materialService.SaveChanges();
                    SaveHistory("Xóa nguyên liệu: " + material.Name);
                    SaveHistory("Đã xóa nguyên liệu có ID: " + material.ID);
                    response = request.CreateResponse(HttpStatusCode.OK, material);
                }
                return response; 
                
            });
        }
    }
}
