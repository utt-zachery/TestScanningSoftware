
using Emgu.CV;
using Emgu.CV.Cvb;
using Emgu.CV.OCR;
using Emgu.CV.Structure;
using Emgu.CV.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Scanning_Software.PhotoScan
{
    public class BarScan
    {
        public static RotatedRect[] detectBarcodes(Bitmap inputImage)
        {
            RotatedRect[] toReturn = new RotatedRect[2];
            Image<Gray, Byte> grayImage = new Image<Bgr, byte>(inputImage).Convert<Gray, Byte>();
            CvInvoke.Threshold(grayImage, grayImage, 120, 1000, Emgu.CV.CvEnum.ThresholdType.BinaryInv);
           
          
            CvInvoke.Blur(grayImage, grayImage, new Size(27, 9), new Point(-1, -1));
            Mat kernel= CvInvoke.GetStructuringElement(Emgu.CV.CvEnum.ElementShape.Rectangle, new Size(21, 7), new Point(-1, -1));
            CvInvoke.MorphologyEx(grayImage, grayImage,Emgu.CV.CvEnum.MorphOp.Close,kernel, new Point(-1,-1),1,Emgu.CV.CvEnum.BorderType.Default,new MCvScalar(1));
            CvInvoke.Erode(grayImage, grayImage, null,new Point(-1,-1),40, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(1));
            CvInvoke.Dilate(grayImage, grayImage, null, new Point(-1, -1), 40, Emgu.CV.CvEnum.BorderType.Default, new MCvScalar(1));
                        grayImage.ToBitmap().Save("backscatter.png");
            VectorOfVectorOfPoint contours = new VectorOfVectorOfPoint();
            CvInvoke.FindContours(grayImage, contours,null, Emgu.CV.CvEnum.RetrType.List, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxSimple);
            Graphics boxGraphics = Graphics.FromImage(inputImage);

            toReturn[0] = CvInvoke.MinAreaRect(contours[0]);
            toReturn[1] = CvInvoke.MinAreaRect(contours[1]);

            Debug.WriteLine("Barcode Size: " + contours.Size);
            return toReturn;
        }

        public static Bitmap cropImage(Image img, Rectangle cropArea)
        {
            Bitmap target = new Bitmap(cropArea.Width, Math.Abs(cropArea.Height));

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(img, new Rectangle(0, 0, target.Width, target.Height),
                                 cropArea,
                                 GraphicsUnit.Pixel);
            }
            return target;
        }

        public static Bitmap rotateImage(Bitmap bmp, float angle)
        {
            float alpha = -angle;

            //edit: negative angle +360
            while (alpha < 0) alpha += 360;

            float gamma = 90;
            float beta = 180 - angle - gamma;

            float c1 = bmp.Height;
            float a1 = (float)(c1 * Math.Sin(alpha * Math.PI / 180) / Math.Sin(gamma * Math.PI / 180));
            float b1 = (float)(c1 * Math.Sin(beta * Math.PI / 180) / Math.Sin(gamma * Math.PI / 180));

            float c2 = bmp.Width;
            float a2 = (float)(c2 * Math.Sin(alpha * Math.PI / 180) / Math.Sin(gamma * Math.PI / 180));
            float b2 = (float)(c2 * Math.Sin(beta * Math.PI / 180) / Math.Sin(gamma * Math.PI / 180));

            int width = Convert.ToInt32(b2 + a1);
            int height = Convert.ToInt32(b1 + a2);

            Bitmap rotatedImage = new Bitmap(Math.Abs(width), Math.Abs(height));
            using (Graphics g = Graphics.FromImage(rotatedImage))
            {
                g.TranslateTransform(rotatedImage.Width / 2, rotatedImage.Height / 2); //set the rotation point as the center into the matrix
                g.RotateTransform(angle); //rotate
                g.TranslateTransform(-rotatedImage.Width / 2, -rotatedImage.Height / 2); //restore rotation point into the matrix
                g.DrawImage(bmp, new Point((width - bmp.Width) / 2, (height - bmp.Height) / 2)); //draw the image on the new bitmap
            }
            return rotatedImage;
        }

        
    }
}
