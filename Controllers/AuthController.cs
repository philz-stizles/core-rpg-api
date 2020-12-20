using System;
using System.Threading.Tasks;
using AutoMapper;
using core_rpg_mvc.Data;
using core_rpg_mvc.Dtos;
using core_rpg_mvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace core_rpg_mvc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepo;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository authRepo, IMapper mapper)
        {
            _mapper = mapper;
            _authRepo = authRepo;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            try{
                return Ok(await _authRepo.Register(_mapper.Map<User>(dto), dto.Password));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            try{
                return Ok(await _authRepo.Login(dto.Username, dto.Password));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}