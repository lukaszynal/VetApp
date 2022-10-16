using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetApp.Classes;
using VetAppDal.Classes;

namespace VetAppDal.Repo.Interfaces
{
    public interface IAnimalsRepository
    {
        public Task<List<Animal>> GetAnimalsAsync(AnimalQueryParameters queryParemeters);
        public Task<Animal> GetAnimalAsync(int id);
        public Task<Animal> AddAnimalAsync(Animal animal);
        public Task<Animal> UpdateAnimalAsync(int id, Animal animal);
        public Task<int> DeleteAnimalAsync(int id);
    }
}
