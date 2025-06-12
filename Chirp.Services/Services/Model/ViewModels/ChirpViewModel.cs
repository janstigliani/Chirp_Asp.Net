using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chirp.Model;

namespace Chirp.Services.Services.Model.ViewModels
{
    public class ChirpViewModel
    {
        public int ChirpsId { get; set; }

        public DateTime CreationTime { get; set; }

        public string Text { get; set; }

        public string? ExternalUrl { get; set; }

        public double? Lat { get; set; }

        public double? Lng { get; set; }
    }
}
