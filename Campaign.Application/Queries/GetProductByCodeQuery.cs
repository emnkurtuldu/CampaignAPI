using Campaign.Application.Interfaces;
using Campaign.Domain.Models.Products;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Campaign.Application.Queries
{
    public class GetProductByCodeQuery : IRequest<CommonResult<Product>>
    {
        public string ProductCode { get; }

        public GetProductByCodeQuery(string productCode)
        {
            this.ProductCode = productCode;
        }
    }
}
