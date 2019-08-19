using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Net;
using System.IO;
using System.Xml.Serialization;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Newtonsoft.Json.Converters;
using System.Collections.Generic;
using System.Text;

namespace PBIBling
{
    //public class Comunicacao<T1, T2>
    //{

    //    public String SerializeObject(T1 pObject)
    //    {

    //        try
    //        {
    //            String XmlizedString = null;
    //            MemoryStream memoryStream = new MemoryStream();
    //            XmlSerializer xs = new XmlSerializer(pObject.GetType());
    //            System.Xml.XmlTextWriter xmlTextWriter = new System.Xml.XmlTextWriter(memoryStream, System.Text.Encoding.GetEncoding("ISO-8859-1"));
    //            xs.Serialize(xmlTextWriter, pObject);
    //            memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
    //            XmlizedString = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(memoryStream.ToArray());
    //            return XmlizedString;
    //        }
    //        catch (Exception e) {
    //            return null; 
    //        }
    //    }

    //    public T2 Deserialize(StreamReader stream,ref string mensagemretorno)
    //    {
    //        try
    //        {
    //            string xml = stream.ReadToEnd();

    //            System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
    //            doc.LoadXml(xml);
    //            System.Xml.XmlNodeList listanos = doc.GetElementsByTagName("erro");
    //            doc = null;
    //            if (listanos.Count <= 0)
    //            {
    //                StringReader stringReader;
    //                stringReader = new StringReader(xml);
    //                System.Xml.XmlTextReader xmlReader;
    //                xmlReader = new System.Xml.XmlTextReader(stringReader);

    //                XmlSerializer serializer = new XmlSerializer(typeof(T2));
    //                T2 result = (T2)serializer.Deserialize(xmlReader);
    //                xmlReader.Close();
    //                stringReader.Close();
    //                return result;
    //            }
    //            else
    //                mensagemretorno = xml;

    //            return default(T2);
    //        }
    //        catch (Exception e)
    //        {
    //            return default(T2);
    //        }
    //    }
    //}

