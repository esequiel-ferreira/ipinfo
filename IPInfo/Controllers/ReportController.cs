/*
    Author:     Esequiel de Paiva Ferreira - esequiel.paiva@gmail.com
    Date:       11/04/2024
    Purpose:    Demo Asp.net Core Web Api.    
*/

using IPInfo.ApiControllers;
using IPInfo.Entities.Model;
using IPInfo.Entities.Report;
using Microsoft.AspNetCore.Mvc; 

namespace IPInfo.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReportController : Controller
    {
        private readonly ILogger<IPInfoController> _logger;
        private readonly IReport _report; 

        public ReportController(ILogger<IPInfoController> logger, IReport report)
        {
            _logger = logger;
            _report = report;
        }

        [HttpGet]
        public async Task<ActionResult<Country>> GetAdressPerCountry([FromQuery(Name = "twoLetterCodes")] string[] twoLetterCodes)
        {
            try
            {
                IP2CountryReport ip2CountryReport = new IP2CountryReport(_report);
                var countriesSummary = ip2CountryReport.GetCountriesSummary(twoLetterCodes);
                return Ok(countriesSummary);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        } 
    }
}
