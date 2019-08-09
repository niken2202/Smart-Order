﻿using Service;
using SmartOrder.Infrastructure;
using System.Net.Http;
using System.Web.Http;
using System.Linq;
using System.Web;
using System;
using System.IO;
using System.Net;
using System.Web.Hosting;

namespace SmartOrder.api
{
    [RoutePrefix("api/image")]
    public class ImageController : ApiControllerBase
    {
        public ImageController(IErrorService errorService, IHistoryService _historyService) : base(errorService, _historyService)
        {
        }

        [HttpPost]
        [Route("api/upload")]
        public HttpResponseMessage UploadImage(HttpRequestMessage request)
        {
           
            return CreateHttpResponse(request, () =>
            {
                string imageName = null;
                string filePath = null;
                var httpRequest = HttpContext.Current.Request;
                //Upload Image
                var postedFile = httpRequest.Files["Image"];
                //Create custom filename
                if (postedFile != null)
                {
                    imageName = new String(Path.GetFileNameWithoutExtension(postedFile.FileName).Take(10).ToArray()).Replace(" ", "-");
                    imageName = imageName + DateTime.Now.ToString("yymmssfff") + Path.GetExtension(postedFile.FileName);
                    filePath = HttpContext.Current.Server.MapPath("/Images/" + imageName);
                    postedFile.SaveAs(filePath);
                }
               
                var response = request.CreateResponse(HttpStatusCode.OK,"/Images/"+imageName);
                return response;
            });
        }
    }
}