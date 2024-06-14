using DataLayer.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using Service.User;
using System.Drawing;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Service.Upload;
using Models;
using System.Net;
using Newtonsoft.Json;

namespace marketing_api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class UploadController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IUploadInterface _uploadService;

        public UploadController(ILogger<UserController> logger, IUserInterface userService, IUploadInterface uploadService)
        {
            _logger = logger;
            _uploadService = uploadService;
        }

        [HttpPost]
        public async Task<IActionResult> Upload()
        {
            try
            {
                var formCollection = await Request.ReadFormAsync();
                var file = formCollection.Files.First();
                var req = JsonConvert.DeserializeObject<DocumentRequest>(Request.Form["data"]);

                var folderName = Path.Combine("Resources", req.context, req.request, (req.contextId == 0) ? "Images" :Convert.ToString(req.contextId) );

                var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (!Directory.Exists(pathToSave))
                {
                    DirectoryInfo di = Directory.CreateDirectory(pathToSave);
                }
                if (file.Length > 0)
                {
                    var fileName = (req.contextId == 0) ? DateTime.Now.ToString("yyyyMMddHHmmssffff")+Path.GetExtension(file.FileName) : Convert.ToString(req.contextId) + "_" + ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"')+Path.GetExtension(file.FileName) ;
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    if (req.context == "candidate")
                    {
                        if (req.request == "document" && req.contextId != 0)
                        {
                            CandidateDocumentDTO doc = new CandidateDocumentDTO();
                            doc.Name = fileName;
                            doc.Type = Path.GetExtension(fileName);
                            doc.Size = 0;
                            doc.CandidateId = req.contextId;
                            doc.Path = dbPath;
                            doc.UploadDate = DateTime.Now;
                            return Ok(_uploadService.SaveDocument(doc));
                        }
                    }
                    if (req.context == "client")
                    {
                        if (req.request == "document" && req.contextId != 0)
                        {
                            ClientDocumentDTO doc = new ClientDocumentDTO();
                            doc.Name = fileName;
                            doc.Type = Path.GetExtension(fileName);
                            doc.Size = 0;
                            doc.ClientId = req.contextId;
                            doc.Path = dbPath;
                            doc.UploadDate= DateTime.Now;
                            return Ok(_uploadService.SaveDocument(doc));
                        }
                    }


                    //Byte[] b = System.IO.File.ReadAllBytes(fullPath);
                    //return File(b,file.ContentType);
                    return Ok(new { dbPath });
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex}");
            }
        }

        //[HttpPost("Download")]
        //public async Task<IActionResult> Downloading(string _context)
        //{
        //    try
        //    {
        //        var formCollection = await Request.ReadFormAsync();
        //        _context = "Crop";
        //        var folderName = Path.Combine("Resources", _context);
        //        var pathToFile = Path.Combine(Directory.GetCurrentDirectory(), folderName);

        //        string firstText = "Hello";
        //        string secondText = "World";

        //        PointF firstLocation = new PointF(10f, 10f);
        //        PointF secondLocation = new PointF(10f, 50f);

        //        string imageFilePath = pathToFile + "bajra-2f-millet-500x500.jpg";
        //        Bitmap bitmap = (Bitmap)System.Drawing.Image.FromFile(imageFilePath);


        //        using (Graphics graphics = Graphics.FromImage(bitmap))
        //        {
        //            using (Font arialFont = new Font("Arial", 10))
        //            {
        //                graphics.DrawString(firstText, arialFont, Brushes.Blue, firstLocation);
        //                graphics.DrawString(secondText, arialFont, Brushes.Red, secondLocation);
        //            }
        //        }

        //        bitmap.Save(imageFilePath);

        //        return Ok();
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Internal server error: {ex}");
        //    }
        //}
    }

    public class DocumentRequest
    {
        public string context { get; set; }

        public string request { get; set; }

        public int contextId { get; set; }
    }
}
