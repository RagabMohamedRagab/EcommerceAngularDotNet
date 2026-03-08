namespace Ecom.API.Helpers
{
    public class ApiException : IResponse
    {
        public ApiException(bool IsSucess,string Message,int status,string Details)
        {
            this.IsSucess = IsSucess;
            this.Message = Message;
            this.Details = Details;
            this.Status = status;
        }
        public bool IsSucess { get; set ; }
        public string Message { get ; set ; }
        public int Status { get; set; }
        public string Details { get; set; }
    }
}
