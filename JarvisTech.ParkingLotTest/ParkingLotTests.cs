using JarvisTech.ParkingLot.Biz;
using JarvisTech.ParkingLot.Repository;
using JarvisTech.ParkingLot.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JarvisTech.ParkingLotTest
{
    [Trait("Category", "Park")]
    public class ParkingLotTests
    {
        #region Properties

        private IConfiguration _config;

        private IParkingBiz ParkingBiz;

        private ServiceProvider ServiceProvider;


        public IConfiguration Configuration
        {
            get
            {
                if (_config == null)
                {
                    IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile($"appSettings.json", optional: false);
                    //builder.AddEnvironmentVariables();
                    _config = builder.Build();
                }
                return _config;
            }
        }

        #endregion

        #region Constructor

        public ParkingLotTests()
        {
            ConfigureServices();
        }

        #endregion

        #region TestMethods

        [Fact]
        public async Task ParkVehicle()
        {
            var result = await ParkingBiz.ParkVehicle("motorcyclsagfase");

            Assert.True(result.statusCode != System.Net.HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("scooter")]
        public async Task ParkVehicle(string vehicleType)
        {
            var result = await ParkingBiz.ParkVehicle(vehicleType);

            Assert.True(result.statusCode == System.Net.HttpStatusCode.OK);
        }



        #endregion



        #region Private Methods

        private void ConfigureServices()
        {
            IServiceCollection serviceCollection = new ServiceCollection();
            serviceCollection.AddSingleton(Configuration);

            ServiceProvider = serviceCollection.AddLogging(lb =>
            {
                lb.AddFile(_config.GetSection("Logging"));
                lb.AddConsole();
                lb.SetMinimumLevel(LogLevel.Debug).AddConfiguration(Configuration);
            }).
            AddScoped<IParkingAssignmentService, MotorCylecParkingAssignmentService>().
            AddTransient<IRepository, DbRepository>().
            AddScoped<ITicketingService, TicketingService>().
            BuildServiceProvider();


            ServiceProvider = serviceCollection.AddScoped<IParkingBiz, ParkingBiz>().BuildServiceProvider();
            ParkingBiz = ServiceProvider.GetService<IParkingBiz>();
        }

        #endregion
    }
}
