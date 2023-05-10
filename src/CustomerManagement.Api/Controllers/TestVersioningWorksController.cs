using System.Diagnostics;
using CustomerManagement.Common.Logging;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace CustomerManagement.Api.Controllers
{
    /// <summary>
    ///     Item object
    /// </summary>
    public record Item
    {
        /// <summary>
        ///     Item ID (in Guid form)
        /// </summary>
        public Guid Id { get; set; }
    }

    /// <summary>
    ///     Basic controller for testing with
    /// </summary>
    public class TestVersioningWorksController : BaseV1ApiController
    {
        private readonly ILogger<TestVersioningWorksController> _logger;

        /// <summary>
        ///     ctor
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public TestVersioningWorksController(IMediator mediator, ILogger<TestVersioningWorksController> logger) : base(mediator)
        {
            _logger = logger;
        }

        /// <summary>
        ///     List all items
        /// </summary>
        /// <returns></returns>
        
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IEnumerable<Item>), 200)]
        [HttpGet]
        public async Task<IEnumerable<Item>> GetItems()
        {
            var sw = new Stopwatch();
            sw.Start();
            
            var results = Enumerable
                .Range(1, 100)
                .Select(x => new Item { Id = Guid.NewGuid() }).ToList();
            
            _logger.LogInformation("{Method} {Message} {Elapsed}", nameof(GetItems), "Done", sw.Elapsed);

            sw.Stop();
            
            return results;
        }
    }
}