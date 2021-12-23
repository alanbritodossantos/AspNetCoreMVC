using Alura.ListaLeitura.App.Logica;
using Alura.ListaLeitura.App.Mvc;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;

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
            builder.MapRoute("{classe}/{metodo}", RoteamentoPadrao.TratamentoPadrao);
            //builder.MapRoute("Livros/ParaLer", LivrosLogica.ParaLer);
            //builder.MapRoute("Livros/Lendo", LivrosLogica.Lendo);
            //builder.MapRoute("Livros/Lidos", LivrosLogica.Lidos);
            //builder.MapRoute("Livros/Detalhes/{id:int}", LivrosLogica.Detalhes);//foi adicionado "int" que signigica que só passa valores inteiross
            //builder.MapRoute("Cadastro/NovoLivro/{nome}/{autor}", CadastroLogica.NovoLivro);
            //builder.MapRoute("Cadastro/ExibeFormulario", CadastroLogica.ExibeFormulario);
            //builder.MapRoute("Cadastro/Incluir", CadastroLogica.Incluir);
            var rotas = builder.Build();// esse é o cara que pega as informações acima e constroi de fato a rota

            app.UseRouter(rotas);//usando o metodo do aspnet core 
            //app.Run(Roteamento); // usando o metodo do dicionario
        }  
    }
}