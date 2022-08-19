namespace MainWeb.Services{
    public interface IBaseService<T>{
        Task<T> Add(T item);
        Task<T> Update(T item);
        Task<bool> Delete(T item);
    }
}