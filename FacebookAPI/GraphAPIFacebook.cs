using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace FacebookAPI
{
    public class GraphAPIFacebook
    {
        private string accesstoken;
        private string cookie;
        private String fb_dtsg = "";
        private String id;
        private static string HOST_FACEBOOK = "https://www.facebook.com/api/graphql/";
        private static string URL_GET_COOKIE = "https://m.facebook.com/composer/ocelot/async_loader/?publisher=feed";

        public string Accesstoken
        {
            get
            {
                return accesstoken;
            }

            set
            {
                accesstoken = value;
            }
        }
        public string Cookie
        {
            get
            {
                return cookie;
            }

            set
            {
                cookie = value;
            }
        }
        public string Fb_dtsg
        {
            get
            {
                return fb_dtsg;
            }

            set
            {
                fb_dtsg = value;
            }
        }
        public string Id { get => id; set => id = value; }

        public GraphAPIFacebook()
        {

        }
        public GraphAPIFacebook(String token)
        {
            this.accesstoken = token;
        }
        public GraphAPIFacebook(String cookie, String fb_dtsg)
        {
            this.cookie = cookie;
            this.fb_dtsg = fb_dtsg;
        }
        public static GraphAPIFacebook Build(String cookie, String user_agent)
        {
            try
            {
                String dataRequest = API.GET(URL_GET_COOKIE, cookie, user_agent);
                GraphAPIFacebook graphAPIFacebook = new GraphAPIFacebook();
                graphAPIFacebook.accesstoken = GetTokenFromContent(dataRequest);
                graphAPIFacebook.fb_dtsg = GetFbDtsgFromContent(dataRequest);
                graphAPIFacebook.Id = GetIDFromCookie(cookie);
                return graphAPIFacebook;
            }
            catch (Exception exx)
            {
                throw exx;
            }
        }
        public static String GetTokenFromContent(String dataAccessToken)
        {
            if (dataAccessToken.Contains("EAAA"))
            {
                String endString = "\\\",\\\"useLocalFilePreview\\\"";
                int start = dataAccessToken.IndexOf("EAAA");
                int end = dataAccessToken.IndexOf(endString);
                String t = dataAccessToken.Substring(start, end - start);
                return t;
            }
            else
            {
                throw new Exception("Cookie có thể đã hết hạn vui lòng thử lại");
            }
        }
        public static String GetFbDtsgFromContent(String dataAccessToken)
        {
            if (dataAccessToken.Contains("EAAA"))
            {
                string pattern = @"name=""fb_dtsg"" value=""[a-zA-Z0-9:]+""";
                string patGetFBDTSG = @"""[a-zA-Z0-9:]+""";
                RegexOptions options = RegexOptions.Multiline;
                String fb = Regex.Matches(dataAccessToken, pattern, options)[0].Value;
                String fb_dtsg = Regex.Match(fb, patGetFBDTSG, options).Value;
                return fb_dtsg.Replace('"', ' ').Trim();
            }
            else
            {
                throw new Exception("Cookie có thể đã hết hạn vui lòng thử lại");
            }
        }
        public String GetUrl(string para)
        {
            return HOST_FACEBOOK + para + "&access_token=" + Accesstoken;
        }
        public static String GetIDFromCookie(string cookie)
        {
            var temp = cookie.Split(';');
            foreach (var item in temp)
            {
                var temp2 = item.Trim().Split('=');
                if (temp2.Length > 1)
                {
                    if (temp2[0].Trim().Equals("c_user"))
                        return temp2[1];

                }
            }
            return "";
        }
        public String RequestGet(String para, String user_agent)
        {
            try
            {
                return API.GET(GetUrl(para), this.cookie, user_agent);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public String RequestPost(String para, String data,String content_type ,String user_agent = "")
        {
            try
            {
                String url = HOST_FACEBOOK;
                if (String.IsNullOrEmpty(para))
                    url = GetUrl(para);
                String json = API.POST(url, data, content_type, cookie);
                return json;
            }catch(Exception ex)
            {
                throw ex;
            }
            return "";
        }
    }
}
