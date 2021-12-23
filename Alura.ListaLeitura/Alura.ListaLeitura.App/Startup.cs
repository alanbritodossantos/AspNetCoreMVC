using Alura.ListaLeitura.App.Logica;
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
    // essa classe contém os metodos de inicialização apenas! 
    public class Startup
    {

        public void ConfigureServices(IServiceCollection service)
        {
            service.AddRouting();// injetando o serviço de roteamento
        }

        //mapeando rotas para suas logicas especificas 
        public void Configure(IApplicationBuilder app)
        {
            var builder = new RouteBuilder(app);//classe responsavel por construir rotas 
            builder.MapRoute("Livros/ParaLer", LivrosLogica.LivrosParaLer);
            builder.MapRoute("Livros/Lendo", LivrosLogica.LivrosLendo);
            builder.MapRoute("Livros/Lidos", LivrosLogica.LivrosLidos);
            builder.MapRoute("Livros/Detalhes/{id:int}", LivrosLogica.ExibeDetalhes);//foi adicionado "int" que signigica que só passa valores inteiross
            builder.MapRoute("Cadastro/NovoLivro/{nome}/{autor}", CadastroLogica.NovoLivroParaLer);
            builder.MapRoute("Cadastro/NovoLivro", CadastroLogica.ExibeFormulario);
            builder.MapRoute("Cadastro/Incluir", CadastroLogica.ProcessaFormulario);
            var rotas = builder.Build();// esse é o cara que pega as informações acima e constroi de fato a rota

            app.UseRouter(rotas);//usando o metodo do aspnet core 
            //app.Run(Roteamento); // usando o metodo do dicionario
        }  
    }
}