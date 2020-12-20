using core_rpg_mvc.Dtos;
using core_rpg_mvc.Models;
using core_rpg_mvc.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace core_rpg_mvc.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CharacterController : ControllerBase
    {
        private static Character knight = new Character();
        private readonly ICharacterService _characterSrv;

        public CharacterController(ICharacterService characterSrv)
        {
            _characterSrv = characterSrv;
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await _characterSrv.GetAll());
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = false, Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                return Ok(await _characterSrv.Get(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> Post(CharacterDto dto)
        {
            return Ok(await _characterSrv.Create(dto));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, CharacterDto dto)
        {
            try
            {
                return Ok(await _characterSrv.Update(id, dto));
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = false, Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                return Ok(await _characterSrv.Delete(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = false, Message = ex.Message });
            }
        }

        [HttpDelete("AddCharacterSkill")]
        public async Task<IActionResult> AddCharacterSkill(CharacterSkillDto dto)
        {
            try
            {
                return Ok(await _characterSrv.AddCharacterSkill(dto));
            }
            catch (Exception ex)
            {
                return BadRequest(new { Status = false, Message = ex.Message });
            }
        }
    }
}