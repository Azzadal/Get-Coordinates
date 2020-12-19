using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MapsApp
{
    class PolygonCoordinates
    {
        // переменная для получения данных
        string line = "";
        Match match;
        // список с координатами
        List<string> coord = new List<string>();
        public List<string> GetCoordinates(string adress, int dotFrequency)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            // подключение к сервису
            using (WebClient wc = new WebClient())
            {
                wc.Headers.Add("User-Agent: Other");
                line = wc.DownloadString($"https://nominatim.openstreetmap.org/search?q=135+{adress}&format=xml&polygon_geojson=1&addressdetails=1");
            }
            match = Regex.Match(line, @"{""type"":""Polygon"",""coordinates"":(.*?)}", RegexOptions.Singleline);
            line = match.Groups[1].Value;
            // шаблон для отсеивания нужных данных
            Regex regex = new Regex(@"([\[]{1}[^\[](.*?)([\]]){1})");
            MatchCollection matches = regex.Matches(line);

            // заполнение списка координатами точек
            foreach (Match m in matches)
            {
                coord.Add(m.Value);
            }
            // удаление определенных координат
            for (int i = 0; i < coord.Count; i++)
            {
                if (i % dotFrequency == 0)
                {
                    coord.RemoveAt(i);
                }
            }
            return coord;
        }
    }
}
