using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campaign.Application.Commands
{
    public class CreateOrderCommand : IRequest<CommonResult<object>>
    {
        public string ProductCode { get; set; }
        public int Quantity { get; set; }
    }
}
