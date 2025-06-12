using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chirp.Model;
using Chirp.Services.Services.Interfaces;
using Chirp.Services.Services.Model.DTO;
using Chirp.Services.Services.Model.Filter;
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
            if (!string.IsNullOrWhiteSpace(filter.ExtUrl))
            {
                query = query.Where(x => x.ExternalUrl.Contains(filter.ExtUrl));
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

        public async Task<List<ChirpViewModel>> GetAllChirps()
        {
            return await _context.Chirps.Select(z => new ChirpViewModel
            {
                ChirpsId = z.ChirpsId,
                CreationTime = z.CreationTime,
                Text = z.Text,
                ExternalUrl = z.ExternalUrl,
                Lat = z.Lat,
                Lng = z.Lng,
            }).ToListAsync();
        }

        public async Task<ChirpViewModel> GetChirpsById(int id)
        {
            var entity = await _context.Chirps.FindAsync(id);
            if(entity == null)
            {
                return null;
            }
            return new ChirpViewModel
            {
                ChirpsId = entity.ChirpsId,
                CreationTime = entity.CreationTime,
                Text = entity.Text,
                ExternalUrl = entity.ExternalUrl,
                Lat = entity.Lat,
                Lng = entity.Lng,
            };
        }

        public async Task<bool> UpdateChirps(int id, Chirps_DTO_Update chirps)
        {
            var entity = await _context.Chirps.FindAsync(id);
            if (entity == null)
            {
                return false;
            }

            if (!string.IsNullOrWhiteSpace(chirps.ExternalUrl))
            {
               entity.ExternalUrl = chirps.ExternalUrl;
            }
            if (!string.IsNullOrWhiteSpace(chirps.Text))
            {
                entity.Text = chirps.Text;
            }
            if (chirps.Lng != null)
            {
                entity.Lng = chirps.Lng;
            }
            if (chirps.Lat != null)
            {
                entity.Lng = chirps.Lat;
            }

            _context.Entry(chirps).State = EntityState.Modified;

            await _context.SaveChangesAsync();
    
            return true;
        }

        public async Task<int?> PostChirps(Chirps_DTO chirps)
        {
            if(string.IsNullOrWhiteSpace(chirps.Text))
            {
                return null;
            }

            var entity = new Chirps
            {
                CreationTime = DateTime.UtcNow,
                Text = chirps.Text,
                ExternalUrl = chirps.ExternalUrl,
                Lat = chirps.Lat,
                Lng = chirps.Lng
            };

            _context.Chirps.Add(entity);
            await _context.SaveChangesAsync();

            return entity.ChirpsId;
        }

        public async Task<int?> DeleteChirps(int id)
        {
            Chirps? chirps = await _context.Chirps.Include(c => c.Comments)
                                              .Where(c => c.ChirpsId == id)
                                              .SingleOrDefaultAsync();

            if (chirps == null)
            {
                return null;
            }
            if (chirps.Comments != null || chirps.Comments.Count > 0)
            {
                return -1;
            }

            _context.Chirps.Remove(chirps);
            await _context.SaveChangesAsync();

            return id;
        }
    }
}
