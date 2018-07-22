using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Ship.Infrastructure.Utility
{
    public class StreamHelper
    {
        private static readonly ILogger<StreamHelper> logger;
        /// <summary>
        /// 将 Stream 转成 byte[]
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        public static byte[] StreamToBytes(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            // 设置当前流的位置为流的开始
            stream.Seek(0, SeekOrigin.Begin);
            return bytes;
        }

        /// <summary>
        /// 将 byte[] 转成 Stream
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static Stream BytesToStream(byte[] bytes)
        {
            Stream stream = new MemoryStream(bytes);
            return stream;
        }

        /************************************************/


        /// <summary> 
        /// 根据图片路径返回图片的字节流byte[] 
        /// </summary> 
        /// <param name="imagePath">图片路径</param> 
        /// <returns>返回的字节流</returns> 
        public static byte[] Image2ByteWithPath(string imagePath)
        {
            try
            {
                FileStream files = new FileStream(imagePath, FileMode.Open);
                byte[] imgByte = new byte[files.Length];
                files.Read(imgByte, 0, imgByte.Length);
                files.Close();
                return imgByte;
            }
            catch (Exception ex)
            {
                logger.LogError(ex,"StreamHelper.Image2ByteWithPath 异常");
                return null;
            }
        }

        /// <summary> 
        /// 字节流转换成图片 
        /// </summary> 
        /// <param name="byt">要转换的字节流</param> 
        /// <returns>转换得到的Image对象</returns> 
        public static Image BytToImg(byte[] byt)
        {
            try
            {
                MemoryStream ms = new MemoryStream(byt);
                Image img = Image.FromStream(ms);
                return img;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "StreamHelper.BytToImg 异常");
                return null;
            }
        }

        /// <summary> 
        /// 图片转换成字节流 
        /// </summary> 
        /// <param name="img">要转换的Image对象</param> 
        /// <returns>转换后返回的字节流</returns> 
        public static byte[] ImgToByt(Bitmap bmp)
        {
            MemoryStream ms = new MemoryStream();
            byte[] imagedata = null;
            bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            imagedata = ms.GetBuffer();
            return imagedata;
        }

        /// <summary>
        /// 将Image转换为byte[]
        /// </summary>
        /// <param name="image">Image</param>
        /// <returns>byte[]</returns>
        public static byte[] ConvertImage(Image image)
        {
            MemoryStream ms = new MemoryStream();
            BinaryFormatter bf = new BinaryFormatter();
            bf.Serialize(ms, (object)image);
            ms.Close();
            return ms.ToArray();
        }

        /// <summary>
        /// 把图片Url转化成Image对象
        /// </summary>
        /// <param name="imageUrl"></param>
        /// <returns></returns>
        public static Image Url2Img(string imageUrl)
        {
            try
            {
                if (string.IsNullOrEmpty(imageUrl))
                {
                    return null;
                }

                WebRequest webreq = WebRequest.Create(imageUrl);
                WebResponse webres = webreq.GetResponse();
                Stream stream = webres.GetResponseStream();
                Image image;
                image = Image.FromStream(stream);
                stream.Close();

                return image;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "StreamHelper.Url2Img 异常");
            }

            return null;
        }

        /// <summary>
        /// 把本地图片路径转成Image对象
        /// </summary>
        /// <param name="imagePath"></param>
        /// <returns></returns>
        public static Image ImagePath2Img(string imagePath)
        {
            try
            {
                if (string.IsNullOrEmpty(imagePath))
                {
                    return null;
                }

                byte[] bytes = Image2ByteWithPath(imagePath);
                Image image = BytToImg(bytes);
                return image;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "StreamHelper.ImagePath2Img 异常");
                return null;
            }
        }
    }
}
