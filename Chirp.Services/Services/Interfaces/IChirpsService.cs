using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chirp.Model;
using Chirp.Services.Services.Model.DTO;
using Chirp.Services.Services.Model.Filter;
using Chirp.Services.Services.Model.ViewModels;

namespace Chirp.Services.Services.Interfaces
{
    public interface IChirpsService
    {
        public Task<List<ChirpViewModel>> GetChirpsByFilter(filter filter);
        public Task<List<ChirpViewModel>> GetAllChirps();
        public Task<ChirpViewModel> GetChirpsById(int id);
        public Task<bool> UpdateChirps(int id, Chirps_DTO_Update chirps);
        public Task<int?> PostChirps(Chirps_DTO chirps);
        public Task<int?> DeleteChirps(int id);

    }
}
