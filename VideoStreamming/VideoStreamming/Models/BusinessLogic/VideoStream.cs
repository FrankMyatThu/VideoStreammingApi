using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace VideoStreamming.Models.BusinessLogic
{
    public class VideoStream
    {
        private readonly string _filename;

        public VideoStream(string filename, string ext)
        {
            _filename = string.Format("D:\\Test\\{0}.{1}", filename, ext);
        }

        public async void WriteToStream(Stream outputStream, HttpContent content, TransportContext context)
        {
            try
            {
                using (var video = File.Open(_filename, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    var length = (int)video.Length;
                    var buffer = new byte[length];
                    var bytesRead = 1;

                    while (length > 0 && bytesRead > 0)
                    {
                        bytesRead = video.Read(buffer, 0, Math.Min(length, buffer.Length));
                        await outputStream.WriteAsync(buffer, 0, bytesRead);
                        length -= bytesRead;
                    }
                }
            }
            catch (HttpException ex)
            {
                return;
            }
            finally
            {
                outputStream.Close();
            }
        }

        //public void WriteToStream(Stream outputStream, HttpContent content, TransportContext context)
        //{
        //    try
        //    {
        //        using (var video = File.Open(_filename, FileMode.Open, FileAccess.Read, FileShare.Read))
        //        {
        //            var length = (int)video.Length;
        //            var buffer = new byte[length];
        //            var bytesRead = 1;

        //            while (length > 0 && bytesRead > 0)
        //            {
        //                bytesRead = video.Read(buffer, 0, Math.Min(length, buffer.Length));
        //                outputStream.WriteAsync(buffer, 0, bytesRead);
        //                length -= bytesRead;
        //            }
        //        }
        //    }
        //    catch (HttpException ex)
        //    {
        //        return;
        //    }
        //    finally
        //    {
        //        outputStream.Close();
        //    }
        //}
    }
}