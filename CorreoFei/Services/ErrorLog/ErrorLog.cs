﻿using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace CorreoFei.Services.ErrorLog
{
    public class ErrorLog : IErrorLog
    {
        private readonly IWebHostEnvironment  _webHostEnvironment;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ErrorLog(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor)
        {
            _webHostEnvironment = webHostEnvironment;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        public async Task ErrorLogAsync(string Mensaje)
        {
            try
            {
                String webRootPath = _webHostEnvironment.WebRootPath;

                String path = "";
                path = Path.Combine(webRootPath, "log");

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                //Retreive server/local IP address.
                var feature = _httpContextAccessor.HttpContext.Features.Get<IHttpConnectionFeature>();

                String LocalIPAddr = feature?.LocalIpAddress.ToString();

                using (StreamWriter outputFile = new StreamWriter(Path.Combine(path, "log.txt"), true))
                {
                    await outputFile.WriteLineAsync(Mensaje + " - " +
                        _httpContextAccessor.HttpContext.User.Identity.Name +
                        " - " + LocalIPAddr + " - " + DateTime.Now.ToString()
                        );
                }
            }
            catch (Exception ex)
            {
                //no hace nada
            }
        }
    }
}
