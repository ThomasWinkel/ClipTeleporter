using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClipTeleporter
{
    internal class ClipHandler
    {
        public Clip Clip { get; set; }
        private const string Server = "https://clipteleporter.visio-shapes.com/"; //"http://127.0.0.1:5000";

        public async Task<string> SendClip()
        {
            try
            {
                Clip = null;
                DataObject dataObject = (DataObject)Clipboard.GetDataObject();
                string strDataObject = DataObjectToString(dataObject);
                Dictionary<string, string> dict = new Dictionary<string, string>();
                string token = GeneratePassword(10);
                string password = GeneratePassword(10);
                dict["token"] = token;
                dict["data_object"] = StringCipher.Encrypt(strDataObject.Compress(), password);
                string json = JsonConvert.SerializeObject(dict);
                HttpClient httpClient = new HttpClient();
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(Server + "/api/add_clip", content);
                json = await response.Content.ReadAsStringAsync();
                Dictionary<string, string> message = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
                if (message["message"] == "Ok")
                {
                    Clip = new Clip
                    {
                        Date = DateTime.Now,
                        Direction = "Sent",
                        Token = token + "#" + password
                    };
                    Clipboard.SetText(token + "#" + password);
                    return "Successful sending clip to server.\nFind token in your clipboard: " + token + "#" + password;
                }
                else
                {
                    return "Error sending clip to server.";
                }
            }
            catch
            {
                return "Error sending clip to server.";
            }
        }

        public async Task<string> GetClip(string token_password)
        {
            Clip = null;
            if (string.IsNullOrEmpty(token_password)) return "Invalid token.";
            if (!token_password.Contains("#")) return "Invalid token.";
            string token = token_password.Split('#')[0];
            string password = token_password.Split('#')[1];
            HttpClient httpClient = new HttpClient();
            var response = await httpClient.GetAsync(Server + "/api/get_clip/" + token);
            string strDataObject = await response.Content.ReadAsStringAsync();
            if (strDataObject == "")
            {
                return "Error receiving clip from server.";
            }
            DataObject dataObject = StringToDataObject(StringCipher.Decrypt(strDataObject, password).Decompress());
            Clip = new Clip
            {
                Date = DateTime.Now,
                Direction = "Received",
                Token = token_password
            };
            Clipboard.SetDataObject(dataObject);
            return "Successful received clip from server.\nNow you can paste it into your application.";
        }

        private string DataObjectToString(DataObject dataObject)
        {
            if (dataObject == null) return null;

            List<DataObjectFormat> formatList = new List<DataObjectFormat>();

            foreach (string format in dataObject.GetFormats(true))
            {
                DataObjectFormat dataObjectFormat = new DataObjectFormat();
                formatList.Add(dataObjectFormat);
                dataObjectFormat.Format = format;

                object data = dataObject.GetData(format);

                if (data == null) continue;

                dataObjectFormat.Type = data.GetType();

                if (data.GetType().Equals(typeof(System.String)))
                {
                    dataObjectFormat.Data = data as string;
                }
                else if (data.GetType().Equals(typeof(System.IO.MemoryStream)))
                {
                    dataObjectFormat.Data = Convert.ToBase64String((data as System.IO.MemoryStream).ToArray());
                }
                else if (data.GetType().Equals(typeof(System.Drawing.Bitmap)))
                {
                    using (MemoryStream memoryStream = new MemoryStream())
                    {
                        (data as Bitmap).Save(memoryStream, ImageFormat.Bmp);
                        dataObjectFormat.Data = Convert.ToBase64String(memoryStream.ToArray());
                    }
                }
            }

            string json = JsonConvert.SerializeObject(formatList);
            return json;
        }

        private DataObject StringToDataObject(string strDataObject)
        {
            DataObject dataObject = new DataObject();

            var formatList = JsonConvert.DeserializeObject<List<DataObjectFormat>>(strDataObject);

            foreach (DataObjectFormat dataObjectFormat in formatList)
            {
                if (dataObjectFormat.Data == null)
                {
                    dataObject.SetData(dataObjectFormat.Format, null);
                }
                else if (dataObjectFormat.Type.Equals(typeof(System.String)))
                {
                    dataObject.SetData(dataObjectFormat.Format, dataObjectFormat.Data);
                }
                else if (dataObjectFormat.Type.Equals(typeof(System.IO.MemoryStream)))
                {
                    MemoryStream streamData = new MemoryStream(Convert.FromBase64String(dataObjectFormat.Data));
                    dataObject.SetData(dataObjectFormat.Format, streamData);
                }
                else if (dataObjectFormat.Type.Equals(typeof(System.Drawing.Bitmap)))
                {
                    using (MemoryStream streamData = new MemoryStream(Convert.FromBase64String(dataObjectFormat.Data)))
                    {
                        System.Drawing.Bitmap bitmap = new Bitmap(streamData);
                        dataObject.SetData(dataObjectFormat.Format, bitmap);
                    }
                }
                else
                {
                    dataObject.SetData(dataObjectFormat.Format, null);
                }
            }

            return dataObject;
        }

        public static string GeneratePassword(int length)
        {
            const string allowedChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            var password = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] data = new byte[4];
                for (int i = 0; i < length; i++)
                {
                    rng.GetBytes(data);
                    uint randomValue = BitConverter.ToUInt32(data, 0);
                    password.Append(allowedChars[(int)(randomValue % (uint)allowedChars.Length)]);
                }
            }

            return password.ToString();
        }
    }
}