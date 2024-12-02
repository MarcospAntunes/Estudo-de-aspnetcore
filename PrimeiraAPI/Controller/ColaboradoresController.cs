using Microsoft.AspNetCore.Mvc;
using WebApi.Model;
using WebApi.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace WebApi.Controllers {
  [ApiController]
  [Route("api/v1/colaboradores")]
  public class ColaboradoresController: ControllerBase {
    private readonly IColaboradoresRepository colaboradoresRepository;

    public ColaboradoresController(IColaboradoresRepository colaboradoresRepositoryArg) {
      colaboradoresRepository = colaboradoresRepositoryArg ?? throw new ArgumentNullException();
    }

    [Authorize]
    [HttpPost]
    public IActionResult Add([FromForm] ColaboradoresViewModel colaboradoresView) {
      var filePath = Path.Combine("Storage", colaboradoresView.foto.FileName);
      Stream fileStream = new FileStream(filePath, FileMode.Create);
      colaboradoresView.foto.CopyTo(fileStream);

      var colaborador = new Colaboradores(colaboradoresView.nomeCompleto, colaboradoresView.CPF, colaboradoresView.nascimento, filePath);
      colaboradoresRepository.Add(colaborador);

      return Ok();
    }

    [Authorize]
    [HttpPost]
    [Route("{id}/download")]
    public IActionResult DownloadFoto(int id) {
      var colaborador = colaboradoresRepository.Get(id);
      var dataBytes = System.IO.File.ReadAllBytes(colaborador.foto);

      return File(dataBytes, "image/png");
    }

    [Authorize]
    [HttpGet]
    public IActionResult Get() {
      var colaboradores = colaboradoresRepository.Get();

      return Ok(colaboradores);
    }
  }
}