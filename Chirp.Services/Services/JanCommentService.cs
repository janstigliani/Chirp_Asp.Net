using Chirp.Model;
using Chirp.Services.Services.Interfaces;
using Chirp.Services.Services.Model.DTO;
using Chirp.Services.Services.Model.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chirp.Services.Services
{
    public class JanCommentService : ICommentService
    {
        private readonly ChirpContext _context;

        public JanCommentService(ChirpContext context)
        {
            _context = context;
        }
        public async Task<List<CommentViewModel>> GetComments()
        {
            return await _context.Comments.Select(c => new CommentViewModel
            {
                    CommentId = c.CommentId,
                    ChirpId = c.ChirpId,
                    CreationTime = c.CreationTime,
                    Text = c.Text,
                    ParentId = c.ParentId,
            }).ToListAsync();
        }

        public async Task<CommentViewModel> GetCommentById(int id)
        {
            var entity = await _context.Comments.FindAsync(id);
            if (entity == null)
            {
                return null;
            }
            return new CommentViewModel
            {
                CommentId = entity.CommentId,
                ChirpId = entity.ChirpId,
                CreationTime = entity.CreationTime,
                Text = entity.Text,
                ParentId = entity.ParentId
            };
        }
        public async Task<bool> UpdateComments(int id, Comment_DTO_Update comment)
        {
            var entity = await _context.Comments.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(comment.Text))
            {
                entity.Text = comment.Text;
            }

            _context.Entry(entity).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<int?> PostComments(Comment_DTO comment)
        {
            if (comment.Text == null)
            {
                return null;
            }

            var chirpParent = await _context.Chirps.FindAsync(comment.ChirpId);

            if (chirpParent == null)
            {
                return null;
            }

            var newComment = new Comment
            {
                ChirpId = comment.ChirpId,
                Text = comment.Text,
                ParentId = comment.ParentId
            };

            _context.Comments.Add(newComment);

            await _context.SaveChangesAsync();

            return newComment.CommentId;
        }

        public async Task<int?> DeleteComments(int id)
        {
            Comment comment = await _context.Comments.FindAsync(id);
            if (comment == null)
            {
                return null;
            }

            _context.Comments.Remove(comment);
            await _context.SaveChangesAsync();

            return comment.CommentId;
        }
    }
}
