using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog.DAO;
using blog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace blog.Areas.API.Controllers
{
    [Area("API")]
    [Route("api/post")]
    [ApiController]
    public class PostAPIController : ControllerBase
    {
        private readonly PostDAO dao;

        public PostAPIController(PostDAO dao)
        {
            this.dao = dao;
        }

        [Route("lista")]
        [HttpGet]
        public IActionResult BuscaTodosPosts()
        {
            return Ok(dao.Lista());
        }

        [Route("{Id}")]
        [HttpGet]
        public IActionResult BuscaPostPorId(int Id)
        {
            return Ok(dao.BuscaPorID(Id));
        }

        [Route("titulo/{termo}")]
        [HttpGet]
        public IActionResult BuscaPostPorTermo(string termo)
        {
            return Ok(dao.BuscaPeloTermo(termo));
        }

        [Route("novo")]
        [HttpPost]
        public IActionResult CadastraPost([FromBody] Post post)
        {
            if (ModelState.IsValid)
            {
                dao.Insere(post);
                return CreatedAtAction("BuscaPorId", new { id = post.Id }, post);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }
    }
}
