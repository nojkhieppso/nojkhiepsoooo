using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace HomeCinema.Web.Infrastructure.Core
{
    public class UploadMultipartFormProvider : MultipartFormDataStreamProvider
    {
        public UploadMultipartFormProvider(string rootPath) : base(rootPath) { }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            if (headers != null &&
                headers.ContentDisposition != null)
            {
                return String.Format(CultureInfo.InvariantCulture, "{0}_{1}", Guid.NewGuid(), headers
                    .ContentDisposition
                    .FileName.TrimEnd('"').TrimStart('"'));
            }

            return base.GetLocalFileName(headers);
        }
    }
}