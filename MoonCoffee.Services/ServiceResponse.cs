using System;

namespace MoonCoffee.Services
{
    public class ServiceResponse<T>
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public DateTime Time { get; set; }
    }
}