using System;

namespace DigoErp.Repository.Exceptions
{
    public class DigoErpException : Exception
    {
        public string QueryCauseError;
        public string ActualErrorMessage;
        public int ErrorCode;

        public DigoErpException(string errMessage, int errorCode, string causedQuery, Exception errorHappen)
            : base(errMessage, errorHappen)
        {
            ErrorCode = errorCode;
            QueryCauseError = causedQuery;
            ActualErrorMessage = errorHappen.Message;
        }
        public DigoErpException(string errMessage, int errorCode, Exception errorHappen)
            : base(errMessage, errorHappen)
        {
            ErrorCode = errorCode;
            QueryCauseError = "User Entry";
            ActualErrorMessage = errorHappen.Message;
        }
        public DigoErpException(string errMessage, int errorCode, string causedQuery)
            : base(errMessage)
        {
            ErrorCode = errorCode;
            QueryCauseError = causedQuery;
            ActualErrorMessage = "";
        }
        public DigoErpException(string errMessage, int errorCode)
            : base(errMessage)
        {
            ErrorCode = errorCode;
            QueryCauseError = "User Entry";
            ActualErrorMessage = "";
        }
        public override string ToString()
        {
            return "Error: " + Message + " Returned Error:" + ActualErrorMessage + " Code: " + ErrorCode + " WhatCauseTheError: " + QueryCauseError;
        }
        public string ToSmallString()
        {
            return "خطأ " + ErrorCode + " : " + Message;
        }
    }
}
