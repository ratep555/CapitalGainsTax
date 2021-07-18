using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Extensions;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.ViewModels;
using Core.ViewModels.Charts;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class ChartsController : BaseApiController
    {
        private readonly IChartsService _chartsService;
        private readonly IMapper _mapper;
        public ChartsController(IChartsService chartsService, IMapper mapper)
        {
            _mapper = mapper;
            _chartsService = chartsService;

        }
        [HttpGet("ai")]
        public async Task<ActionResult> ShowAllStocksWithCategories()
        {
            var list = await _chartsService.ShowAllStocksWithCategories();

            if (list == null) return NotFound();

            return Ok(list);
        }
        [HttpGet("miki")]
        public async Task<ActionResult> ShowListOfProfitsOrLosses()
        {
            var email = User.RetrieveEmailFromPrincipal();

            var list = await _chartsService.ShowListOfProfitOrLosses(email);

            if (list == null) return NotFound();

            var list1 = _mapper.Map<IEnumerable<AnnualProfitOrLossDto>>(list);

            return Ok(list);
        }
        [HttpGet]
        public async Task<ActionResult> ShowListOfProfitsOrLosses1()
        {
            var email = User.RetrieveEmailFromPrincipal();

            var list = await _chartsService.ShowListOfProfitOrLosses(email);

            if (list.Count() > 0 ) return Ok( new {list});

            return BadRequest();
        }

    }
}





