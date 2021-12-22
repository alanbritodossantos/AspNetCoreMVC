using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App
{
    public class Startup
    {

        public void ConfigureServices(IServiceCollection service)
        {
            service.AddRouting();
        }

        public void Configure(IApplicationBuilder app)
        {
            var builder = new RouteBuilder(app);//classe responsavel por construir rotas 
            builder.MapRoute("Livros/ParaLer", LivrosParaLer);
            builder.MapRoute("Livros/Lendo", LivrosLendo);
            builder.MapRoute("Livros/Lidos", LivrosLidos);
            builder.MapRoute("Cadastro/NovoLivro/{nome}/{autor}", NovoLivroParaLer);
            builder.MapRoute("Livros/Detalhes/{id:int}", ExibeDetalhes);//foi adicionado "int" que signigica que só passa valores inteiross
            builder.MapRoute("Cadastro/NovoLivro", ExibeFormulario);
            builder.MapRoute("Cadastro/Incluir", ProcessaFormulario);
            var rotas = builder.Build();// esse é o cara que pega as informações acima e constroi de fato a rota

            app.UseRouter(rotas);//usando o metodo do aspnet core 
            //app.Run(Roteamento); // usando o metodo do dicionario
        }

        private Task ProcessaFormulario(HttpContext context)
        {
            var livro = new Livro()
            {
                Titulo = context.Request.Form["titulo"].First(),//o "form" pega os valores do formulario por causa que a verbalização mudo para "post" 
                Autor = context.Request.Form["autor"].First(),
            };
            var repo = new LivroRepositorioCSV();
            repo.Incluir(livro);
            return context.Response.WriteAsync("O livro foi  adicionado com sucesso");
        }

        private Task ExibeFormulario(HttpContext context)
        {
            //O sinal "@" permite escrever mais de uma linha
            var html = CarregaArquivoHTML("formulario");
            return context.Response.WriteAsync(html);
        }

        private string CarregaArquivoHTML(string nomeArquivo)
        {
            var nomeCompletoArquivo = $"HTML/{nomeArquivo}.html";
            using (var arquivo = File.OpenText(nomeCompletoArquivo))
            {
                return arquivo.ReadToEnd();//vai ler todo o conteudo e coloca numa string
            }
        }

        private Task ExibeDetalhes(HttpContext context)
        {
            int id = Convert.ToInt32(context.GetRouteValue("id"));
            var repo = new LivroRepositorioCSV(); // <<<<objeto que representa um repositorio
            var livro = repo.Todos.First(l => l.Id == id);
            return context.Response.WriteAsync(livro.Detalhes());
        }

        public Task NovoLivroParaLer(HttpContext context)
        {
            var livro = new Livro()
            {
                Titulo = Convert.ToString(context.GetRouteValue("nome")),
                Autor = Convert.ToString(context.GetRouteValue("autor")),
            };
            var repo = new LivroRepositorioCSV();
            repo.Incluir(livro);
            return context.Response.WriteAsync("O livro foi  adicionado com sucesso");
        }

        public Task Roteamento(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            var caminhosAtendidos = new Dictionary<string, RequestDelegate> {
                {"/Livros/ParaLer", LivrosParaLer },
                {"/Livros/Lendo", LivrosLendo },
                {"/Livros/Lidos", LivrosLidos }

            };

            if (caminhosAtendidos.ContainsKey(context.Request.Path))
            {
                var metodo = caminhosAtendidos[context.Request.Path];
                return metodo.Invoke(context);
            }

            context.Response.StatusCode = 404;
            return context.Response.WriteAsync("Caminho inexistente.");
        }

        public Task LivrosParaLer(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            var conteudoArquivo = CarregaArquivoHTML("para-ler");

            foreach (var livro in _repo.ParaLer.Livros)//listando os livros
            {
                conteudoArquivo = conteudoArquivo.Replace("#NOVO-ITEM#", $"<li>{livro.Titulo} - {livro.Autor} </li>#NOVO-ITEM#");
            }
            conteudoArquivo = conteudoArquivo.Replace("#NOVO-ITEM#","");//depois que terminar de listar, onde estiver "#NOVO-ITEM#" coloque como vazio

            return context.Response.WriteAsync(conteudoArquivo);
        }

        public Task LivrosLendo(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            return context.Response.WriteAsync(_repo.Lendo.ToString());
        }

        public Task LivrosLidos(HttpContext context)
        {
            var _repo = new LivroRepositorioCSV();
            return context.Response.WriteAsync(_repo.Lidos.ToString());
        }
    }
}