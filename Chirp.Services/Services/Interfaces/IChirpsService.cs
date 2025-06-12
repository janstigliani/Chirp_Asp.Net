using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Chirp.Model;
using Chirp.Services.Services.Model.ViewModels;

namespace Chirp.Services.Services.Interfaces
{
    public interface IChirpsService
    {
        public Task<List<ChirpViewModel>> GetChirpsByFilter(filter filter);

    }
}