    public static class Comunicacao
    {
        public static String SerializeObject<T>(T pObject)
        {
            try
            {
                String XmlizedString = null;
                MemoryStream memoryStream = new MemoryStream();
                XmlSerializer xs = new XmlSerializer(pObject.GetType());
                System.Xml.XmlTextWriter xmlTextWriter = new System.Xml.XmlTextWriter(memoryStream, System.Text.Encoding.GetEncoding("ISO-8859-1"));
                xs.Serialize(xmlTextWriter, pObject);
                memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
                XmlizedString = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(memoryStream.ToArray());
                return XmlizedString;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public static T RequestContent<T>(string url, Dictionary<string, string> parameters, string method = "GET", string accept = null)
        {
            List<string> varPost = new List<string>();
            List<string> varPostBody = new List<string>();
            string postString = string.Empty;
            string postBody = string.Empty;

            foreach (var par in parameters)
            {
                if (varPost.Count == 0)
                    varPost.Add(string.Format("?{0}={1}", par.Key, par.Value));
                else
                    varPost.Add(string.Format("&{0}={1}", par.Key, par.Value));
            }
            varPost.ForEach(x => postString += x);

            foreach (var par in parameters)
            {
                if (varPostBody.Count == 0)
                    varPostBody.Add(string.Format("{0}={1}", par.Key, par.Value));
                else
                    varPostBody.Add(string.Format("&{0}={1}", par.Key, par.Value));
            }
            varPostBody.ForEach(x => postBody += x);

            if (method.Equals("GET"))
            {
                url += postString;
            }
            Console.WriteLine("URL chamada: " + url);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = method;
            if (accept != null)
                req.Accept = accept;

            if (!method.Equals("GET"))
            {
                if (accept.Equals("application/json"))
                    req.ContentType = accept;
                else
                    req.ContentType = "application/x-www-form-urlencoded";

                StreamWriter requestWriter = new StreamWriter(req.GetRequestStream());
                requestWriter.Write(postBody);
                requestWriter.Close();
            }

            T result = default(T);
            var response = req.GetResponse();
            StreamReader responseReader = new StreamReader(response.GetResponseStream());
            try
            {
                if (response.ContentType.Contains("application/json"))
                {
                    var json = responseReader.ReadToEnd();
                    result = JsonConvert.DeserializeObject<T>(json, new MyDateTimeConverter(), new Util.DecimalConverter()); // Deserialize<T>(responseReader);
                }
                else
                    result = Deserialize<T>(responseReader);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro transformar o response no objeto em questão.");
                Console.WriteLine("Erro: " + ex.Message);
                Console.WriteLine("Stack: " + ex.StackTrace);
                Console.WriteLine("Dados POST: " + postString);
            }

            responseReader.Close();
            //req.GetResponse().Close();
            if (result == null)
            {
                return default(T);
            }
            else
            {
                return result;
            }
        }

        public static T RequestContent<T>(string url, Dictionary<string, string> parameters, string key, string token, string method = "GET", string accept = null, string authenticationType = "basic")
        {
            List<string> varPost = new List<string>();
            List<string> varPostBody = new List<string>();
            string postString = string.Empty;
            string postBody = string.Empty;

            foreach (var par in parameters)
            {
                if (varPost.Count == 0)
                    varPost.Add(string.Format("?{0}={1}", par.Key, par.Value));
                else
                    varPost.Add(string.Format("&{0}={1}", par.Key, par.Value));
            }
            varPost.ForEach(x => postString += x);

            foreach (var par in parameters)
            {
                if (varPostBody.Count == 0)
                    varPostBody.Add(string.Format("{0}={1}", par.Key, par.Value));
                else
                    varPostBody.Add(string.Format("&{0}={1}", par.Key, par.Value));
            }
            varPostBody.ForEach(x => postBody += x);

            if (method.Equals("GET"))
            {
                url += postString;
            }
            Console.WriteLine("URL chamada: " + url);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = method;
            if (authenticationType.Equals("basic"))
                req.Headers["Authorization"] = authenticationType + " " + Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes(String.Format("{0}:{1}", key, token)));
            else
            {
                req.Headers["Authorization"] = authenticationType + " " + token;
            }

            if (accept != null)
                req.Accept = accept;

            if (!method.Equals("GET"))
            {
                if (accept.Equals("application/json"))
                    req.ContentType = accept;
                else
                    req.ContentType = "application/x-www-form-urlencoded";

                StreamWriter requestWriter = new StreamWriter(req.GetRequestStream());
                requestWriter.Write(postBody);
                requestWriter.Close();
            }

            T result = default(T);
            var response = req.GetResponse();
            StreamReader responseReader = new StreamReader(response.GetResponseStream());
            try
            {
                if (response.ContentType.Contains("application/json"))
                {
                    var json = responseReader.ReadToEnd();
                    result = JsonConvert.DeserializeObject<T>(json, new MyDateTimeConverter(), new Util.DecimalConverter()); // Deserialize<T>(responseReader);
                }
                else
                    result = Deserialize<T>(responseReader);
            }
            catch (Exception ex)
            {
                Console.WriteLine("URL: " + url);
                Console.WriteLine("Dados GET: " + varPost);
                throw ex;
            }

            responseReader.Close();
            req.GetResponse().Close();
            if (result == null)
            {
                return default(T);
            }
            else
            {
                return result;
            }
        }

        public static bool RequestPutJsonContent<K>(string url, K parameters, string key, string token, string method = "PUT", string authenticationType = "basic")
        {
            string postString = string.Empty;

            postString = Newtonsoft.Json.JsonConvert.SerializeObject(parameters);

            var bytes = Encoding.ASCII.GetBytes(postString);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = method;
            request.ContentType = "application/json";
            //request.Headers["Authorization"] = "basic " + Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes(String.Format("{0}:{1}", key, token)));

            if (authenticationType.Equals("basic"))
                request.Headers["Authorization"] = authenticationType + " " + Convert.ToBase64String(Encoding.GetEncoding("UTF-8").GetBytes(String.Format("{0}:{1}", key, token)));
            else
            {
                request.Headers["Authorization"] = authenticationType + " " + token;
            }

            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Dispose();
                requestStream.Flush();
                requestStream.Close();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                response.Close();
                return true;
            }

            return false;
        }

        public static T RequestContent<T>(string url, Dictionary<string, string> parameters, string token, string method = "GET", string accept = null)
        {
            List<string> varPost = new List<string>();
            string postString = string.Empty;

            foreach (var par in parameters)
            {
                if (varPost.Count == 0)
                    varPost.Add(string.Format("?{0}={1}", par.Key, par.Value));
                else
                    varPost.Add(string.Format("&{0}={1}", par.Key, par.Value));
            }
            varPost.ForEach(x => postString += x);

            if (method.Equals("GET"))
            {
                url += postString;
            }
            Console.WriteLine("URL chamada: " + url);
            HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
            req.Method = method;
            req.Headers["Authorization"] = "basic " + token;

            if (accept != null)
                req.Accept = accept;

            if (!method.Equals("GET"))
            {
                if (accept.Equals("application/json"))
                    req.ContentType = accept;
                else
                    req.ContentType = "application/x-www-form-urlencoded";

                StreamWriter requestWriter = new StreamWriter(req.GetRequestStream());
                requestWriter.Write(postString);
                requestWriter.Close();
            }

            T result = default(T);
            var response = req.GetResponse();
            StreamReader responseReader = new StreamReader(response.GetResponseStream());
            try
            {
                if (response.ContentType.Contains("application/json"))
                {
                    var json = responseReader.ReadToEnd();
                    result = JsonConvert.DeserializeObject<T>(json, new MyDateTimeConverter(), new Util.DecimalConverter()); // Deserialize<T>(responseReader);
                }
                else
                    result = Deserialize<T>(responseReader);
            }
            catch (Exception ex)
            {
                Console.WriteLine("URL: " + url);
                Console.WriteLine("Dados GET: " + varPost);
                throw ex;
            }

            responseReader.Close();
            req.GetResponse().Close();
            if (result == null)
            {
                return default(T);
            }
            else
            {
                return result;
            }
        }

