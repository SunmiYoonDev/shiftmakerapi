using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Dayoff.BLL.Repositories;
using Dayoff.DAL.Models;
using Dayoff.DAL.Models.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Dayoff.WebAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;

        public UserController(IUserRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
        }
        [HttpPut]
        public IActionResult Edit(UserForEditResource userProfile)
        {
            if (_repo.FindUserById(userProfile.id) == null)
                return Ok(new { error = "100" });
            _repo.Edit(userProfile);

            var data = "success";
            return Ok(new { data });
        }
        [HttpDelete]
        public IActionResult DeleteUser(Guid userId)
        {
            var deleteUser = _repo.FindUserById(userId);
            if (deleteUser == null)
                return Ok(new { error = "100" });
            _repo.Delete(userId);

            var data = "success";

            return Ok(new { data });
        }
    }
}
