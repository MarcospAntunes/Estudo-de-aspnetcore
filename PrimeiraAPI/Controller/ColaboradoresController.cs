using Microsoft.AspNetCore.Mvc;
using WebApi.Model;
using WebApi.ViewModel;

namespace WebApi.Controllers {
  [ApiController]
  [Route("api/v1/colaboradores")]
  public class ColaboradoresController: ControllerBase {
    private readonly IColaboradoresRepository colaboradoresRepository;

    public ColaboradoresController(IColaboradoresRepository colaboradoresRepositoryArg) {
      colaboradoresRepository = colaboradoresRepositoryArg ?? throw new ArgumentNullException();
    }

    [HttpPost]
    public IActionResult Add(ColaboradoresViewModel colaboradoresView) {
      var colaborador = new Colaboradores(colaboradoresView.nomeCompleto, colaboradoresView.CPF, colaboradoresView.nascimento);

      colaboradoresRepository.Add(colaborador);

      return Ok();
    }

    [HttpGet]
    public IActionResult Get() {
      var colaboradores = colaboradoresRepository.Get();

      return Ok(colaboradores);
    }
  }
}