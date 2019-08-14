using System;
using Danske.Service.Contracts;
using Danske.Service.Host.Mappers;
using Danske.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Danske.Service.Host.Controllers
{
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public class PathController : ControllerBase
    {
        private readonly ICalculationService _calculationService;
        private readonly IGraphMapper _graphMapper;

        public PathController(ICalculationService calculationService, IGraphMapper graphMapper)
        {
            _calculationService = calculationService;
            _graphMapper = graphMapper;
        }

        [HttpPost("/path")]
        public ActionResult<Graph> Calculate([FromBody] int[] input)
        {
            if (input == null || input.Length == 0)
                throw new ArgumentException("input must be provided and not empty");

            var result = _calculationService.Traverse(input);

            return Ok(_graphMapper.Map(result));
        }
    }
}
