using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace MerchantDB
{
    public class ByteConverter
    {
        /// 將 Byte 陣列轉換為 Image。
        /// </summary>
        /// <param name="Buffer">Byte 陣列。</param>        
        public Image BufferToImage(byte[] Buffer)
        {
            if (Buffer == null || Buffer.Length == 0) { return null; }
            byte[] data = null;
            Image oImage = null;
            Bitmap oBitmap = null;
            //建立副本
            data = (byte[])Buffer.Clone();
            try
            {
                MemoryStream oMemoryStream = new MemoryStream(Buffer);
                //設定資料流位置
                oMemoryStream.Position = 0;
                oImage = System.Drawing.Image.FromStream(oMemoryStream);
                //建立副本
                oBitmap = new Bitmap(oImage);
            }
            catch
            {
                throw;
            }
            //return oImage;
            return oBitmap;
        }
    }
}