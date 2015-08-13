using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace SLouple.MVC.Shared
{
    public class Lang
    {
        private int? langID;
        private string langName;
        private string langCode;
        private TaskCompletionSource<bool> taskAddKeyword = new TaskCompletionSource<bool>();
        private Dictionary<string, string> langDic = new Dictionary<string, string>(); //Best property name 2014

        public Lang(int langID)
        {
            this.langID = langID;
        }

        public Lang(string langName)
        {
            this.langName = langName;
        }

        public int GetLangID(){
            if (langID == null)
            {
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                langID = sqlSP.LangGetLangID(langName);
            }
            return langID.Value;
        }

        public string GetLangName()
        {
            if (langName == null)
            {
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                langName = sqlSP.LangGetLangName (langID.Value);
            }
            return langName;
        }

        public string GetLangCode()
        {
            if (langCode == null)
            {
                SqlStoredProcedures sqlSP = new SqlStoredProcedures();
                this.langCode = sqlSP.LangGetLangCode(GetLangID());
            }
            return langCode;
        }

        public void GetTranslationFromSQL()
        {
            string[] keywords = langDic.Keys.ToArray();
            string keywordString = null;
            foreach (string keyword in keywords)
            {
                keyword.Replace("|", "\\|");
                if (keywordString == null)
                {
                    keywordString = keyword;
                }
                else
                {
                    keywordString = keywordString + "|" + keyword;
                }
            }
            SqlStoredProcedures sqlSP = new SqlStoredProcedures();
            string contextString = sqlSP.LangGetTranslation(GetLangID(), "SL_MVC", keywordString);
            contextString = contextString.Replace("\\|", "\\"); //TODO: Add FULL support for \| escape
            string[] context = contextString.Split('|');
            for (int i = 0; i < keywords.Length; i++)
            {
                langDic[keywords[i]] = context[i];
            }
            taskAddKeyword.SetResult(true);
        }

        public void AddKeyword(string keyword)
        {
            if (langDic.ContainsKey(keyword))
            {
                return;
            }
            langDic.Add(keyword, null);
        }

        public string GetTrans(string keyword)
        {
            if (langDic.ContainsKey(keyword))
            {
                if (langDic[keyword] == "(NULL)" || langDic[keyword] == "{NULL}")
                {
                    return "Translation not found";
                }
                return (langDic[keyword]);
            }
            else
            {
                return "Keyword not found";
            }
        }

        public HtmlString GetHTMLTrans(string keyword)
        {
            return new HtmlString(GetTrans(keyword));
        }

        public async Task<string> GetTransAsync(string keyword)
        {
            langDic.Add(keyword, null);
            await taskAddKeyword.Task;
            return langDic[keyword];
        }

        public void Clear()
        {
            langDic.Clear();
            taskAddKeyword = new TaskCompletionSource<bool>();
        }

        public static string GetLangNameNative(string langName)
        {
            SqlStoredProcedures sqlSP = new SqlStoredProcedures();
            return sqlSP.LangGetLangNameNative(langName);
        }

        public static Dictionary<string, string> GetLangListNative()
        {
            SqlStoredProcedures sqlSP = new SqlStoredProcedures();
            return sqlSP.LangSelectLangListNative();
        }

        public void AddStores()
        {
            char[] stores = { 'N','S','X','Y','Z'};
            foreach(char store in stores){
                this.AddKeyword("Store.Name." + store);
            }
        }

        public void AddDayOfWeek()
        {
            foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)).OfType<DayOfWeek>().ToList())
            {
                this.AddKeyword("Date." + day);
            }
        }

    }
}
