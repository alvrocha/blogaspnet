using blog.DB;
using blog.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace blog.DAO
{
    public class PostDAO
    {
        private BlogContext ctx;
        public object Categoria { get; set; }

        public PostDAO(BlogContext ctx)
        {
            this.ctx = ctx;
        }

        public void Adiciona(Post p, Usuario u)
        {
            ctx.Usuarios.Attach(u);
            p.Autor = u;
            ctx.Posts.Add(p);
            ctx.SaveChanges();
        }

        public void Insere(Post p)
        {
            ctx.Posts.Add(p);
            ctx.SaveChanges();
        }

        public IList<Post> Lista()
        {
            return ctx.Posts.ToList();
        }

        public IList<Post> BuscaCategoria(string Categoria)
        {
            return ctx.Posts.Where(Post => Post.Categoria.Contains(Categoria)).ToList();
        }

        public void Remove(int ID)
        {
            Post Post = ctx.Posts.Find(ID);
            ctx.Posts.Remove(Post);
            ctx.SaveChanges();
        }

        public Post BuscaPorID(int id)
        {
            Post Post = ctx.Posts.Find(id);
            return Post;
        }

        public void Atualiza(Post p)
        {
            ctx.Entry(p).State = EntityState.Modified;
            ctx.SaveChanges();
        }

        public void Publica(int ID)
        {
            Post P = ctx.Posts.Find(ID);
            P.Publicado = true;
            P.DataPublicacao = DateTime.Now;
            ctx.SaveChanges();
        }

        public IList<Post> ListaPublicados()
        {
            return ctx.Posts.Where(p => p.Publicado == true)
                .OrderByDescending(p => p.DataPublicacao).ToList();
        }

        public IList<Post> BuscaPeloTermo(string termo)
        {
            var aux = ctx.Posts
                    .Where(p => (p.Publicado) && (p.Titulo.Contains(termo) || p.Resumo.Contains(termo)))
                    .ToList();
            return aux;
        }

    }
}
