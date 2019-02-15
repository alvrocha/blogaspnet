using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog.DAO;
using blog.Filters;
using blog.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace blog.Controllers
{
    public class UsuarioController : Controller
    {
        private UsuarioDAO dao;

        public UsuarioController(UsuarioDAO dao)
        {
            this.dao = dao;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Autentica(LoginViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = this.dao.Busca(viewModel.LoginName, viewModel.Password);
                if(usuario != null)
                {
                    HttpContext.Session.SetString("usuario", JsonConvert.SerializeObject(usuario));
                    return RedirectToAction("index", "post", new { area = "Admin" });
                }
                else
                {
                    ModelState.AddModelError("login.Invalido","Login ou senha incorretos");
                }
            }
            return View("Login", viewModel);
        }

        [HttpGet]
        public IActionResult NovoUser()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastra(RegistroViewModel model)
        {
            if (ModelState.IsValid)
            {
                Usuario usuario = new Usuario()
                {
                    Nome = model.LoginName,
                    Email = model.Email,
                    Senha = model.Senha
                };
                dao.Adiciona(usuario);
                return RedirectToAction("Index","Post",new { Area="Admin" } );
            }
            else
            {
                return View("NovoUser", model);
            }
        }

    }
}