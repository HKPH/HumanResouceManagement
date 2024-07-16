using HumanManagement.Models;

namespace HumanManagement.Data.Repository.Interface
{
    public interface IAttachmentRepository
    {
        List<Attachment> GetAttachments();
        Attachment GetAttachmentById(int attachmentId);
        bool Save();
        bool CreateAttachment(Attachment attachment);
        bool UpdateAttachment(Attachment attachment);
        bool DeleteAttachment(int attachmentId);
    }
}
