using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chirp.Services.Services.Model.DTO
{
    public class Chirps_DTO
    {
        public string Text { get; set; }
        public string? ExternalUrl { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }

        public Chirps_DTO(string text, string? externalUrl, double? lat, double? lng)
        {
            Text = text;
            ExternalUrl = externalUrl;
            Lat = lat;
            Lng = lng;
        }
    }
}
