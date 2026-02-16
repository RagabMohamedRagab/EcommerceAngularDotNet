namespace Ecom.API.Helpers
{
    public interface IResponsePageResult<T> where T : class 
    {
        public List<T> Entities { get; set; }
        public int TotalCount { get; set; }
    }
}
