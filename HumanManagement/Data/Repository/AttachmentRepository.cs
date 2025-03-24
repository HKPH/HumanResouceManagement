using HumanManagement.Data.Repository.Interface;
using HumanManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace HumanManagement.Data.Repository
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly DBContext _context;

        public AttachmentRepository(DBContext context)
        {
            _context = context;
        }

        public async Task<Attachment> CreateAttachment(Attachment attachment)
        {
            await _context.Attachments.AddAsync(attachment);
            await Save();
            return attachment;
        }

        public async Task<Attachment> DeleteAttachment(int attachmentId)
        {
            var attachment = await GetAttachmentById(attachmentId);
            if (attachment == null) return null;

            _context.Attachments.Remove(attachment);
            await Save();
            return attachment;
        }

        public async Task<Attachment> GetAttachmentById(int attachmentId)
        {
            return await _context.Attachments
                .FirstOrDefaultAsync(a => a.Id == attachmentId);
        }

        public async Task<List<Attachment>> GetAttachments()
        {
            return await _context.Attachments
                .OrderBy(a => a.Id)
                .ToListAsync();
        }

        public async Task<bool> Save()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Attachment> UpdateAttachment(Attachment attachment)
        {
            var attachmentUpdate = await GetAttachmentById(attachment.Id);
            if (attachmentUpdate == null) return null;

            _context.Entry(attachmentUpdate).CurrentValues.SetValues(attachment);
            await Save();
            return attachment;
        }
    }
}
