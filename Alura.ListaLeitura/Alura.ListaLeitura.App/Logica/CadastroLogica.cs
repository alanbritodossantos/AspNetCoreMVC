using Alura.ListaLeitura.App.HTML;
using Alura.ListaLeitura.App.Negocio;
using Alura.ListaLeitura.App.Repositorio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Alura.ListaLeitura.App.Logica
{
    public class CadastroController
    {

        public string Incluir(Livro livro)
        {
            var repo = new LivroRepositorioCSV();
            repo.Incluir(livro);
            return "O livro foi  adicionado com sucesso";
        }

        public IActionResult ExibeFormulario()
        {
            //O sinal "@" permite escrever mais de uma linha
            //var html = HtmlUtils.CarregaArquivoHTML("formulario");
            var html = new ViewResult { ViewName = "formulario.html" };// viewResult representa o resultado HTML
            return html;
        }

    }
}
