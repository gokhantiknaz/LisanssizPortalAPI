using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Humanity.Application.Models.DTOs
{
    public class CustomResponseDto<T>
    {
        public T Data { get; set; }

        //Clientlara StatusCode u göstermeyeceğiz.
        [JsonIgnore]
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; }

        public List<string> EkDetay { get; set; }

        public static CustomResponseDto<T> Success(int statusCode, T data)
        {
            return new CustomResponseDto<T> { Data = data, StatusCode = statusCode };
        }

        public static CustomResponseDto<T> Success(int statusCode)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode };
        }

        public static CustomResponseDto<T> Fail(int statusCode, List<string> errors, List<string> ekDetay)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = errors,EkDetay=ekDetay };
        }

        public static CustomResponseDto<T> Fail(int statusCode, string error,string ekDetay)
        {
            return new CustomResponseDto<T> { StatusCode = statusCode, Errors = new List<string> { error }, EkDetay = new List<string> { ekDetay } };
        }
    }
}
