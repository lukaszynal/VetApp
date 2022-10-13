using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VetApp.Classes;
using VetAppDal;
using VetAppDal.Classes;
using VetAppDal.Repo.Interfaces;

namespace VetApp.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public sealed class AnimalsController : ControllerBase
    {
        private readonly ILogger logger;
        private readonly IAnimalsRepository animalRepository;

        public AnimalsController(ILogger<AnimalsController> logger, IAnimalsRepository animalRepository)
        {
            this.logger = logger;
            this.animalRepository = animalRepository;
        }

        [HttpGet("/animals")]
        public ActionResult<List<Animal>> GetAnimals([FromQuery] AnimalQueryParameters queryParameters)
        {
            return this.animalRepository.GetAnimals(queryParameters);
        }

        [HttpGet("/animals/{id}")]
        public ActionResult<Animal> GetAnimal([FromRoute]int id)
        {
            this.logger.LogInformation(message: $"GetAnimal({id})");

            try
            {
                Animal animal = this.animalRepository.GetAnimal(id);
                this.logger.LogInformation(message: $"GetAnimal({id}) returned a animal.", animal);
                return animal;
            }
            catch (Exception e)
            {
                this.logger.LogError("Unable to get animal information", e);
                return this.StatusCode(404);
            }
        }

        [HttpPost("/animals")]
        public ActionResult<Animal> AddAnimal([FromBody]Animal animal)
        {
            this.logger.LogInformation(message: $"AddAnimal()");

            if (ModelState.IsValid)
            {
                this.animalRepository.AddAnimal(animal);
                this.logger.LogInformation(message: "UpdateAnimal() successfully completed");
                return animal;
            }
            else
            {
                this.logger.LogError(message: "Unable to add animal");
                return this.StatusCode(400);
            }
        }

        [HttpPut("/animals/{id}")]
        public ActionResult<Animal> UpdateAnimal([FromRoute]int id, [FromBody]Animal animal)
        {
            this.logger.LogInformation(message: $"UpdateAnimal({id})");

            try
            {
                this.animalRepository.UpdateAnimal(id, animal);
                this.logger.LogInformation($"UpdateAnimal({id}) successfully completed");
                return animal;
            }
            catch (Exception e)
            {
                this.logger.LogError("Unable to update animal information", e);
                return this.StatusCode(400);
            }
        }

        [HttpDelete("/animals/{id}")]
        public ActionResult<int> DeleteAnimal([FromRoute]int id)
        {
            this.logger.LogInformation($"DeleteAnimal({id})");

            try
            {
                this.animalRepository.DeleteAnimal(id);
                this.logger.LogInformation($"DeleteAnimal({id}) successfully completed");
                return id;
            }
            catch (Exception e)
            {
                this.logger.LogError("Unable to get animal information", e);
                return this.StatusCode(404);
            }
        }
    }
}
