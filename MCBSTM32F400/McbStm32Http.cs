﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Xml.Linq;

namespace MCBSTM32F400
{
    static public class McbStm32Http
    {
        static public McbStm32Environment RequestEnvironmentalData()
        {
            string uri = "http://mcbstm32f400/";
            string username = "admin";
            string password = "";

            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(uri);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic",
                Convert.ToBase64String(Encoding.UTF8.GetBytes($"{username}:{password}")));

            HttpResponseMessage response = client.GetAsync("environment.cgx").Result;
            response.EnsureSuccessStatusCode();

            Dictionary<string, string> data = ParseXml(response.Content.ReadAsStringAsync().Result);
            return new McbStm32Environment()
            {
                Temperature = Convert.ToDouble(data["amb_degc"]),
                Humidity = Convert.ToDouble(data["amb_percent"]),
                CoreTemperature = Convert.ToDouble(data["core_degc"]),
                DeviceTime = Convert.ToDateTime(data["dev_time"]),
                AcquisitionTime = DateTime.Now
            };
        }

        static public Dictionary<string, string> ParseXml(string xml)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            XDocument doc = XDocument.Parse(xml);

            List<string> ids = (from e in doc.Descendants("id") select e.Value).ToList();
            List<string> values = (from e in doc.Descendants("value") select e.Value).ToList();

            for(int i = 0; i < ids.Count; ++i)
            {
                result.Add(ids[i], values[i]);
            }

            return result;
        }
    }
}
