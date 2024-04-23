using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PalyavalsztoV4.Models.V41
{
    public class APIResponse<T>
    {
        public int StatusCode { get; set; } = 0;
        public string? Message { get; set; }
        public T Data { get; set; } = default(T);
        public DateTime Date { get; set; } = DateTime.Now;

    }
}
