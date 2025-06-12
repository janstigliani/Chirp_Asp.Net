using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chirp.Model;
using Chirp.Services.Services.Interfaces;
using Chirp.Services.Services.Model.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace Chirp.Services.Services
{
    public class JanChirpsService : IChirpsService
    {
        private readonly ChirpContext _context;

        public JanChirpsService(ChirpContext context)
        {
            _context = context;
        }

        public async Task<List<ChirpViewModel>> GetChirpsByFilter(filter filter)
        {
            IQueryable<Chirps> query = _context.Chirps.AsQueryable();
            if (!string.IsNullOrWhiteSpace(filter.Text))
            {
                query = query.Where(x => x.Text == filter.Text);
            }

            var result = await query.Select(z => new ChirpViewModel
            {
                ChirpsId = z.ChirpsId,
                CreationTime = z.CreationTime,
                Text = z.Text,
                ExternalUrl = z.ExternalUrl,
                Lat = z.Lat,
                Lng = z.Lng,
            }).ToListAsync();
            
            return result;
        }
    }
}
