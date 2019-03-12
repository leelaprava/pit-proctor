using System;
using System.Collections.Generic;
using System.Text;
using PitProctor.Interfaces;
using NLog;

namespace PitProctor.Services
{
    public class BarService : IBarService
    {
        private readonly IFooService _fooService;
        public BarService(IFooService fooService)
        {
            _fooService = fooService;
        }

        public void DoSomeRealWork()
        {
            for (int i = 0; i < 10; i++)
            {
                _fooService.DoThing(i);
            }
        }
    }

    public class FooService : IFooService
    {
        private readonly Logger _logger;
        public FooService()//ILoggerFactory loggerFactory)
        {
            _logger = LogManager.GetCurrentClassLogger(); ;
        }

        public void DoThing(int number)
        {
            _logger.Info($"Doing the thing {number}");
            Console.WriteLine($"Doing the thing {number}");
        }
    }
}
