using System.Net.NetworkInformation;

namespace Ecom.API.Helpers
{
    public class ResponsePageResult<T> : IResponse, IResponsePageResult<T> where T : class
    {

        public ResponsePageResult()
        {
            
        }
        public ResponsePageResult(bool IsSucess) : this(IsSucess, string.Empty, 0, [],0)
        { 
        }

        public ResponsePageResult(bool IsSucess,string Message) : this(IsSucess, Message, 0, [],0)
        {
            
        }
        public ResponsePageResult(bool IsSucess,string Message,int status) : this(IsSucess, Message, status, [],0)
        {
            
        }
        public ResponsePageResult(bool IsSucess, string Message, int Status, List<T> Entities, int Total): this(IsSucess, Message, Status, [], 0,0,0)
        {

        }
       
         
        public ResponsePageResult(bool IsSucess,string Message,int Status,List<T> Entities,int Total, int pagesize, int pagenumber)
        {
            this.IsSucess = IsSucess;
            this.Message = Message;
            this.Status = Status;
            this.Entities = Entities;
            this.TotalCount = Total;
            this.PageSize = pagesize;
            this.PageNumber = pagenumber;
        }
        public bool IsSucess { get; set; }
        public string Message { get ; set ; }
        public int Status { get; set ; }
        public List<T> Entities { get; set ; }
        public int TotalCount { get; set; }
        public int PageNumber { get ; set ; }
        public int PageSize { get ; set; }
    }
}
