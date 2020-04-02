using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using ThoughtWorks.QRCode.Codec;
/*using ThoughtWorks.QRCode.Codec.Data;
using ThoughtWorks.QRCode.Codec.Util;*/

namespace CMD_QRCode
{
    class Program
    {

        /**
         * program pretvara string u 2D barkod
         * 
         * parametri:
         * 1-vrijednost
         * 2-putanja do slike (putanja + naziv)
         * 3-vrsta enkodinga (Byte, AlphaNumeric, Numeric)
         * 4-veličina (4 default)
         * 5-verzija (1-40)
         * 6-korekcijski level (l,m,q,h)
         */
        static void Main(string[] args)
        {

            try
            {

                if (args[0].Trim() == String.Empty)
                {
                    throw new System.ArgumentException("Nije definiran podatak koji se pretvara u 2d barkod.");
                }

                QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
                String encoding = args[2];
                if (encoding == "Byte")
                {
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                }
                else if (encoding == "AlphaNumeric")
                {
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
                }
                else if (encoding == "Numeric")
                {
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
                }
                else
                {
                    throw new System.ArgumentException("Krivi enkoding. (Byte, AlphaNumeric, Numeric)");
                }

                /*veličina*/
                try
                {
                    int scale = Convert.ToInt16(args[3]);
                    qrCodeEncoder.QRCodeScale = scale;
                }
                catch (Exception ex)
                {
                    throw new System.ArgumentException("Krivo definirana veličina!");
                }
                try
                {
                    int version = Convert.ToInt16(args[4]);
                    qrCodeEncoder.QRCodeVersion = version;
                }
                catch (Exception ex)
                {
                    throw new System.ArgumentException("Kriva verzija (1-40)");
                }

                string errorCorrect = args[5];
                if (errorCorrect == "L")
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
                else if (errorCorrect == "M")
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                else if (errorCorrect == "Q")
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
                else if (errorCorrect == "H")
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                else
                    throw new System.ArgumentException("Kriva korekcija! (L, M, Q, H)");

                Image image;
                String data = args[0];
                image = qrCodeEncoder.Encode(data);

                image.Save(args[1]);

            }
            catch (Exception eGreska)
            {
                Console.Write(eGreska.Message);
            }

        }
    }
}
