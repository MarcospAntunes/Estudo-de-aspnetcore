namespace WebApi.Model
{
  public interface IColaboradoresRepository
  {
    void Add(Colaboradores colaboradores);
    List<Colaboradores> Get(int pageNumber, int pageQuantity);
    Colaboradores? Get(int id);
  }
}