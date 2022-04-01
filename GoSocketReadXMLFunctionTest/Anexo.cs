using System.Linq;
using System.Xml.Serialization;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace GoSocketReadXMLFunctionTest
{
    public class Anexo
    {
        [FunctionName("CronAnexo")]
        public static void Run([TimerTrigger("0 */30 * * * *")] TimerInfo myTimer, ILogger log)
        {
            gosocket goSocketXML = DeserializeXML();
            log.LogInformation($"Cantidad de nodos tipo área {goSocketXML.area.Length}");
            int employeesNumTwoOrMore = goSocketXML.area.Where(x => x.employees.Length > 2).Count();
            log.LogInformation($"Cantidad de nodos tipo área con más de 2 empleados {employeesNumTwoOrMore}");
            log.LogInformation($"Nombre de Área y Total Salarios");
            foreach (gosocketArea area in goSocketXML.area)
            {
                log.LogInformation($"({area.name}) | ({area.employees.Sum(x => x.salary)})");
            }
        }

        private static gosocket DeserializeXML()
        {
            string filePath = @"Resources\GoSocket.xml";
            XmlSerializer xmlSerializer = new(typeof(gosocket));
            using System.IO.StreamReader sr = new(filePath);
            return (gosocket)xmlSerializer.Deserialize(sr);
        }
    }
}
