using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthorAPI.Data;
using AuthorBlazor.Data;
using AuthorBlazor.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AuthorBlazor
{
    public class Program
    {
        public static void Main(string[] args)
        {
            
                CreateHostBuilder(args).Build().Run();
                
                //IAuthorService authorService = new CloudAuthorService();
            
                //Console.WriteLine(authorService.GetAuthorsAsync().Result);
            
                //Console.WriteLine(authorService.GetAsync(1));
            
                //Console.WriteLine(authorService.GetAuthorFirstName(1));
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}