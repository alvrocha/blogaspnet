using blog.DB;
using blog.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace blog.DAO
{
    public class UsuarioDAO
    {
        private BlogContext contexto;

        public UsuarioDAO(BlogContext contexto)
        {
            this.contexto = contexto;
        }

        public Usuario Busca(string login, string senha)
        {
            return contexto.Usuarios
                .Where(u => u.Nome.Equals(login) && u.Senha.Equals(senha))
                .FirstOrDefault<Usuario>();
        }

        public void Adiciona(Usuario usuario)
        {
            contexto.Usuarios.Add(usuario);
            contexto.SaveChanges();
        }
    }
}
