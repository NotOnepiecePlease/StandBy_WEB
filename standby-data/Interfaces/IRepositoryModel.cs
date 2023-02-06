namespace standby_data.Interfaces
{
    public interface IRepositoryModel<T> where T : class
    {
        T Adicionar(T entity);
        void Deletar(T entity);
        T Atualizar(T entity);
        T BuscarPorID(int id);
        List<T> BuscarTodos();

        void SalvarModificacoes();
    }
}