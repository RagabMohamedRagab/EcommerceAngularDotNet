namespace Ecom.API.Helpers
{
    public interface IResponseResult<T> where T : class
    {
        public T Entity { get; set; }
    }
}
