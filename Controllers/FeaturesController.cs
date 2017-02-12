using System.Linq;
using System.Threading.Tasks;
using FeatureService.Models.Api;
using FeatureService.Models.Domain;
using FeatureService.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FeatureService.Controllers
{
    [Route("api/[controller]")]
    public class FeaturesController : Controller
    {
        private IFeatureService _featureService;

        public FeaturesController(IFeatureService featureService)
        {
            _featureService = featureService;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAll()
        {
            var features = await _featureService.GetAllFeatures();
            var responses = features.Select(f => (FeatureResponse)f);
            return Ok(responses);
        }

        // GET api/values/5
        [HttpGet("{featureId}", Name = "GetFeature")]
        public async Task<IActionResult> Get(string featureId)
        {
            var feature = await _featureService.GetFeature(featureId);
            return Ok((FeatureResponse)feature);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]FeatureRequest feature)
        {
            var newFeature = await _featureService.CreateFeature((Feature)feature);
            return CreatedAtRoute("GetFeature", new { FeatureId = newFeature.Id }, (FeatureResponse)newFeature);
        }

        [HttpPut("{featureId}")]
        public async Task<IActionResult> Put(string featureId, [FromBody]FeatureRequest feature)
        {
            var response = await _featureService.UpdateFeature(featureId, (Feature)feature);
            return Ok((FeatureResponse)response);
        }

        [HttpDelete("{featureId}")]
        public async Task<IActionResult> Delete(string featureId)
        {
            await _featureService.DeleteFeature(featureId);
            return Ok();
        }
    }
}
