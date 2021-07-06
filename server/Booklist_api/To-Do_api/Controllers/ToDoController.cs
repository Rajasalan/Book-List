using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ToDo.DB;

namespace To_Do_api.Controllers
{
    
    [RoutePrefix("api/v1/books")]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    //[EnableCors("*", "*", "*")]
    public class ToDoController : GenericController
    {
        [HttpGet]
        [Route("GetBookList")]
        public StatusAPIOutput GetBookList(HttpRequestMessage request)
        {
            StatusAPIOutput statusAPIOutput = null;
            Hashtable Collection = new Hashtable();
            RequestInput requestInput = new RequestInput();
            List<ErrorMessage> errorMessages = new List<ErrorMessage>();
            try
            {
                requestInput = GetRequestInput();
                BLLAccount bLLAccount = new BLLAccount();
                List<usp_fetch_books> todoList = bLLAccount.GetBookList(requestInput);
                Collection.Add("books", todoList);
                statusAPIOutput = new StatusAPIOutput { code = 1, message = "Successfully", callBackObj = Collection, isSuccess = true };
                return statusAPIOutput;
            }
            catch (Exception ex)
            {
                statusAPIOutput = new StatusAPIOutput { code = -3, message = ex.Message, callBackObj = Collection, errorID = "1003", errorMessages = errorMessages };
                return statusAPIOutput;
            }
        }


        [HttpPost]
        [Route("InsertBookList")]
        public StatusAPIOutput InsertBookList(HttpRequestMessage request)
        {
            Hashtable hashtable = new Hashtable();
            Hashtable Collection = new Hashtable();
            StatusAPIOutput statusAPIOutput = new StatusAPIOutput();
            RequestInput requestInput = new RequestInput();
            List<ErrorMessage> errorMessages = new List<ErrorMessage>();
            string mode = ""; int book_id = 0; string title = ""; string author = ""; string description = ""; string description2 = "";
            try
            {
                requestInput = GetRequestInput();
                if (!string.IsNullOrEmpty(Convert.ToString(requestInput.dPO.mode)))
                {
                    mode = requestInput.dPO.mode;
                }
                if (!string.IsNullOrEmpty(Convert.ToString(requestInput.dPO.book_id)))
                {
                    book_id = requestInput.dPO.book_id;
                }
                if (!string.IsNullOrEmpty(Convert.ToString(requestInput.dPO.title)))
                {
                    title = requestInput.dPO.title;
                }               
                if (!string.IsNullOrEmpty(Convert.ToString(requestInput.dPO.author)))
                {
                    author = requestInput.dPO.author;
                }
                if (!string.IsNullOrEmpty(Convert.ToString(requestInput.dPO.description)))
                {
                    description = requestInput.dPO.description;
                }
                if (!string.IsNullOrEmpty(Convert.ToString(requestInput.dPO.description2)))
                {
                    description2 = requestInput.dPO.description2;
                }
            }
            catch (Exception ex)
            {
                statusAPIOutput = new StatusAPIOutput { code = -1, message = ex.Message, callBackObj = Collection, errorID = "1005" };
                return statusAPIOutput;
            }
            try
            {
                //Inserting Console ID
                BLLAccount bLLAccount = new BLLAccount();
                bLLAccount.InsertBooks(requestInput,  mode,  book_id,  title,  author,  description,  description2);
                statusAPIOutput = new StatusAPIOutput { code = 1, message = "Successfully", callBackObj = Collection, isSuccess = true };
                return statusAPIOutput;
            }
            catch (Exception ex)
            {
                statusAPIOutput = new StatusAPIOutput { code = -3, message = ex.Message, callBackObj = Collection, errorID = "1005", errorMessages = errorMessages };
                return statusAPIOutput;
            }
        }
    }
}
