using Microsoft.AspNetCore.Mvc;
using HumanManagement.Models;
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
        public async Task<IActionResult> GetAttachments()
        {
            var attachments = await _attachmentRepository.GetAttachments();
            return Ok(attachments);
        }

        [HttpGet("{attachmentId}")]
        public async Task<IActionResult> GetAttachment(int attachmentId)
        {
            var attachment = await _attachmentRepository.GetAttachmentById(attachmentId);
            if (attachment == null)
            {
                return NotFound();
            }
            return Ok(attachment);
        }

        [HttpPost("upload")]
        public async Task<IActionResult> Upload([FromForm] IFormFile file, [FromForm] int employeeId)
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
                    await file.CopyToAsync(fileStream);
                }

                var attachment = new Attachment
                {
                    FileName = file.FileName,
                    FilePath = uniqueFileName,
                    UploadDate = DateTime.Now,
                    EmployeeId = employeeId
                };

                var createdAttachment = await _attachmentRepository.CreateAttachment(attachment);
                if (createdAttachment == null)
                {
                    return StatusCode(500, "Can't create attachment.");
                }

                return Ok("Create successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{attachmentId}")]
        public async Task<IActionResult> UpdateAttachment(int attachmentId, [FromBody] Attachment attachment)
        {
            if (attachmentId != attachment.Id || attachment == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var updatedAttachment = await _attachmentRepository.UpdateAttachment(attachment);
                if (updatedAttachment == null)
                {
                    return StatusCode(500, "Can't update attachment.");
                }

                return Ok("Update successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{attachmentId}")]
        public async Task<IActionResult> DeleteAttachment(int attachmentId)
        {
            try
            {
                var deletedAttachment = await _attachmentRepository.DeleteAttachment(attachmentId);
                if (deletedAttachment == null)
                {
                    return StatusCode(500, "Can't delete attachment.");
                }

                return Ok("Delete successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("download/{id}")]
        public async Task<IActionResult> DownloadAttachment(int id)
        {
            var attachment = await _attachmentRepository.GetAttachmentById(id);
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
                    await stream.CopyToAsync(memory);
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
