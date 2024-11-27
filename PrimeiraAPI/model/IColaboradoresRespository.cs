namespace WebApi.Model {
  public interface IColaboradoresRepository {
    void Add(Colaboradores colaboradores);
    List<Colaboradores> Get();
    Colaboradores? Get(int id);
  }
}