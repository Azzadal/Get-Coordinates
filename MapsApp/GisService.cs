using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MapsApp
{
    class GisService
    {
        string line;
        public string Connect(string adress)
        {
            ServicePointManager.SecurityProtocol = 
                SecurityProtocolType.Tls12 | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls;
            // подключение к сервису
            using (WebClient wc = new WebClient())
            {
                try
                {
                    wc.Headers.Add("User-Agent: Other");
                    line = wc.DownloadString($"{adress}");
                    if (String.IsNullOrEmpty(line))
                    {
                        MessageBox.Show("Пустая строка");
                    }
                }
                catch (WebException ex)
                {
                    WebExceptionStatus status = ex.Status;
                    if (status == WebExceptionStatus.ProtocolError)
                    {
                        HttpWebResponse httpResponse = (HttpWebResponse)ex.Response;
                        MessageBox.Show("Статусный код ошибки:" + 
                            (int)httpResponse.StatusCode + " - " + httpResponse.StatusCode);
                    }
                }
            }
            return line;
        }
    }
}
