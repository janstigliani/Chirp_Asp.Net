using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chirp.Model;

namespace Chirp.Services.Services.Model.ViewModels
{
    internal class CommentViewModel
    {
        public int CommentId { get; set; }

        public int ChirpId { get; set; }

        public int? ParentId { get; set; }

        public string Text { get; set; } = null!;

        public DateTime CreationTime { get; set; }
    }
}
