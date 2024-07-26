using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using HumanManagement.Models;
using HumanManagement.Services;
using Microsoft.AspNetCore.Http;
using HumanManagement.Data.Repository.Interface;

namespace HumanManagement.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttachmentsController : ControllerBase
    {
        private readonly IAttachmentRepository _attachmentRepository;

        public AttachmentsController(IAttachmentRepository attachmentRepository)
        {
            _attachmentRepository = attachmentRepository;
        }

        [HttpGet]
        public IActionResult GetAttachments()
        {
            var attachments = _attachmentRepository.GetAttachments();
            return Ok(attachments);
        }

        [HttpGet("{attachmentId}")]
        public IActionResult GetAttachment(int attachmentId)
        {
            var attachment = _attachmentRepository.GetAttachmentById(attachmentId);
            if (attachment == null)
            {
                return NotFound();
            }
            return Ok(attachment);
        }

        [HttpPost("upload")]
        public IActionResult Upload([FromForm] IFormFile file, [FromForm] int employeeId)
        {
            if (file == null || file.Length == 0)
                return BadRequest("No file uploaded.");

            if (employeeId <= 0)
                return BadRequest("Invalid employee ID.");

            // Xác định thư mục lưu trữ tệp
            var baseDirectory = AppContext.BaseDirectory;
            var uploadsFolder = Path.Combine(baseDirectory, "Uploads");

            // Tạo thư mục nếu nó không tồn tại
            if (!Directory.Exists(uploadsFolder))
            {
                try
                {
                    Directory.CreateDirectory(uploadsFolder);
                }
                catch (Exception ex)
                {
                    return StatusCode(500, $"Failed to create directory: {ex.Message}");
                }
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            try
            {
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    file.CopyTo(fileStream);
                }

                var attachment = new Models.Attachment
                {
                    FileName = file.FileName,
                    FilePath = uniqueFileName,
                    UploadDate = DateTime.Now,
                    EmployeeId = employeeId
                };

                if (!_attachmentRepository.CreateAttachment(attachment))
                {
                    return StatusCode(500, "Can't create");
                }

                return Ok("Create successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{attachmentId}")]
        public IActionResult UpdateAttachment(int attachmentId, [FromBody] Models.Attachment attachment)
        {
            if (attachmentId != attachment.Id || attachment == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                if (!_attachmentRepository.UpdateAttachment(attachment))
                {
                    return StatusCode(500, "Can't update");
                }

                return Ok("Update successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{attachmentId}")]
        public IActionResult DeleteAttachment(int attachmentId)
        {
            try
            {
                if (!_attachmentRepository.DeleteAttachment(attachmentId))
                {
                    return StatusCode(500, "Can't delete");
                }

                return Ok("Delete successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("download/{id}")]
        public IActionResult DownloadAttachment(int id)
        {
            var attachment = _attachmentRepository.GetAttachmentById(id);
            if (attachment == null)
            {
                return NotFound();
            }

            var baseDirectory = AppContext.BaseDirectory;
            var filePath = Path.Combine(baseDirectory, "Uploads", attachment.FilePath);
            if (!System.IO.File.Exists(filePath))
            {
                return NotFound();
            }

            try
            {
                var memory = new MemoryStream();
                using (var stream = new FileStream(filePath, FileMode.Open))
                {
                    stream.CopyTo(memory);
                }
                memory.Position = 0;
                return File(memory, GetContentType(filePath), attachment.FileName);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private string GetContentType(string path)
        {
            var types = new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"}
            };

            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types.ContainsKey(ext) ? types[ext] : "application/octet-stream";
        }
    }
}
