﻿using Microsoft.Extensions.Logging;
using System;

namespace Prodest.Scd.Infrastructure.Mapping
{
    public class ProcessoEletronicoLoggerFactory : ILoggerFactory
    {
        public void AddProvider(ILoggerProvider provider) { }

        public ILogger CreateLogger(string categoryName)
        {
            if (categoryName == "Microsoft.EntityFrameworkCore.Database.Command")
            {
                return new ProcessoEletronicoLogger(categoryName);
            }

            return new NullLogger();
        }

        public void Dispose() { }

        public class ProcessoEletronicoLogger : ILogger
        {
            public string CategoryName { get; set; }

            public ProcessoEletronicoLogger(string category)
            {
                CategoryName = category;
            }

            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                if (eventId.Name.Equals("Microsoft.EntityFrameworkCore.Database.Command.CommandExecuting"))
                {
                    Console.WriteLine();
                    Console.WriteLine(formatter(state, exception));
                    Console.WriteLine();
                }
            }

            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }
        }

        private class NullLogger : ILogger
        {
            public bool IsEnabled(LogLevel logLevel)
            {
                return false;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            { }

            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }
        }
    }
}
