﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Threading;
using MatthiWare.CommandLine;
using Harpocrates_Secrets.CommandLineArg;
using Newtonsoft.Json;

namespace Harpocrates_Secrets
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Console.WriteLine("Helllo World");
            //CreateWebHostBuilder(args).Build().Run();
            ProcessComandLineArguments(args);
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();

        //FunctionName: IntroducitonLine
        //Funcation Parameters->ReturnType: Void -> Void
        //Function Purpose: To introduce the purpose of the program in a slighly more dynamic way
        static void IntroductionLine()
        {
            //Because I am extra, I want the message to print letter by letter
            string introLine = "Hello, my name is Harpocrates, the gatekeeper of secrets. What secrets would you like to uncover?";
            for (int i = 0; i < introLine.Length; i++)
            {
                Console.Write(introLine[i]);
                Thread.Sleep(75);
            }
        }

        static void ProcessComandLineArguments(string[] args)
        {
            var options = new CommandLineParserOptions
            {
                AppName = "HARPOCRATES"
            };
            var parser = new CommandLineParser<CLIArgs>(options);
            var result = parser.Parse(args);
            if (result.HasErrors)
            {
                Console.Error.WriteLine("Error in command line arguments");
                System.Environment.Exit(1);
            }
            ParseJson(result.Result.JSONProfile);
            

        }

        static void ParseJson(dynamic json)
        {
            using (StreamReader jsonReader = new StreamReader(json))
            {
                var readInJson = jsonReader.ReadToEnd();
                var items = JsonConvert.DeserializeObject<Dictionary<String, String>>(readInJson);
                foreach (var keys in items.Keys)
                {
                    Console.WriteLine(keys + ":" + items[keys]);
                }

            }



            //string jsonResponse = System.IO.File.ReadAllText(json);
            //dynamic jsonStringToDict = JsonConvert.DeserializeObject<Dictionary<String, String>>(jsonResponse);
            //Console.WriteLine(jsonStringToDict);
        }


    }


}


