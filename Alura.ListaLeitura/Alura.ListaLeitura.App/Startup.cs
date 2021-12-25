using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Alura.ListaLeitura.App
{
    // essa classe contém os metodos de inicialização apenas! 
    public class Startup
    {

        public void ConfigureServices(IServiceCollection service)
        {
            service.AddMvc();//adicionando framework MVC
        }

        //mapeando rotas para suas logicas especificas 
        public void Configure(IApplicationBuilder app)
        {
            //UseDeveloperExceptionPage => Só pode se utilizado para teste, em produção não é uma boa ideia
            app.UseDeveloperExceptionPage();//mostra o erro no navegador(capturar exceções sem tratamento lançadas)
            app.UseMvcWithDefaultRoute();//usando o mvc como roteamento padrão
        }
    }
}