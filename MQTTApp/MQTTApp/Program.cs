using System;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ApiLimiteAgua.Model;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Client.Options;
using MQTTnet.Client.Subscribing;

class Program
{
    static async Task Main(string[] args)
    {
        await RunMqttClientAsync();
    }

    static async Task RunMqttClientAsync()
    {
        var factory = new MqttFactory();
        var mqttClient = factory.CreateMqttClient();

        var options = new MqttClientOptionsBuilder()
            .WithClientId("PublisherClientId")
            .WithTcpServer("localhost", 1883) // Replace with your MQTT broker address
            .Build();

        mqttClient.UseConnectedHandler(async e =>
        {
            Console.WriteLine("Connected to the MQTT broker.");

            // Subscribe to a topic when connected
            var subscribeOptions = new MqttClientSubscribeOptionsBuilder()
                .WithTopicFilter("sapucai") // Replace with the topic you want to subscribe to
                .Build();

            var subscribeOptions2 = new MqttClientSubscribeOptionsBuilder()
                .WithTopicFilter("sfrancisco") // Replace with the topic you want to subscribe to
                .Build();

            var cancellationToken = new CancellationToken();

            await mqttClient.SubscribeAsync(subscribeOptions, cancellationToken);
            await mqttClient.SubscribeAsync(subscribeOptions2, cancellationToken);

            Console.WriteLine("Inscrito nos tópicos: \"sfrancisco\" e \"sapucai\"");
        });

        DadosLimiteAguaModel dados = new DadosLimiteAguaModel();

        mqttClient.UseApplicationMessageReceivedHandler(async e =>
        {
            Console.WriteLine($"Received message on topic {e.ApplicationMessage.Topic}: {Encoding.UTF8.GetString(e.ApplicationMessage.Payload)}");
            dados.altura = Double.Parse(Encoding.UTF8.GetString(e.ApplicationMessage.Payload));

            if(e.ApplicationMessage.Topic == "sapucai")
            {
                dados.local = "Rio Sapucaí";
            }
            else if(e.ApplicationMessage.Topic == "sfrancisco")
            {
                dados.local = "Rio São Francisco";
            }

            dados.dataColeta = DateTime.Now;

            string apiUrl = "https://localhost:7297/ApiLimiteAgua/PostInfoLimiteAgua";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Converte o modelo em HttpContent
                    HttpContent content = new ObjectContent<DadosLimiteAguaModel>(dados, new System.Net.Http.Formatting.JsonMediaTypeFormatter());

                    // Configuração do cabeçalho Content-Type
                    content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    // Realiza a chamada HTTP GET
                    HttpResponseMessage response = await client.PostAsync(apiUrl, content);

                    // Verifica se a chamada foi bem-sucedida (status code 200-299)
                    if (response.IsSuccessStatusCode)
                    {
                        // Lê o conteúdo da resposta como uma string
                        string responseData = await response.Content.ReadAsStringAsync();

                        // Processa os dados recebidos conforme necessário
                        Console.WriteLine("Resposta da API:");
                        Console.WriteLine(responseData);
                    }
                    else
                    {
                        Console.WriteLine($"Erro na chamada API. Status Code: {response.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Erro: {ex.Message}");
                }
            }
        });


        await mqttClient.ConnectAsync(options, CancellationToken.None);

        // Keep the program running to receive messages
        while (true)
        {
            await Task.Delay(1000);
        }
    }
}
