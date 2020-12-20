using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapsApp
{
    class PolygonCoordinates
    {
        // переменная для получения данных
        string line;
        Match match;
        MatchCollection matches;
        // список с координатами
        List<string> coord = new List<string>();
        public List<string> GetCoordinates(string adress, int dotFrequency)
        {
            try
            {
                GisService gisOsm = new GisService();
                line = gisOsm.Connect($"https://nominatim.openstreetmap.org/search.php?q={adress}&polygon_geojson=1&format=jsonv2");
                match = Regex.Match(line, @"{""type"":""MultiPolygon"",""coordinates"":(.*?)}", RegexOptions.Singleline);
                if (String.IsNullOrEmpty(match.Value))
                {
                    match = Regex.Match(line, @"{""type"":""Polygon"",""coordinates"":(.*?)}", RegexOptions.Singleline);
                }
                line = match.Groups[1].Value;
                // шаблон для отсеивания нужных данных
                Regex regex = new Regex(@"([\[]{1}[^\[](.*?)([\]]){1})");
                matches = regex.Matches(line);
                // заполнение списка координатами точек
                coord.Clear();
                foreach (Match m in matches)
                {
                    coord.Add(m.Value);
                }
                // удаление определенных координат
                for (int i = 0; i < coord.Count; i+= dotFrequency)
                {
                    coord.RemoveAt(i);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            return coord;
        }
    }
}
