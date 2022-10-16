using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VetApp.Classes;
using VetAppDal.Classes;
using VetAppDal.Repo.Interfaces;

#pragma warning disable S112 // General exceptions should never be thrown

namespace VetAppDal.Repo
{
    public class AnimalsRepository : IAnimalsRepository
    {
        private readonly VetAppDbContext context;

        public AnimalsRepository(VetAppDbContext ctx)
        {
            context = ctx;
        }

        public async Task<List<Animal>> GetAnimalsAsync(AnimalQueryParameters queryParemeters)
        {
            IQueryable<Animal> animals = context.Animals;

            if (!string.IsNullOrEmpty(queryParemeters.Name))
            {
                animals = animals.Where(x => x.Name.ToLower().Contains(queryParemeters.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(queryParemeters.Owner))
            {
                animals = animals.Where(x => x.Owner.ToLower().Contains(queryParemeters.Owner.ToLower()));
            }

            animals = animals
                .Skip(queryParemeters.Size * (queryParemeters.Page - 1))
                .Take(queryParemeters.Size);

            return await animals.ToListAsync();
        }

        public async Task<Animal> GetAnimalAsync(int id)
        {
            if (context.Animals.Any(x => x.Id == id))
            {
                return  await this.context.Animals.FirstAsync(x => x.Id == id);
            }
            else
            {
                throw new Exception($"Animal with id='{id}' is not found.");
            }
        }

        public async Task<Animal> AddAnimalAsync(Animal animal)
        {
            this.context.Add(animal);
            await this.context.SaveChangesAsync();
            return animal;
        }

        public async Task<Animal> UpdateAnimalAsync(int id, Animal animal)
        {
            if (animal.Id != id)
            {
                throw new Exception($"Animal with id='{id}' is not valid.");
            }

            if (context.Animals.Any(x => x.Id == id))
            {
                this.context.Entry(animal).State = EntityState.Modified;
                await this.context.SaveChangesAsync();

                return animal;
            }
            else
            {
                throw new Exception($"Animal with id='{id}' is not found.");
            }
        }

        public async Task<int> DeleteAnimalAsync(int id)
        {
            if (context.Animals.Any(x => x.Id == id))
            {
                Animal animal = context.Animals.First(x => x.Id == id);
                this.context.Animals.Remove(animal);
                await this.context.SaveChangesAsync();
                return id;
            }
            else
            {
                throw new Exception($"Animal with id='{id}' is not found.");
            }
        }
    }
}
