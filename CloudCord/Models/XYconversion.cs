using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CloudCord.Models
{
    public class XYconversion
    {

        private const double a = 6377397.155;
        private const double b = 6356078.96325;
        private const double m0 = 0.9999;
        private const double pi = 3.141592653589793238462643383279502884197169399375;
        private double K;
        private double Ll;
        private double d;
        private const double Aa = 1.00503730595;
        private const double Bb = 0.00504784913826;
        private const double Cc = 0.0000105637684036;
        private const double Dd = 0.0000000206333204250;
        private const double ecrt = 0.0819708403177;
        private const double RO = 180 / pi;
        private double e2;      
        private double ecrt2;   
        private const double zz = 6378137.0;
        private const double v = 6356752.31414034743838862;
        private const double H0 = 16.5 / RO;
        private const double j2 = 0.00669438002290341574957495;
        private const double j = 0.081819191042831850706886;
        private const double jcrt2 = 0.00673949677548162190622331;
        private const double jcrt = 0.0820944381519334225976402;
        private const double g = 0.00167922039462940614691445;


        [Key]
        public int izracunId { get; set; }

        [StringLength(10, ErrorMessage = "Maximum {2} characters exceeded")]
        public string tagIz { get; set; }

        [Required]
        [Display(Name = "X coordinate")]
        public double xCoord { get; set; }
        [Required]
        [Display(Name = "Y coordinate")]
        public double yCoord { get; set; }
        [Display(Name = "E coordinate")]
        public double? ECoord { get; set; }
        [Display(Name = "N coordinate")]
        public double? NCoord { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime createdIz { get; set; }


        public void izracun()
        {

            e2 = (Math.Pow(a, 2) - Math.Pow(b, 2)) / Math.Pow(a, 2);
            ecrt2 = (Math.Pow(a, 2) - Math.Pow(b, 2)) / Math.Pow(b, 2);

            if (yCoord < 6000000)
            {
                K = 5500000;
                Ll = 15;
            }

            if (yCoord > 6000000 && yCoord < 7000000)
            {
                K = 6500000;
                Ll = 18;
            }
            double xr = xCoord / m0;
            double yr = (yCoord - K) / m0;



            double FILr = (2 * xr) / (a + b);
            double FIL = (FILr * 180) / pi;

            do
            {
                double m1 = a * (1 - e2);

                double m2 = Aa * FILr;

                double m3 = (-1) * (Bb / 2) * Math.Sin(2 * FILr);

                double m4 = (Cc / 4) * Math.Sin(4 * FILr);

                double m5 = (-1) * (Dd / 6) * Math.Sin(6 * FILr);

                double Z4 = m2 + m3 + m4 + m5;

                double Bx = m1 * (Z4);
                d = xr - Bx;
                FILr = FILr + ((2 * d) / (a + b));
                FIL = (FILr * 180) / pi;
                d = Math.Abs(d);

            } while (d >= 0.0001);


            double FIr = FILr;


            double t = Math.Tan(FIr);
            double w = Math.Sqrt(ecrt2) * Math.Cos(FIr);
            double n = a / Math.Sqrt(1 - (e2 * Math.Pow(Math.Sin(FIr), 2)));
            double FI1 = ((-t) * (1 + Math.Pow(w, 2))) / (2 * Math.Pow(n, 2));
            double FI2 = (t * (5 + 3 * Math.Pow(t, 2) + 6 * Math.Pow(w, 2) - 6 * Math.Pow(t, 2) * Math.Pow(w, 2))) / (24 * Math.Pow(n, 4));
            double ll1 = 1 / (n * Math.Cos(FIr));
            double ll2 = (-1) * (1 + 2 * Math.Pow(t, 2) + Math.Pow(w, 2)) / (6 * Math.Pow(n, 3) * Math.Cos(FIr));
            double ll3 = (5 + 28 * Math.Pow(t, 2) + 24 * Math.Pow(t, 4)) / (120 * Math.Pow(n, 5) * Math.Cos(FIr));

            double cc3 = t / n;
            double cc4 = (-t * (1 + Math.Pow(t, 2) - Math.Pow(w, 2))) / (3 * Math.Pow(n, 3));
            double cc5 = (t * (2 + 5 * Math.Pow(t, 2) + 3 * Math.Pow(t, 4))) / (15 * Math.Pow(n, 5));

            double FIrad = FIr + FI1 * Math.Pow(yr, 2) + FI2 * Math.Pow(yr, 4);     //FI 
            double l = ll1 * yr + ll2 * Math.Pow(yr, 3) + ll3 * Math.Pow(yr, 5);
            double c = cc3 * yr + cc4 * Math.Pow(yr, 3) + cc5 * Math.Pow(yr, 5);

            double FI0 = RO * FIrad;
            double L0 = (Ll / RO) + l;                    // LAMBDA
            double C0 = c * RO;


            double NIr = FIrad;
            double Hr = L0;

            double dd = Hr - H0;

            double hc1 = ((-3 * g) / 2) + ((31 * Math.Pow(g, 3)) / 24) + ((-669 * Math.Pow(g, 5)) / 640);
            double hc2 = ((15 * Math.Pow(g, 2)) / 8) + ((-435 * Math.Pow(g, 4)) / 128);
            double hc3 = ((-35 * Math.Pow(g, 3)) / 12) + ((651 * Math.Pow(g, 5)) / 80);
            double hc4 = (315 * Math.Pow(g, 4)) / 64;
            double hc5 = (-693 * Math.Pow(g, 5)) / 80;

            double Oa = zz * (1 - g) * (1 - Math.Pow(g, 2)) * (1 + (9 * Math.Pow(g, 2) / 4) + ((225 * Math.Pow(g, 4)) / 64));

            double Bfi = Oa * (NIr + Math.Sin(2 * NIr) * (hc1 + (hc2 + (hc3 + (hc4 + hc5 * Math.Cos(2 * NIr)) * Math.Cos(2 * NIr)) * Math.Cos(2 * NIr)) * Math.Cos(2 * NIr)));

            double I = zz / (Math.Sqrt(1 - j2 * Math.Pow(Math.Sin(NIr), 2)));
            double xh = Math.Tan(NIr);
            double yh = Math.Sqrt(jcrt2 * Math.Pow(Math.Cos(NIr), 2));

            double s1 = I * Math.Cos(NIr);
            double s3 = ((I * Math.Pow(Math.Cos(NIr), 3)) / 6) * (1 - Math.Pow(xh, 2) + Math.Pow(yh, 2));
            double s5 = ((I * Math.Pow(Math.Cos(NIr), 5)) / 120) * (5 - (18 * Math.Pow(xh, 2)) + Math.Pow(xh, 4) + (14 * Math.Pow(yh, 2)) + (13 * Math.Pow(yh, 4)) + (4 * Math.Pow(yh, 6)) - (58 * Math.Pow(xh, 2) * Math.Pow(yh, 2)) - (64 * Math.Pow(xh, 2) * Math.Pow(yh, 4)) - (24 * Math.Pow(xh, 2) * Math.Pow(yh, 6)));
            double s7 = ((I * Math.Pow(Math.Cos(NIr), 7)) / 5040) * (61 - (479 * Math.Pow(xh, 2)) + (179 * Math.Pow(xh, 4)) - Math.Pow(xh, 6) + (331 * Math.Pow(yh, 2)) + (715 * Math.Pow(yh, 4)) - (3298 * Math.Pow(xh, 2) * Math.Pow(yh, 2)) - (8655 * Math.Pow(xh, 2) * Math.Pow(yh, 4)) - (10964 * Math.Pow(xh, 2) * Math.Pow(yh, 6)) + (1771 * Math.Pow(xh, 4) * Math.Pow(yh, 2)) + (6080 * Math.Pow(xh, 4) * Math.Pow(yh, 4)));
            double s9 = ((I * Math.Pow(Math.Cos(NIr), 9)) / 362880) * (1385 - (19028 * Math.Pow(xh, 2)) + (18270 * Math.Pow(xh, 4)) - (1636 * Math.Pow(xh, 6)) + (12284 * Math.Pow(yh, 2)) - (214140 * Math.Pow(yh, 2) * Math.Pow(xh, 2)) + (290868 * Math.Pow(yh, 2) * Math.Pow(xh, 4)) - (47188 * Math.Pow(yh, 2) * Math.Pow(xh, 6)));

            double p2 = (((I * Math.Pow(Math.Cos(NIr), 2)) / 2) * xh);
            double p4 = (((I * Math.Pow(Math.Cos(NIr), 4)) / 24) * xh) * (5 - Math.Pow(xh, 2) + (9 * Math.Pow(yh, 2)) + (4 * Math.Pow(yh, 4)));
            double p6 = (((I * Math.Pow(Math.Cos(NIr), 6)) / 720) * xh) * (61 - (58 * Math.Pow(xh, 2)) + Math.Pow(xh, 4) + (270 * Math.Pow(yh, 2)) + (445 * Math.Pow(yh, 4)) + (324 * Math.Pow(yh, 6)) - (330 * Math.Pow(xh, 2) * Math.Pow(yh, 2)) - (680 * Math.Pow(xh, 2) * Math.Pow(yh, 4)) - (600 * Math.Pow(xh, 2) * Math.Pow(yh, 6)));
            double p8 = (((I * Math.Pow(Math.Cos(NIr), 8)) / 40320) * xh) * (1385 - (3110 * Math.Pow(xh, 2)) + (543 * Math.Pow(xh, 4)) - Math.Pow(xh, 6) + (10899 * Math.Pow(yh, 2)) + (34419 * Math.Pow(yh, 4)) - (32802 * Math.Pow(xh, 2) * Math.Pow(yh, 2)) - (129087 * Math.Pow(xh, 2) * Math.Pow(yh, 4)) + (9219 * Math.Pow(xh, 4) * Math.Pow(yh, 2)) + (49644 * Math.Pow(xh, 4) * Math.Pow(yh, 4)));

            double Ep = (s1 * dd) + (s3 * Math.Pow(dd, 3)) + (s5 * Math.Pow(dd, 5)) + (s7 * Math.Pow(dd, 7)) + (s9 * Math.Pow(dd, 9));

            double Np = Bfi + (p2 * Math.Pow(dd, 2)) + (p4 * Math.Pow(dd, 4)) + (p6 * Math.Pow(dd, 6)) + (p8 * Math.Pow(dd, 8));

            ECoord = (m0 * Ep) + 500000;
            NCoord = m0 * Np;
        }
    }
}
