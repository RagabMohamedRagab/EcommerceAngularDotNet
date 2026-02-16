namespace Ecom.API.Helpers
{
    public interface IResponse
    {
        public bool IsSucess { get; set; }
        public string Message { get; set; }

        public int Status { get; set; }
    }
}
