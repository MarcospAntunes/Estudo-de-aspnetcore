using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Model {

  [Table("colaboradores")]
  public class Colaboradores {
    [Key]
    public int id { get; private set; }
    public string nomeCompleto { get; private set; }
    public string CPF { get; private set; }
    public string nascimento { get; private set; }

    public Colaboradores(string nomeCompleto, string CPF, string nascimento) {
      this.nomeCompleto = nomeCompleto;
      this.CPF = CPF;
      this.nascimento = nascimento;
    }
  }
}
