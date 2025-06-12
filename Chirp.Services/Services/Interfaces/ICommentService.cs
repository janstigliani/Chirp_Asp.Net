using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chirp.Model;

namespace Chirp.Services.Services.Interfaces
{
    internal interface ICommentService
    {
        public Task<List<Comment>> GetComments();
    }
}
