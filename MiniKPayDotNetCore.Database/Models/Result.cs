﻿using MiniKPayDotNetCore.Database.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniKPayDotNetCore.MiniKPay.Database.Models
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public bool IsError { get { return !IsSuccess; } }
        public bool IsValidationError { get { return Type == EnumRespType.ValidationError; } }
        public bool IsSystemError { get { return Type == EnumRespType.SystemError; } }
        private EnumRespType Type { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }

        public static Result<T> Success(T data,string message)
        {
            return new Result<T>()
            {
                IsSuccess = true,
                Type = EnumRespType.Success,
                Data = data,
                Message = message
            };
        }

        public static Result<T> ValidationError(string message,T? data = default)
        {
            return new Result<T>()
            {
                IsSuccess = false,
                Type = EnumRespType.ValidationError,
                Data = data,
                Message = message,

            };
        }

        public static Result<T> SystemError(string message,T? data = default)
        {
            return new Result<T>
            {
                IsSuccess = false,
                Type = EnumRespType.SystemError,
                Data = data,
                Message = message,
            };
        }

       
    }
}
