using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCTest.Logic
{
    public class Info 
    {
        public string Result { get; set; }
        public string Message { get; set; }

        public Info()
        {
            this.Result = "";
            this.Message = "";
        }

        public Info(string Result, string Message)
        {
            this.Result = Result;
            this.Message = Message;
        }
    }

    public class InfoError : Info
    {
        public string StackTrace { get; set; }

        public InfoError() : base ()
        {
            this.StackTrace = "";            
        }

        public InfoError(string StackTrace, string Result, string Message) : base(Result, Message)
        {
            this.StackTrace = StackTrace;
        }
    }
}