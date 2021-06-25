﻿using CarShop.Data;
using CarShop.Services;
using Microsoft.EntityFrameworkCore;
using MyWebServer;
using MyWebServer.Controllers;
using MyWebServer.Results.Views;
using System;
using System.Threading.Tasks;

namespace CarShop
{
    public class StartUp
    {
        public static async Task Main()
        {
            await HttpServer
                .WithRoutes(routes => routes
                    .MapStaticFiles()
                    .MapControllers())
                .WithServices(services => services.Add<IViewEngine, CompilationViewEngine>()
                .Add<CarShopDbContext>()
                .Add<IValidator, Validator>()
                .Add<IPasswordHasher, PasswordHasher>())
                .WithConfiguration<CarShopDbContext>(context => context.Database.Migrate())
                .Start();
        }
    }
}
