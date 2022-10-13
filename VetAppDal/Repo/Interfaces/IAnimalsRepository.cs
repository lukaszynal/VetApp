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
        public List<Animal> GetAnimals(AnimalQueryParameters queryParemeters);
        public Animal GetAnimal(int id);
        public Animal AddAnimal(Animal animal);
        public Animal UpdateAnimal(int id, Animal animal);
        public int DeleteAnimal(int id);
    }
}
