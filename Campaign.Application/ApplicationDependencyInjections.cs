using Campaign.Application.Commands;
using Campaign.Application.Queries;
using Campaign.Application.Validators;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Campaign.Application
{
    public static class ApplicationDependencyInjections
    {
        public static IServiceCollection Register(IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddTransient<IValidator<CreateProductCommand>, CreateProductCommandValidator>();
            services.AddTransient<IValidator<CreateCampaignCommand>, CreateCampaignCommandValidator>();
            services.AddTransient<IValidator<CreateOrderCommand>, CreateOrderCommandValidator>();
            services.AddTransient<IValidator<IncreaseTimeCommand>, IncreaseTimeCommandValidator>();

            services.AddTransient<IValidator<GetCampaignByCodeQuery>, GetCampaignByCodeQueryValidator>();
            services.AddTransient<IValidator<GetProductByCodeQuery>, GetProductByCodeQueryValidator>();

            services.AddMediatR(new Type[]
            {
                typeof(CreateProductCommand),
                typeof(CreateCampaignCommand),
                typeof(CreateOrderCommand),
                typeof(IncreaseTimeCommand),

                typeof(GetCampaignByCodeQuery),
                typeof(GetProductByCodeQuery)
            }
            );
            return services;
        }
    }
}
