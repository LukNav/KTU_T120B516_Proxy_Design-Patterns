﻿using Microsoft.Net.Http.Headers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace WindowsFormsApplication.Helpers
{
    public static class HttpRequests
    {
        public static HttpResponseMessage GetRequest(string Url)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, Url)
            {
                Headers =
                {
                    { HeaderNames.Accept, "*/*" },
                    { HeaderNames.UserAgent, "Client" }
                }
            };

            using (var httpClient = new HttpClient())
                return httpClient.Send(httpRequestMessage);
        }
        public static HttpResponseMessage DeleteRequest(string Url)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Delete, Url)
            {
                Headers =
                {
                    { HeaderNames.Accept, "*/*" },
                    { HeaderNames.UserAgent, "Client" }
                }
            };

            using (var httpClient = new HttpClient())
                return httpClient.Send(httpRequestMessage);
        }
        public static HttpResponseMessage PostRequest<T>(string Url, T body)
        {
            string content = JsonSerializer.Serialize(body);

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, Url)
            {
                Headers =
                {
                    { HeaderNames.Accept, "*/*" },
                    { HeaderNames.UserAgent, "Yes" }
                },
                Content = new StringContent(content,
                                    Encoding.UTF8,
                                    "application/json")
            };

            using (var httpClient = new HttpClient())
                return httpClient.Send(httpRequestMessage);

            return null;
        }

        public static string Message(this HttpResponseMessage httpResponseMessage)
        {
            return httpResponseMessage.Content.ReadAsStringAsync().Result;
        }


        public static T Deserialize<T>(this HttpResponseMessage httpResponseMessage)
        {
            return JsonSerializer.Deserialize<T>(httpResponseMessage.Message(), new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
        }
    }
}
