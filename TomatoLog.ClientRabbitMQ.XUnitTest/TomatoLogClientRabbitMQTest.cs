using System;
using System.Threading.Tasks;
using TomatoLog.Client.RabbitMQ;
using TomatoLog.Client.Extensions;
using TomatoLog.Common.Config;
using TomatoLog.Common.Interface;
using Xunit;

namespace TomatoLog.ClientRabbitMQ.XUnitTest
{
    public class TomatoLogClientRabbitMQTest
    {
        ITomatoLogClient client;
        public TomatoLogClientRabbitMQTest()
        {
            EventRabbitMQOptions options = new EventRabbitMQOptions
            {
                Logger = null,
                LogLevel = Microsoft.Extensions.Logging.LogLevel.Information,
                ProjectLabel = "20272",
                ProjectName = "TomatoLog-Server",
                SysOptions = new EventSysOptions
                {
                    EventId = true,
                    IP = true,
                    IPList = true,
                    MachineName = true,
                    ProcessId = true,
                    ProcessName = true,
                    ThreadId = true,
                    Timestamp = true,
                    UserName = true
                },
                Tags = null,
                Version = "1.0.1",
                Exchange = "TomatoLog-Exchange",
                ExchangeType = "direct",
                Host = "172.16.1.219",
                Password = "123456",
                Port = 5672,
                QueueName = "TomatoLog-Queue",
                RouteKey = "All",
                UserName = "lgx",
                vHost = "TomatoLog"
            };
            client = new TomatoLogClientRabbitMQ(options);
        }

        [Fact]
        public async Task WriteLogAsync()
        {
            for (int i = 0; i < 10; i++)
            {
                //await client.WriteLogAsync(LogLevel.Debug, "ES Exception ��һ�β�����Ϣ", null, null);
                //await client.WriteLogAsync(LogLevel.Information, "ES Exception ��һ�β�����Ϣ", null, null);
                //await client.WriteLogAsync(LogLevel.Critical, "ES Exception ��һ�β�����Ϣ", null, null);
                //await client.WriteLogAsync(LogLevel.None, "ES Exception ��һ�β�����Ϣ", null, null);
                //await client.WriteLogAsync(LogLevel.Trace, "ES Exception ��һ�β�����Ϣ", null, null);
                //await client.WriteLogAsync(LogLevel.Warning, "ES Exception ��һ�β�����Ϣ", null, null);
                // await client.WriteLogAsync(LogLevel.Error, "ES Exception ��һ�β�����Ϣ", null, null);
                try
                {
                    throw new Exception("RabbitMQ�����쳣");
                }
                catch (Exception ex)
                {
                    await ex.AddTomatoLogAsync(1320);
                }
            }
        }
    }
}