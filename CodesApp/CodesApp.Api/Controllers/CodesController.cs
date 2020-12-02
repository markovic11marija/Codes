using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodesApp.Model;
using CodesApp.Service.Interfaces;
using CodesApp.Service.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CodesApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CodesController : ControllerBase
    {
        private readonly ICodesService _codesService;

        public CodesController(ICodesService codesService)
        {

            _codesService = codesService;
        }


        [HttpPost]
        public async Task<ActionResult<int>> Add(CodeModel model)
        {
            var result = await _codesService.Add(model);

            if (result > 0)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpPost("{codeId}")]
        public async Task<ActionResult<int>> Update(int codeId, CodeModel model)
        {
            var result = await _codesService.Update(codeId, model);

            if (result > 0)
                return Ok(result);

            return BadRequest(result);
        }


        [HttpPost("acitate-deactivate/{codeId}")]
        public async Task<ActionResult<int>> ActivateDeactivate(int codeId, bool isActive)
        {
            var result = await _codesService.ActivateDeactivate(codeId, isActive);

            if (result > 0)
                return Ok(result);

            return BadRequest(result);
        }

        [HttpDelete]
        public ActionResult Delete(int codeId)
        {
            if (codeId <= 0)
            {
                return BadRequest();
            }

            _codesService.Delete(codeId);

            return NoContent();

        }

        [HttpGet]
        public async Task<ActionResult<IList<Code>>> AllCodes()
        {
            return Ok(await _codesService.GetAllCodes());
        }

    }
}
