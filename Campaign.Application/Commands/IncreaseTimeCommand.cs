using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campaign.Application.Commands
{
    public class IncreaseTimeCommand : IRequest<CommonResult<object>>
    {
        public int Time { get; set; }
    }
}
