using RESTService.Services.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace RESTService.Console
{
    public static class Program
    {
        private static readonly IQueueService _queueService;
        static bool flag = true;

        static void Main(string[] args)
        {
            var tarefa = new Thread(ExecutarTarefa);
            tarefa.Start();
        }
        static void ExecutarTarefa()
        {
            int seq = 0;
            while (flag)
            {

                if (seq == 4) flag = false;
                Thread.Sleep(TimeSpan.FromSeconds(3));
                seq++;
            }

        }
    }
}

