using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace OG_MFTG.DataLayer
{

    public class CreateLogFile
    {
        private StringBuilder _logFormat = new StringBuilder();
        private StringBuilder _errorTime = new StringBuilder();

        public CreateLogFile()
        {
            StringBuilder year = new StringBuilder();
            StringBuilder month = new StringBuilder();
            StringBuilder day = new StringBuilder();

            year.Append(DateTime.Now.Year.ToString());
            month.Append(DateTime.Now.Month.ToString());
            day.Append(DateTime.Now.Day.ToString());

            _logFormat.Append(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString() + " ==> ");
            _errorTime.Append(year + month.ToString() + day);
        }

        public async Task ErrorLog(string errMsg, string name)
        {
            StringBuilder directory = new StringBuilder();
            StringBuilder fileName = new StringBuilder();
            StringBuilder path = new StringBuilder();

            directory.Append(HttpContext.Current.Server.MapPath("Logs/ErroLog"));
            fileName.Append($"{DateTime.Now:yyyy-MM-dd}__{name}.txt");
            path.Append(Path.Combine(directory.ToString(), fileName.ToString()));

            using (var sw = new StreamWriter(path.ToString(), true))
            {
                await sw.WriteLineAsync(_logFormat + errMsg);
                await sw.FlushAsync();
                sw.Close();
            }
        }
    }
}
