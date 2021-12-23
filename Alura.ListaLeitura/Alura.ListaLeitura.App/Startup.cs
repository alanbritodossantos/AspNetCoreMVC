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
            service.AddMvc();//adicionando framework MVC
        }

        //mapeando rotas para suas logicas especificas 
        public void Configure(IApplicationBuilder app)
        {
            app.UseMvcWithDefaultRoute();//usando o mvc como roteamento padrão
        }
    }
}