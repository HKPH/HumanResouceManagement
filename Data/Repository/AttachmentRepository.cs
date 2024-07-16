using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;

namespace HumanManagement.Data.Repository
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly DBContext _context;
        public AttachmentRepository(DBContext context)
        {
            _context = context;
        }

        public bool CreateAttachment(Attachment attachment)
        {
            _context.Add(attachment);
            return Save();
        }

        public bool DeleteAttachment(int attachmentId)
        {
            var attachment=GetAttachmentById(attachmentId);
            _context.Remove(attachment);
            return Save();
        }

        public Attachment GetAttachmentById(int attachmentId)
        {
            return _context.Attachments.Where(a => a.Id == attachmentId).FirstOrDefault();
        }

        public List<Attachment> GetAttachments()
        {
            return _context.Attachments.OrderBy(a=>a.Id).ToList();
        }


        public bool Save()
        {
            var check=_context.SaveChanges();
            return check>0?true:false;
        }

        public bool UpdateAttachment(Attachment attachment)
        {
            var attachmentUpdate = GetAttachmentById(attachment.Id);
            _context.Entry(attachmentUpdate).CurrentValues.SetValues(attachment);
            return Save();
        }
    }
}
