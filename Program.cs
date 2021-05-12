using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;

namespace consume_api {
    class Program {
        static void Main (string[] args) {
            Console.WriteLine ("Digite o C.E.P.");
            string cep = Console.ReadLine ();

            string uri = "https://viacep.com.br/ws/";
            HttpClient client = new HttpClient ();
            client.BaseAddress = new Uri (uri);
            client.DefaultRequestHeaders
                .Accept.Add (new MediaTypeWithQualityHeaderValue ("application/json"));
            HttpResponseMessage response = client.GetAsync (cep + "/json/").Result;
            if (response.StatusCode != HttpStatusCode.OK) {
                Console.WriteLine ("Endereço não encontrado");
                return;
            }
            Stream stream = response.Content.ReadAsStream ();
            string jsonString = new StreamReader (stream).ReadToEnd ();
            Address address = JsonSerializer.Deserialize<Address> (jsonString);
            Console.WriteLine (address);
        }
    }

    class Address {

        public string cep { set; get; }
        public string logradouro { set; get; }
        public string complemento { set; get; }
        public string bairro { set; get; }
        public string localidade { set; get; }
        public string uf { set; get; }
        public string ibge { set; get; }
        public string gia { set; get; }
        public string ddd { set; get; }
        public string siafi { set; get; }

        public override string ToString () {
            return $"C.E.P.: {cep}\n" +
                $"Rua: {logradouro}\n" +
                $"Complemento: {complemento}\n" +
                $"Bairro: {bairro}\n" +
                $"Localidade: {localidade}\n" +
                $"U.F.: {uf}\n" +
                $"IBGE: {ibge}\n";
        }

    }
}
