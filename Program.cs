
using ElevatorChallenge.Application;
using ElevatorChallenge.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace ElevatorChallenge
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Create a service collection
            var services = new ServiceCollection();

            // Register dependencies
            services.AddTransient<IElevatorService, ElevatorService>();

            // Build the service provider
            var serviceProvider = services.BuildServiceProvider();

            // Resolve and use dependencies
            IElevatorService elevatorService = serviceProvider.GetRequiredService<IElevatorService>();

            // Initialize Elevator App
            ElevatorApp elevatorApp = new ElevatorApp(elevatorService);

            // Run
            await elevatorApp.Run(elevatorMaxCapacity: 8);

            // Dispose serviceProvider when done
            if (serviceProvider is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}