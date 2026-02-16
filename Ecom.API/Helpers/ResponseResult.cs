namespace Ecom.API.Helpers
{
    public class ResponseResult<T> : IResponse, IResponseResult<T> where T : class
    {
        public ResponseResult()
        {
            
        }
        public ResponseResult(bool IsSucess):this(IsSucess,string.Empty,200,null)
        {
           
        }

        public ResponseResult(bool IsSucess,string Message):this(IsSucess,Message,200,null)
        {
            
        }

        public ResponseResult(bool IsSucess,string Message,int status):this(IsSucess,Message,status,null)
        {
           
        }
        public ResponseResult(bool IsSucess,string Message,int status=200,T Entity=null)
        {
            this.IsSucess = IsSucess;
            this.Message = Message;
            this.Status = status;
            this.Entity = Entity;
        }
      
        
        
        public bool IsSucess { get ; set ; }
        public string Message { get ; set ; }
        public int Status { get; set; }
        public T Entity { get ; set ; }
    }
}
