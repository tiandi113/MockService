using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Text;

namespace MockService
{
    public class MockMiddleware
    {
        private readonly RequestDelegate request;
        public MockMiddleware(RequestDelegate request)
        {
            this.request = request;
        }
        public async Task Invoke(HttpContext httpContext)
        {
            var req = httpContext.Request;
            var head = req.Headers["Mock"];

            if (head == "true")
            {
                var path = req.Path;
                var mocklist = AppConfig.GetAppSettings<List<MockModel>>("mock");
                var mock = mocklist.FirstOrDefault(m => m.api == path);
                if (mock != null && mock.method.ToUpper() == req.Method)
                {
                    if (!mock.mockjs.EndsWith(".json"))
                    {
                        mock.mockjs += ".json";
                    }
                    httpContext.Response.ContentType = "text/json;charset=utf-8;";

                    var filePath = AppContext.BaseDirectory + $"/Mock/{mock.mockjs}";
                    var json = getJson(filePath);
                    await httpContext.Response.WriteAsync(json);
                }
            }
            await request.Invoke(httpContext);
        }
        private String getJson(String fileName)
        {

            using (StreamReader streamReader = new StreamReader(fileName))
            {
                var str = streamReader.ReadToEnd();
                streamReader.Close();
                return str;
            }

        }
    }
}
