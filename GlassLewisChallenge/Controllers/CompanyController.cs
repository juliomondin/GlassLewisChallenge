using GlassLewisChallenge.Domain;
using GlassLewisChallenge.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace GlassLewisChallenge.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CompanyController : Controller
    {
        private readonly ILogger<CompanyController> _logger;
        private readonly ICompanyService _companyService;
        private readonly IValidatorService _validator;

        public CompanyController(ILogger<CompanyController> logger, ICompanyService companyService, IValidatorService validator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _companyService = companyService ?? throw new ArgumentNullException(nameof(companyService));
            _validator = validator ?? throw new ArgumentNullException(nameof(validator));
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<Company>> GetAll()
        {
            var result = _companyService.GetAll();
            return new OkObjectResult(result);
        }

        [HttpGet]
        [Route("{companyId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Company> Get(long companyId)
        {
            var result = _companyService.Get(companyId);
            return new OkObjectResult(result);
        }

        [HttpGet]
        [Route("isin/{isin}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<Company> GetByIsin(string isin)
        {
            var result = _companyService.GetByIsin(isin);
            return new OkObjectResult(result);
        }

        [HttpPut]
        [Route("{companyId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Company> Update(int companyId, [FromBody] Company company)
        {
            company.Id = companyId;
            if (_validator.Validate(company, _companyService, true))
            {
                var result = _companyService.Update(company);
                return new OkObjectResult(result);
            }
            return new BadRequestResult();
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Company> Insert([FromBody] Company company)
        {
            if (_validator.Validate(company, _companyService))
            {
                var result = _companyService.Insert(company);
                return new CreatedResult("/company", result);
            }

            return new BadRequestResult();
        }

    }
}
