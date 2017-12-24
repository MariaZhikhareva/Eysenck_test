using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Eysenck
{
    class GetQuestions
    {
        List<string> questions;
        public List<string> getQuestions()
        {
            List<string> outList = new List<string>();
            string allText;
            try
            {
                using (StreamReader sr = new StreamReader(Eysenck.Properties.Settings.Default.FileName, Encoding.GetEncoding(1251)))
                {
                    allText = sr.ReadToEnd();
                }
            }
            catch
            {
                //REVIEW: И всё? Пользователю сообщать об ошибке не надо? В лог не пишем?
                allText = string.Empty;
            }
            string[] split = allText.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var item in split)
            {
                outList.Add(item);
            }
            return outList;
        }
    }
}
