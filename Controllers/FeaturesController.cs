using System;
using System.Linq;
using System.Net;
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
        private readonly IFeatureService _featureService;

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

            if (feature == null)
                return NotFound();

            return Ok((FeatureResponse)feature);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]FeatureRequest feature)
        {
            if (!IsValidTimeSpan(feature.Lifetime))
                return BadRequest("lifetime could not be parsed");

            var newFeature = await _featureService.CreateFeature((Feature)feature);

            if (newFeature == null)
                return StatusCode((int)HttpStatusCode.Conflict);

            return CreatedAtRoute("GetFeature", new { FeatureId = newFeature.Id }, (FeatureResponse)newFeature);
        }

        [HttpPut("{featureId}")]
        public async Task<IActionResult> Put(string featureId, [FromBody]FeatureRequest feature)
        {
            if (!IsValidTimeSpan(feature.Lifetime))
                return BadRequest("lifetime could not be parsed");

            var response = await _featureService.UpdateFeature(featureId, (Feature)feature);

            if (response == null)
                return NotFound();

            return Ok((FeatureResponse)response);
        }

        [HttpDelete("{featureId}")]
        public async Task<IActionResult> Delete(string featureId)
        {
            var idFound = await _featureService.DeleteFeature(featureId);

            if (idFound == false)
                return NotFound();

            return Ok();
        }

        private bool IsValidTimeSpan(string timespanAsString)
        {
            if (string.IsNullOrWhiteSpace(timespanAsString))
                return true;

            TimeSpan timespan = default(TimeSpan);
            return TimeSpan.TryParse(timespanAsString, out timespan);
        }
    }
}
