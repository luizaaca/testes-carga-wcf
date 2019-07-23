using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading;

namespace TesteCarga
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da classe "Service1" no arquivo de código, svc e configuração ao mesmo tempo.
    // OBSERVAÇÃO: Para iniciar o cliente de teste do WCF para testar esse serviço, selecione Service1.svc ou Service1.svc.cs no Gerenciador de Soluções e inicie a depuração.
    public class Service1 : IService1
    {
        public Service1()
        {
            //ThreadPool.GetMinThreads(out int worker, out int io);
            //ThreadPool.SetMinThreads(4, 128);
            //https://devblogs.microsoft.com/aspnet/lets-try-wcf-self-hosted-services-in-a-container/
        }
        public string GetData(int value)
        {
            Thread.Sleep(TimeSpan.FromSeconds(2));
            return string.Format("You entered: {0}", value);
        }
       
    }
}