        public static bool RequestPutJsonContent<K>(string url, K parameters, string token)
        {
            string postString = string.Empty;

            postString = Newtonsoft.Json.JsonConvert.SerializeObject(parameters);

            var bytes = Encoding.ASCII.GetBytes(postString);

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "PUT";
            request.ContentType = "application/json";
            request.Headers["Authorization"] = "basic " + token;

            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
                requestStream.Dispose();
                requestStream.Flush();
                requestStream.Close();
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            if (response.StatusCode == HttpStatusCode.OK)
            {
                response.Close();
                return true;
            }

            return false;
        }

        public static T RequestPostJsonContent<T, K>(string url, K parameters)
        {
            List<string> varPost = new List<string>();
            string postString = string.Empty;

            postString = Newtonsoft.Json.JsonConvert.SerializeObject(parameters);
            Console.WriteLine(postString);
            varPost.ForEach(x => postString += x);
            try
            {
                HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);
                req.Method = "POST";

                req.ContentType = "application/json";
                StreamWriter requestWriter = new StreamWriter(req.GetRequestStream());
                requestWriter.Write(postString);
                requestWriter.Close();

                StreamReader responseReader = new StreamReader(req.GetResponse().GetResponseStream());

                string resposta = responseReader.ReadToEnd();
                Console.WriteLine("Resposta: " + resposta);
                T result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(resposta, new MyDateTimeConverter(), new PBIBling.Util.DecimalConverter());
                responseReader.Close();
                req.GetResponse().Close();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("URL: " + url);
                Console.WriteLine("Dados Post: " + postString);
                Console.WriteLine("Exception: " + ex.Message);
                throw ex;
            }
        }

        public static T Deserialize<T>(StreamReader stream)
        {
            try
            {
                string xml = stream.ReadToEnd();
                //JsonSerializerSettings serializerSettings = new JsonSerializerSettings();
                //serializerSettings.Converters.Add(new IsoDateTimeConverter());
                //T root = JsonConvert.DeserializeObject<T>(xml,serializerSettings);

                System.Xml.XmlDocument doc = new System.Xml.XmlDocument();
                doc.LoadXml(xml);
                System.Xml.XmlNodeList listanos = doc.GetElementsByTagName("erro");
                doc = null;
                if (listanos.Count <= 0)
                {
                    StringReader stringReader;
                    stringReader = new StringReader(xml);
                    System.Xml.XmlTextReader xmlReader;
                    xmlReader = new System.Xml.XmlTextReader(stringReader);

                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    T result = (T)serializer.Deserialize(xmlReader);
                    xmlReader.Close();
                    stringReader.Close();
                    return result;
                }
                else
                    throw new Exception("XML invalido");

                return default(T);
            }
            catch (Exception e)
            {
                return default(T);
            }
        }

        /// <summary>
        /// Convert Serialization Time /Date(1319266795390+0800) as String
        /// </summary>
        private static string ConvertJsonDateToDateString(Match m)
        {
            string result = string.Empty;
            DateTime dt = new DateTime(1970, 1, 1);
            dt = dt.AddMilliseconds(long.Parse(m.Groups[1].Value));
            dt = dt.ToLocalTime();
            result = dt.ToString("yyyy-MM-dd HH:mm:ss");
            return result;
        }

        /// <summary>
        /// Convert Date String as Json Time
        /// </summary>
        private static string ConvertDateStringToJsonDate(Match m)
        {
            string result = string.Empty;
            DateTime dt = DateTime.Parse(m.Groups[0].Value);
            dt = dt.ToUniversalTime();
            TimeSpan ts = dt - DateTime.Parse("1970-01-01");
            result = string.Format("\\/Date({0}+0800)\\/", ts.TotalMilliseconds);
            return result;
        }
    }

    public class MyDateTimeConverter : Newtonsoft.Json.JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTime);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value != null && reader.Value.ToString().IndexOf("-").Equals(-1))
            {
                var t = long.Parse(reader.Value.ToString());
                return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddMilliseconds(t);
            }
            else
                return Convert.ToDateTime(reader.Value);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
