using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FeatureService.Models.Api;
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
            return Ok(features);
        }

        // GET api/values/5
        [HttpGet("{featureId}")]
        public async Task<IActionResult> Get(string featureId)
        {
            var feature = await _featureService.GetFeature(featureId);
            return Ok(feature);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Feature feature)
        {
            var newFeature = await _featureService.CreateFeature(feature);
            return CreatedAtRoute("Get", newFeature);
        }

        [HttpPut("{featureId}")]
        public async Task<IActionResult> Put(string featureId, [FromBody]Feature feature)
        {
            var updatedFeature = await _featureService.UpdateFeature(featureId, feature);
            return Ok(updatedFeature);
        }

        [HttpDelete("{featureId}")]
        public async Task<IActionResult> Delete(string featureId)
        {
            await _featureService.DeleteFeature(featureId);
            return Ok();
        }
    }
}
