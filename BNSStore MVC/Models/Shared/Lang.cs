using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SLouple.MVC.Shared
{
    public class Lang
    {
        private int? langID;
        private string langName;
        private string langCode;
        private Dictionary<string, string> langDic = new Dictionary<string, string>(); //Best property name 2014
        private bool ran = false;

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
                langID = SLSqlSPs.Lang.GetLangID(langName);
            }
            return langID.Value;
        }

        public string GetLangName()
        {
            if (langName == null)
            {
                langName = SLSqlSPs.Lang.GetLangName(langID.Value);
            }
            return langName;
        }

        public string GetLangCode()
        {
            if (langCode == null)
            {
                this.langCode = SLSqlSPs.Lang.GetLangCode(GetLangID());
            }
            return langCode;
        }

        public void RequestTrans()
        {
            if (!ran)
            {
                var keywords = langDic.Keys.ToList();
                var contexts = SLSqlSPs.Lang.SelectTranslation(GetLangID(), keywords);
                langDic = keywords.ToDictionary(x => x, x => contexts[keywords.IndexOf(x)]);
                ran = true;
            }
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
            if (!ran)
            {
                RequestTrans();
            }
            if (langDic.ContainsKey(keyword))
            {
                return (langDic[keyword]);
            }
            else
            {
                return "{Missing Key}";
            }
        }

        public HtmlString GetHTMLTrans(string keyword)
        {
            return new HtmlString(GetTrans(keyword));
        }
    }
}
