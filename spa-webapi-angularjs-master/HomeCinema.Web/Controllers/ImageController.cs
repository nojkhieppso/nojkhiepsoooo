using AutoMapper;
using HomeCinema.Data.Infrastructure;
using HomeCinema.Data.Repositories;
using HomeCinema.Entities;
using HomeCinema.Web.Infrastructure.Core;
using HomeCinema.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using HomeCinema.Web.Infrastructure.Extensions;
using HomeCinema.Data.Extensions;

namespace HomeCinema.Web.Controllers
{
    [RoutePrefix("api/image")]
    public class ImageController : ApiControllerBase
    {
        private readonly IEntityBaseRepository<Image> _imagesRepository;

        public ImageController(IEntityBaseRepository<Image> imagesRepository,
            IEntityBaseRepository<Error> _errorsRepository, IUnitOfWork _unitOfWork)
            : base(_errorsRepository, _unitOfWork)
        {
            _imagesRepository = imagesRepository;
        }

        

        [MimeMultipart]
        [Route("images/upload")]
        public async Task<IHttpActionResult> Post()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent("form-data"))
            {
                return BadRequest("Unsupported media type");
            }

            try
            {
                var Images = await Add(Request);
                return Ok(new { Message = "Photos uploaded ok", Images = Images });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }

        }
        public async Task<IEnumerable<ImageViewModel>> Add(HttpRequestMessage request)
        {
            var uploadPath = HttpContext.Current.Server.MapPath("~/Content/images/movies");

            var provider = new UploadMultipartFormProvider(uploadPath);
            
            await request.Content.ReadAsMultipartAsync(provider);

            var images = new List<ImageViewModel>();

            foreach (var file in provider.FileData)
            {
                var fileInfo = new FileInfo(file.LocalFileName);

                images.Add(new ImageViewModel
                {
                    FileName = fileInfo.Name,
                    CreateDate = fileInfo.CreationTime,
                    LocalFilePath = fileInfo.DirectoryName,
                    FileLength = fileInfo.Length / 1024
                    
                });
            }

            return images;
        }


    }
}
