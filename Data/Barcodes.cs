using Emgu.CV;
using Emgu.CV.OCR;
using Emgu.CV.Structure;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tesseract;
using Test_Scanning_Software.PhotoScan;

namespace Test_Scanning_Software.Data
{
    public class Barcodes
    {
        public String socialID;
        public String seatID;
        

        public Barcodes(Bitmap inputImage1, Bitmap inputImage2)
        {
            Thread thread1 = new Thread(() => startScan(inputImage1, inputImage2));
            thread1.Start();
        }

        

        private void startScan(Bitmap stretched, Bitmap inputImage2)
        {
            Image<Gray, Byte> grayImage = new Image<Bgr, byte>(stretched).Convert<Gray, Byte>();
            CvInvoke.Threshold(grayImage, grayImage, 120, 1000, Emgu.CV.CvEnum.ThresholdType.BinaryInv);
            bool isDone = false;
            bool foundFirst = false;
            int lastY = grayImage.Height-1;
            int firstY = 0;
            var mego = grayImage.Data;
            for (int y=grayImage.Height-1; y>-1; y--)
            {
                if (isDone) break;
                int whiteCount=0;
                for (int x=0; x<grayImage.Width; x++)
                {
                    if (whiteCount >= 15)
                    {
                        if (y - lastY >4 && foundFirst)
                        {
                            isDone = true;
                            lastY = y;
                            break;
                        }
                        if (foundFirst == false)
                        {
                            foundFirst = true;
                            firstY = y;
                        }
                        lastY = y;
                        whiteCount = 0;
                        break;
                    }

                    if (mego[y,x,0] > 150)
                    {
                        whiteCount = whiteCount + 1;
                    }
                }
            }
            lastY = lastY + 128;
            TesseractEngine me = new TesseractEngine("tessdata","eng");
            me.SetVariable("tessedit_char_whitelist","0123456789");
            BarScan.cropImage(stretched, new Rectangle(0, lastY, stretched.Width, 5 + firstY - lastY)).Save("failure.png");
            Page p1 = me.Process(BarScan.cropImage(stretched,new Rectangle(0,lastY-2, stretched.Width, 5+firstY - lastY)));
            this.seatID = p1.GetText().Replace("\r\n", "").Replace(" ","");
            
            Debug.WriteLine("Seat: " + this.seatID);
            p1.Dispose();
        
            Page p2 = me.Process(BarScan.cropImage(inputImage2, new Rectangle(0, lastY + 16, inputImage2.Width, 5 + firstY - lastY)));
            this.socialID = p2.GetText().Replace(System.Environment.NewLine, "");
            Debug.WriteLine("Social: " + this.socialID);
        }
     
      

        
    }
}
