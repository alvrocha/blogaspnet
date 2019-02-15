using blog.DAO;
using blog.DB;
using blog.Filters;
using blog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace blog.Areas.Admin.Controllers
{
    [AutorizacaoFilter]
    [Area("Admin")]
    public class PostController : Controller
    {
        private PostDAO dao;
        
        public PostController(PostDAO dao)
        {
            this.dao = dao;
        }
        
        public IActionResult Index()
        {

            IList<Post> lista = dao.Lista();
            return View(lista);
        }

        public IActionResult Novo()
        {
            Post p = new Post();
            return View(p);
        }

        public IActionResult Adiciona(Post p)
        {
            if (ModelState.IsValid)
            {
                string usuJson = HttpContext.Session.GetString("usuario");
                Usuario logado = JsonConvert.DeserializeObject<Usuario>(usuJson);
                dao.Adiciona(p , logado);
                return RedirectToAction("Index");
            }
            else
            {
                return View("Novo", p);
            }
        }

        public IActionResult Categoria([Bind(Prefix = "ID")] string Categoria)
        {
            IList<Post> Post = dao.BuscaCategoria(Categoria);
            return View("index" , Post);
        }

        public IActionResult RemovePost(int ID)
        {
            dao.Remove(ID);
            return RedirectToAction("index");

        }

        public IActionResult Visualiza(int id)
        {
            Post post = dao.BuscaPorID(id);
            return View(post);
        }

        public IActionResult EditaPost(Post post)
        {
            dao.Atualiza(post);
            return RedirectToAction("index");
        }

        public IActionResult Atualiza(Post post)
        {
            dao.Atualiza(post);
            return RedirectToAction("index");
        }

        public IActionResult PublicaPost(int ID)
        {
            dao.Publica(ID);
            return RedirectToAction("index");
        }

        public JsonResult GetAllList()
        {
            IList<Post> lista = dao.Lista();
            return Json(lista);
        }
    }
}