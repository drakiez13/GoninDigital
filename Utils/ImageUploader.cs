using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json.Linq;

namespace GoninDigital.Utils
{
    internal static class ImageUploader
    {
        private static readonly HttpClient client = new HttpClient();

        private static string ImgToBase64(string filePath)
        {
            Bitmap img = new Bitmap(filePath);

            System.IO.MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Png);
            byte[] byteImage = ms.ToArray();

            return Convert.ToBase64String(byteImage);
        }

        public static async Task<string> UploadAsync(string filePath, string name = null)
        {
            string imgEncoded = ImgToBase64(filePath);

            Dictionary<string, string> values = new Dictionary<string, string>
            {
                { "image", imgEncoded },
            };
            if (name != null)
            {
                values.Add("name", name);
            }

            FormUrlEncodedContent content = new FormUrlEncodedContent(values);
            HttpResponseMessage response = await client.PostAsync("https://api.imgbb.com/1/upload?key=3f74cad166e286e4a1ce1b7d541cd19e", content);
            _ = response.EnsureSuccessStatusCode();

            string responseString = await response.Content.ReadAsStringAsync();

            JObject json = JObject.Parse(responseString);
            return (bool)json["success"] ? (string)json["data"]["url"] : throw new Exception("Image uploader server error");
        }
    }


}
