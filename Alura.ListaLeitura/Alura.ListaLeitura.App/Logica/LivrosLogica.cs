using Alura.ListaLeitura.App.HTML;
using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App.Logica
{
    //nessa classe tem metodos responsaveis pela exibição de livros
    public class LivrosLogica
    {

        private static string CarregaLista(IEnumerable<Livro> livros)
        {
            var _repo = new LivroRepositorioCSV();
            var conteudoArquivo = HtmlUtils.CarregaArquivoHTML("lista");

            foreach (var livro in livros)//listando os livros
            {
                conteudoArquivo = conteudoArquivo.Replace("#NOVO-ITEM#", $"<li>{livro.Titulo} - {livro.Autor} </li>#NOVO-ITEM#");
            }
            return conteudoArquivo.Replace("#NOVO-ITEM#", "");//depois que terminar de listar, onde estiver "#NOVO-ITEM#" coloque como vazio
        }

        public static Task LivrosParaLer(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            var html = CarregaLista(_repo.ParaLer.Livros);
            return context.Response.WriteAsync(html);
        }

        public static Task LivrosLendo(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            return context.Response.WriteAsync(_repo.Lendo.ToString());
        }

        public static Task LivrosLidos(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            return context.Response.WriteAsync(_repo.Lidos.ToString());
        }

        public static Task ExibeDetalhes(HttpContext context)
        {
            int id = Convert.ToInt32(context.GetRouteValue("id"));
            var repo = new LivroRepositorioCSV(); // <<<<objeto que representa um repositorio
            var livro = repo.Todos.First(l => l.Id == id);
            return context.Response.WriteAsync(livro.Detalhes());
        }
    }
}
