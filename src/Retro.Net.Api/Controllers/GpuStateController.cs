using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Retro.Net.Api.Infrastructure;
using Retro.Net.Api.Models;
using Retro.Net.Api.Services.Interfaces;
using Retro.Net.Wiring;

namespace Retro.Net.Api.Controllers
{
    [ApiRoute("[controller]")]
    public class GpuStateController : Controller
    {
        private readonly IGameBoyContext _context;

        [HttpGet]
        public async Task<GpuStateViewModel> Get()
        {
            var gpu = _context.GetGpu();
            throw new NotImplementedException();
        }
    }
}
