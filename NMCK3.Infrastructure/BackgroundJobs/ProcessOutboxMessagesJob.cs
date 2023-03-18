using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using NMCK3.Domain.Primitives;
using NMCK3.Infrastructure.Persistence;
using NMCK3.Infrastructure.Persistence.Models;
using Quartz;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace NMCK3.Infrastructure.BackgroundJobs
{
    [DisallowConcurrentExecution]
    public class ProcessOutboxMessagesJob : IJob
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IPublisher _publisher;

        public ProcessOutboxMessagesJob(ApplicationDbContext dbContext, IPublisher publisher)
        {
            _dbContext = dbContext;
            _publisher = publisher;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var messages = await _dbContext
                    .Set<OutboxMessage>()
                    .Where(x => x.ProcessedOnUtc == null)
                    .Take(20)
                    .ToListAsync(context.CancellationToken);

                foreach (var outboxMessage in messages)
                {
                    var domainEvent = JsonConvert
                        .DeserializeObject<IDomainEvent>(
                            outboxMessage.Content,
                            new JsonSerializerSettings()
                            {
                                TypeNameHandling = TypeNameHandling.All
                            });

                    if (domainEvent is null)
                        continue;

                    await _publisher.Publish(domainEvent, context.CancellationToken);

                    outboxMessage.ProcessedOnUtc = DateTime.UtcNow;
                }

                await _dbContext.SaveChangesAsync();
            }
            catch (Exception e)
            {
                //Proper handling (Log,...)
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
