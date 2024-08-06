using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IAttachmentRepository
    {
        Task<List<Attachment>> GetAttachments();
        Task<Attachment> GetAttachmentById(int attachmentId);
        Task<bool> Save();
        Task<Attachment> CreateAttachment(Attachment attachment);
        Task<Attachment> UpdateAttachment(Attachment attachment);
        Task<Attachment> DeleteAttachment(int attachmentId);
    }
}
