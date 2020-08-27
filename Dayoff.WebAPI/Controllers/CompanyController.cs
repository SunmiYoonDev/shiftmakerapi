using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dayoff.BLL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Dayoff.DAL.Models.Dto;
using Dayoff.DAL.Models;

namespace Dayoff.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyRepository _repo;
        private readonly IConfiguration _config;

        public CompanyController(ICompanyRepository repo, IConfiguration config)
        {
            _repo = repo;
            _config = config;
        }
        [HttpGet]
        public IActionResult GetList(Guid userId)
        {
            var data = _repo.GetList(userId);
            if (data == null)
                return Ok(new { error = "108" });
            return Ok(new { data });
        }

        [HttpPost]
        public ActionResult<Company> Add(Guid userId, [FromForm] CompanyForAddResource companyForAddDto)
        {
            _repo.Add(userId, companyForAddDto);
            var data = _repo.GetList(userId);
            return Ok(new { data });
        }
    }
}
