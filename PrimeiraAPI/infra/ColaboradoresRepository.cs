using WebApi.Infra;
using WebApi.Model;

namespace WebApi.Infraestrutura {
  public class ColaboradoresRepository: IColaboradoresRepository {

    private readonly ConnectionContext context = new ConnectionContext();
    public void Add(Colaboradores colaboradores) {
      context.Colaboradores.Add(colaboradores);
      context.SaveChanges();
    }

    public List<Colaboradores> Get() {
      return context.Colaboradores.ToList();
    }

    public Colaboradores? Get(int id) {
      return context.Colaboradores.Find(id);
    }
  }
}