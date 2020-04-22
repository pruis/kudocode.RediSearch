using System;
using System.Net;
using System.Net.Http;
using System.Net.Sockets;
using KudoCode.LogicLayer.Infrastructure.Dtos.Tokens;
using KudoCode.Web.Api;
using KudoCode.Web.Api.Connector;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;

namespace KudoCode.Test.Acceptance
{
    public abstract class InMemoryTest
    {
        private TestServer _server;
        private HttpClient _client;
        protected Connector Connector;
        private string _baseUrl;
        private int _port;

        private Uri Uri { get; set; }

        protected InMemoryTest Start()
        {
            _port = FreeTcpPort();
            Uri = new Uri($"http://localhost:{_port}/api/");
            _baseUrl = Uri.ToString();

            _server = new TestServer(new WebHostBuilder()
                .UseStartup<Startup>().UseUrls(_baseUrl));
            _client = _server.CreateClient();
            _client.BaseAddress = Uri;
            Connector = new Connector(_baseUrl);
            Connector.ConnectorClient.HttpClient = _client;
            return this;
        }

        protected Connector GetNewConnector()
        {
            var client = _server.CreateClient();
            client.BaseAddress = Uri;
            var connector = new Connector(_baseUrl) {ConnectorClient = {HttpClient = client}};
            return connector;
        }

        public InMemoryTest Authenticate()
        {
            var a = Connector.Authentication.GetToken(new GetTokenRequest() {Email = "testB@testC.com", Password = "1234"});
            return this;
        }

        private static int FreeTcpPort()
        {
            var l = new TcpListener(IPAddress.Loopback, 0);
            l.Start();
            var port = ((IPEndPoint) l.LocalEndpoint).Port;
            return port;
        }
    }
}