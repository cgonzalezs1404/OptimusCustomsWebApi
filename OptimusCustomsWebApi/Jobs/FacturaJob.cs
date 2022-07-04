using OptimusCustomsWebApi.Data;
using OptimusCustomsWebApi.Enum;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OptimusCustomsWebApi.Jobs
{
    public class FacturaJob : IJob
    {
        

        public Task Execute(IJobExecutionContext context)
        {
            return new Task(() =>
            {
                
            });

        }
    }
}
