namespace WebApi.ViewModel
{
  public class ColaboradoresViewModel
  {
    public string nomeCompleto { get; set; }
    public string CPF { get; set; }
    public string nascimento { get; set; }
    public IFormFile foto { get; set; }
  }
}