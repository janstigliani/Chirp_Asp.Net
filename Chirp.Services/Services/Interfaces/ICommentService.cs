using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chirp.Model;
using Chirp.Services.Services.Model.DTO;
using Chirp.Services.Services.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Chirp.Services.Services.Interfaces
{
    public interface ICommentService
    {
        public Task<List<CommentViewModel>> GetComments();
        public Task<CommentViewModel> GetCommentById(int id);
        public Task<bool> UpdateComments(int id, Comment_DTO_Update comment);
        public Task<int?> PostComments(Comment_DTO comment);
        public Task<int?> DeleteComments(int id);
    }
}
