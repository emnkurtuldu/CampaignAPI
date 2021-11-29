using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campaign.Application.Commands
{
    public class CreateProductCommand : IRequest<CommonResult<object>>
    {
        public string ProductCode { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }

    }
}
