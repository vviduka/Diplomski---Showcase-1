using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Data;
using System.ComponentModel;
using System.Data.Objects;

namespace CloudCord.Models
{
    public class ENconversion
    {
        private const double a = 6378137.0;
        private const double b = 6356752.31414034743838862;
        private const double m0 = 0.9999;
        private const double pi = 3.141592653589793238462643383279502884197169399375;
        private double FI;
        private double Ll;
        private const double RO = 180 / pi;
        private const double L0 = 16.5 / RO;
        private const double e2 = 0.00669438002290341574957495;
        private const double e = 0.081819191042831850706886;
        private const double ecrt2 = 0.00673949677548162190622331;
        private const double ecrt = 0.0820944381519334225976402;
        private const double g = 0.00167922039462940614691445;
        private double m;
        private double Eistok_st;
        private double Nsjever_st;
        private double e2h;
        private double ecrt2h;
        private const double ah = 6377397.155;
        private const double bh = 6356078.96325;
        private double Kh;
        private double FIh;
        private double Lh;
        private double L0h;
        private double Llh;
        private const double Aah = 1.00503730595;
        private const double Bbh = 0.00504784913826;
        private const double Cch = 0.0000105637684036;
        private const double Ddh = 0.0000000206333204250;
        
        [Key]
        public int izracunId { get; set; }

