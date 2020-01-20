using FluentValidation;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using ThreatAlerting.Models;
using ThreatAlertProcessing.Activities;
using ThreatAlertProcessing.Application;

[assembly: FunctionsStartup(typeof(Startup))]
namespace ThreatAlertProcessing.Application
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services.AddScoped<AbstractValidator<NewAlertEvent>, NewAlertEventValidator>();
        }
    }
}