
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.IO;
using System.Net;
using System.Net.Http;

namespace BoredAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Are you feeling bored? Want to make new adventures and discover new thigs to do? Heres one example on one activity you could make to change your habits. :D ");
            Console.WriteLine("\nA good activity you can do is to " + Atividade());
            Console.WriteLine("It is a "+ Tipo() + "Activity type.");
            Console.WriteLine("It can hold " +Participantes() + " or more participants");
            Console.WriteLine(Link());
            Console.WriteLine(Acessibilidade());

            
        }
        
        

        public static string ObterWebRequest(string metodohttp, string url)
        {
            WebRequest webRequest = WebRequest.Create(url);

            webRequest.Method = metodohttp;

            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();

            string StreamReaderResponse = string.Empty;

            using (Stream stream = response.GetResponseStream())
            {
                StreamReader streamReader = new StreamReader(stream);
                StreamReaderResponse = streamReader.ReadToEnd();
            }

            return StreamReaderResponse;
        }

       

        public static string Atividade()
        {
            string url = "http://www.boredapi.com/api/activity/";
            var requestResult = ObterWebRequest("GET", url);
            var resultadoConversao = JsonConvert.DeserializeObject<CoisasParaFazer>(requestResult);

            return resultadoConversao.activity;
        }

        public static string Tipo()
        {
            string url = "http://www.boredapi.com/api/activity/";
            var requestResult = ObterWebRequest("GET", url);
            var resultadoConversao = JsonConvert.DeserializeObject<CoisasParaFazer>(requestResult);

            return resultadoConversao.type;
        }
        public static int Participantes()
        {
            string url = "http://www.boredapi.com/api/activity/";
            var requestResult = ObterWebRequest("GET", url);
            var resultadoConversao = JsonConvert.DeserializeObject<CoisasParaFazer>(requestResult);

            return resultadoConversao.participants;
        }
        
        public static string Link()
        {
            HttpClient cliente = new HttpClient();
            string url = "http://www.boredapi.com/api/activity/";
            var result = cliente.GetAsync(url).Result;
            var resultBody = result.Content.ReadAsStringAsync().Result;
            var conversao = JToken.Parse(resultBody).ToObject<CoisasParaFazer>();
            if (conversao.link == "")
            {
                return "There is no Link to it at this moment.";
            }
            else
            {
                return "The webiste for it is: " + conversao.link;
            }          
        }
             
        public static string Acessibilidade()
        {
            string url = "http://www.boredapi.com/api/activity/";
            var requestResult = ObterWebRequest("GET", url);
            var resultadoConversao = JsonConvert.DeserializeObject<CoisasParaFazer>(requestResult);

            if (resultadoConversao.acessibility == 0)
            {
               
                return "Unfortunately, it is not acessible for everyone.";
            }
            else
            {
                return "It is acessible for everyone, YAY!";
            }


        
        }


    }
}