        [StringLength(10, ErrorMessage = "Maximum {2} characters exceeded")]
        public string tagIz { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime createdIz {get; set;}
        
        [Required]
        public double Eistok { get; set; }
        [Required]
        public double Nsjever{ get; set; }

        public double? xp {get; set;}
        public double? yp { get; set; }


        public void Izracun()
        {

            e2h = (Math.Pow(ah, 2) - Math.Pow(bh, 2)) / Math.Pow(ah, 2);
            ecrt2h = (Math.Pow(ah, 2) - Math.Pow(bh, 2)) / Math.Pow(bh, 2);
            

            double Ep = (Eistok - 500000) / m0;
            double Np = Nsjever / m0;

            double c1 = ((3 * g) / 2) - ((29 * Math.Pow(g, 3)) / 12) + ((553 * Math.Pow(g, 5)) / 80);
            double c2 = ((21 * Math.Pow(g, 2)) / 8) - ((1537 * Math.Pow(g, 4)) / 128);
            double c3 = ((151 * Math.Pow(g, 3)) / 24) - ((32373 * Math.Pow(g, 5)) / 640);
            double c4 = (1097 * Math.Pow(g, 4)) / 64;
            double c5 = (8011 * Math.Pow(g, 5)) / 160;

            double Aa = a * (1 - g) * (1 - Math.Pow(g, 2)) * (1 + (9 * Math.Pow(g, 2) / 4) + ((225 * Math.Pow(g, 4)) / 64));

            double U = Np / Aa;


            double FIf = U + Math.Sin(2 * U) * (c1 + (c2 + (c3 + (c4 + c5 * Math.Cos(2 * U)) * Math.Cos(2 * U)) * Math.Cos(2 * U)) * Math.Cos(2 * U));


            double N = a / (Math.Sqrt(1 - e2 * Math.Pow(Math.Sin(FIf), 2)));
            double t = Math.Tan(FIf);
            double w = Math.Sqrt(ecrt2 * Math.Pow(Math.Cos(FIf), 2));

            double g2 = ((-t) / (2 * Math.Pow(N, 2))) * (1 + Math.Pow(w, 2));
            double g4 = (t / (24 * Math.Pow(N, 4))) * (5 + (3 * Math.Pow(t, 2)) + (6 * Math.Pow(w, 2)) - (3 * Math.Pow(w, 4)) - (4 * Math.Pow(w, 6)) - (6 * Math.Pow(w, 2) * Math.Pow(t, 2)) - (9 * Math.Pow(w, 4) * Math.Pow(t, 2)));
            double g6 = ((-t) / (720 * Math.Pow(N, 6))) * (61 + (90 * Math.Pow(t, 2)) + (45 * Math.Pow(t, 4)) + (107 * Math.Pow(w, 2)) + (43 * Math.Pow(w, 4)) - (162 * Math.Pow(t, 2) * Math.Pow(w, 2)) - (318 * Math.Pow(w, 4) * Math.Pow(t, 2)) - (45 * Math.Pow(t, 4) * Math.Pow(w, 2)) + (135 * Math.Pow(t, 4) * Math.Pow(w, 4)));
            double g8 = (t / (40320 * Math.Pow(N, 8))) * (1385 + (3633 * Math.Pow(t, 2)) + (4095 * Math.Pow(t, 4)) + (1575 * Math.Pow(t, 6)) + (3116 * Math.Pow(w, 2)) - (5748 * Math.Pow(t, 2) * Math.Pow(w, 2)) - (3276 * Math.Pow(t, 4) * Math.Pow(w, 2)) - (1260 * Math.Pow(t, 6) * Math.Pow(w, 2)));

            double b1 = (1 / (N * Math.Cos(FIf)));
            double b3 = (-1 / (6 * Math.Pow(N, 3) * Math.Cos(FIf))) * (1 + (2 * Math.Pow(t, 2)) + Math.Pow(w, 2));
            double b5 = (1 / (120 * Math.Pow(N, 5) * Math.Cos(FIf))) * (5 + (28 * Math.Pow(t, 2)) + (24 * Math.Pow(t, 4)) + (6 * Math.Pow(w, 2)) - (3 * Math.Pow(w, 4)) - (4 * Math.Pow(w, 6)) + (8 * Math.Pow(t, 2) * Math.Pow(w, 2)) + (4 * Math.Pow(t, 2) * Math.Pow(w, 4)) + (24 * Math.Pow(t, 2) * Math.Pow(w, 6)));
            double b7 = (-1 / (5040 * Math.Pow(N, 7) * Math.Cos(FIf))) * (61 + (662 * Math.Pow(t, 2)) + (1320 * Math.Pow(t, 4)) + (720 * Math.Pow(t, 6)) + (107 * Math.Pow(w, 2)) + (440 * Math.Pow(t, 2) * Math.Pow(w, 2)) - (234 * Math.Pow(t, 2) * Math.Pow(w, 4)) + (336 * Math.Pow(t, 4) * Math.Pow(w, 2)));
            double b9 = (1 / (362880 * Math.Pow(N, 9) * Math.Cos(FIf))) * (1385 + (24568 * Math.Pow(t, 2)) + (83664 * Math.Pow(t, 4)) + (100800 * Math.Pow(t, 6)) + (40320 * Math.Pow(t, 8)) + (47808 * Math.Pow(t, 4) * Math.Pow(w, 2)));

            FI = FIf + (g2 * Math.Pow(Ep, 2)) + (g4 * Math.Pow(Ep, 4)) + (g6 * Math.Pow(Ep, 6)) + (g8 * Math.Pow(Ep, 8));
            Ll = (b1 * Ep) + (b3 * Math.Pow(Ep, 3)) + (b5 * Math.Pow(Ep, 5)) + (b7 * Math.Pow(Ep, 7)) + (b9 * Math.Pow(Ep, 9));


            double p2 = (1 / (2 * Math.Pow(N, 2))) * (1 + Math.Pow(w, 2));
            double p4 = (1 / (24 * Math.Pow(N, 4))) * (1 + (6 * Math.Pow(w, 2)) + (9 * Math.Pow(w, 4)) + (4 * Math.Pow(w, 6)) - (24 * Math.Pow(w, 4) * Math.Pow(t, 2)) - (24 * Math.Pow(w, 6) * Math.Pow(t, 2)));
            double p6 = (1 / (720 * Math.Pow(N, 6))) * (1 + (47 * Math.Pow(w, 2)) + (223 * Math.Pow(w, 4)) + (397 * Math.Pow(w, 6)) - (72 * Math.Pow(w, 2) * Math.Pow(t, 2)) - (768 * Math.Pow(w, 4) * Math.Pow(t, 2)) - (2952 * Math.Pow(w, 6) * Math.Pow(t, 2)) + (120 * Math.Pow(w, 4) * Math.Pow(t, 4)) + (1080 * Math.Pow(w, 6) * Math.Pow(t, 4)));
            double p8 = (1 / (40320 * Math.Pow(N, 8))) * (1 + (412 * Math.Pow(w, 2)) + (288 * Math.Pow(w, 2) * Math.Pow(t, 2)));

            m = 1 + (p2 * Math.Pow(Ep, 2)) + (p4 * Math.Pow(Ep, 4)) + (p6 * Math.Pow(Ep, 6)) + (p8 * Math.Pow(Ep, 8));


            double tm = (m - 1) * 1000;
            double R = 1000 / tm;



            Eistok_st = (Ep / m) + 500000;
            Nsjever_st = Np / m;


            double L = (Ll + L0) * RO;
            double FIst = FI * RO;

            double L0st = (int)L;
            double L0min = (L - L0st) * 60;
            double L0min_final = (int)L0min;
            double L0sec = (L0min - L0min_final) * 60;

            double FI0st = (int)FIst;
            double FImin = (FIst - FI0st) * 60;
            double FImin_final = (int)FImin;
            double FIsec = (FImin - FImin_final) * 60;





            FIh = FIst;

            Lh = L;


            double zh = ((16.5 * pi) / 180);
            double zzh = ((19.5 * pi) / 180);


            if ((Ll + L0) <= zh)
            {
                Kh = 5500000;
                L0h = 15 / RO;
            }
            if ((Ll + L0) > zh && (Ll + L0) < zzh)
            {
                Kh = 6500000;
                L0h = 18 / RO;
            }

            if ((Ll + L0) > zzh)
            {
                Kh = 7500000;
                L0h = 21 / RO;
            }

            Llh = (Ll + L0) - L0h;


            double m1h = ah * (1 - e2h);


            double m2h = Aah * FI;


            double m3h = (-1) * (Bbh / 2) * Math.Sin(2 * FI);


            double m4h = (Cch / 4) * Math.Sin(4 * FI);



            double m5h = (-1) * (Ddh / 6) * Math.Sin(6 * FI);


            double Z4h = m2h + m3h + m4h + m5h;


            double Bxh = m1h * (Z4h);


            double Nh = ah / (Math.Sqrt(1 - (e2h * Math.Pow(Math.Sin(FI), 2))));

            double th = Math.Tan(FI);


            double wh = 0.0819709403177 * Math.Cos(FI);


            double y1h = Nh * Math.Cos(FI);
            double y2h = ((Nh * Math.Pow(Math.Cos(FI), 3)) * (1 - Math.Pow(th, 2) + Math.Pow(wh, 2))) / 6;
            double y3h = ((Nh * Math.Pow(Math.Cos(FI), 5)) * (5 - (18 * Math.Pow(th, 2)) + Math.Pow(th, 4) + (14 * Math.Pow(wh, 2)) - (58 * Math.Pow(th, 2) * Math.Pow(wh, 2)))) / 120;
            double x1h = (Nh * Math.Sin(FI) * Math.Cos(FI)) / 2;
            double x2h = ((Nh * Math.Sin(FI) * Math.Pow(Math.Cos(FI), 3)) * (5 - Math.Pow(th, 2) + (9 * Math.Pow(wh, 2)) + (4 * Math.Pow(wh, 4)))) / 24;
            double c1h = ((Math.Sin(FI) * Math.Pow(Math.Cos(FI), 2)) * (1 + (3 * Math.Pow(wh, 2)) + (2 * Math.Pow(wh, 4)))) / 3;
            double c2h = ((Math.Sin(FI) * Math.Pow(Math.Cos(FI), 4)) * (2 - Math.Pow(th, 2))) / 15;



            double xph = Bxh + (x1h * Math.Pow(Llh, 2)) + (x2h * Math.Pow(Llh, 4));
            double yph = (y1h * Llh) + (y2h * Math.Pow(Llh, 3)) + (y3h * Math.Pow(Llh, 5));

            this.xp = m0 * xph;
            this.yp = (m0 * yph) + Kh;
        }

    }


}