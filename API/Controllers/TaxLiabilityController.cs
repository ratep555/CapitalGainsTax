using System.Threading.Tasks;
using API.Errors;
using API.Extensions;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class TaxLiabilityController : BaseApiController
    {
        private readonly ITaxLiabilitiesService _taxLiabilitiesService;
        private readonly ISurtaxService  _surtaxService;
        private readonly IMapper _mapper;
        public TaxLiabilityController(ITaxLiabilitiesService taxLiabilitiesService, IMapper mapper,
        ISurtaxService surtaxService)
        {
            _mapper = mapper;
            _taxLiabilitiesService = taxLiabilitiesService;
            _surtaxService = surtaxService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TaxLiabilityDTO>> GetTaxLiability(int id)
        {
            var taxLiability = await _taxLiabilitiesService.FindTaxLiabilityById(id);

            if (taxLiability == null) return NotFound();

            return _mapper.Map<TaxLiability, TaxLiabilityDTO>(taxLiability);
        }
        
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<TaxLiabilityDTO>> GetTaxLiabilityByEmail()
        {
            var email = User.RetrieveEmailFromPrincipal();

            var taxLiability = await _taxLiabilitiesService.FindTaxLiabilityByEmail(email);

            if (taxLiability == null) return NotFound();

            return _mapper.Map<TaxLiability, TaxLiabilityDTO>(taxLiability);
        }

        [Authorize]
        [HttpPut("{id}")]
        public async Task<ActionResult<TaxLiabilityToEditDTO>> EditTaxLiability(int id, 
        [FromBody] TaxLiabilityToEditDTO taxLiabilityToEditDto)
        {
            if (id != taxLiabilityToEditDto.Id)
            {
                return BadRequest(new ApiResponse(400, "You lose:)"));
            }
            
            var email = User.RetrieveEmailFromPrincipal();

            await _taxLiabilitiesService.UpdateUsersSurtaxId(email, taxLiabilityToEditDto.SurtaxId);


            await _taxLiabilitiesService.UpdateTaxLiability1(email, taxLiabilityToEditDto);


            return NoContent();
        }
        [Authorize]
        [HttpPut("up/{id}")]
        public async Task<ActionResult> EditTaxLiability1( 
        int id)
        {           
            var email = User.RetrieveEmailFromPrincipal();

            await _taxLiabilitiesService.UpdateUsersSurtaxId(email, id);

            await _taxLiabilitiesService.UpdateTaxLiability2(email, id);

            return NoContent();
        }

         // ovo ti je za typeahead

    }
}











