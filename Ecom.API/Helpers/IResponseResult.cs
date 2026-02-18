namespace Ecom.API.Helpers
{
    public interface IResponseResult<T> 
    {
        public T Entity { get; set; }
    }
}
