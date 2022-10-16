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
        public async Task<ActionResult<List<Animal>>> GetAnimals([FromQuery] AnimalQueryParameters queryParameters)
        {
            return await this.animalRepository.GetAnimalsAsync(queryParameters);
        }

        [HttpGet("/animals/{id}")]
        public async Task<ActionResult<Animal>> GetAnimal([FromRoute]int id)
        {
            this.logger.LogInformation(message: $"GetAnimal({id})");

            try
            {
                Animal animal = await this.animalRepository.GetAnimalAsync(id);
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
        public async Task<ActionResult<Animal>> AddAnimal([FromBody]Animal animal)
        {
            this.logger.LogInformation(message: $"AddAnimal()");

            if (ModelState.IsValid)
            {
                await this.animalRepository.AddAnimalAsync(animal);
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
        public async Task<ActionResult<Animal>> UpdateAnimal([FromRoute]int id, [FromBody]Animal animal)
        {
            this.logger.LogInformation(message: $"UpdateAnimal({id})");

            try
            {
                await this.animalRepository.UpdateAnimalAsync(id, animal);
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
        public async Task<ActionResult<int>> DeleteAnimal([FromRoute]int id)
        {
            this.logger.LogInformation($"DeleteAnimal({id})");

            try
            {
                await this.animalRepository.DeleteAnimalAsync(id);
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
