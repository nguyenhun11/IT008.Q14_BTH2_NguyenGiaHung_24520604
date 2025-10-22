using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Bai05
{
    internal class Program
    {
        public delegate bool DieuKieu(KhuDat k);

        static void Main(string[] args)
        {
            //1. Nhập danh sách 
            Console.WriteLine("1. Nhap danh sach");
            CongTyDaiPhu cty = new CongTyDaiPhu();
            cty.Nhap();

            //2. Xuất danh sách
            Console.WriteLine("\n2. Xuat danh sach\n2.1. Khu dat: ");
            cty.Xuat(KhuDat.LOAI.KhuDat);
            Console.WriteLine("2.2. Nha pho: ");
            cty.Xuat(KhuDat.LOAI.NhaPho);
            Console.WriteLine("2.3. Chung cu:");
            cty.Xuat(KhuDat.LOAI.ChungCu);

            //3. Tổng giá bán 3 loại
            Console.WriteLine("\n3. Tong gia ban:" +
                "\n- Khu dat: " + cty.GiaBan(KhuDat.LOAI.KhuDat) +
                "\n- Nha pho: " + cty.GiaBan(KhuDat.LOAI.NhaPho) +
                "\n- Chung cu: " + cty.GiaBan(KhuDat.LOAI.ChungCu));

            //4. Xuất danh sách theo điều kiện
            Console.WriteLine("\n4. Xuat danh sach" +
                "\n4.1. Khu dat > 100:");
            cty.Xuat(khu => (khu.loai == KhuDat.LOAI.KhuDat && khu.DienTich > 100));
            Console.WriteLine("\n4.2. Nha pho dien tich > 60, nam > 2019");
            cty.Xuat(khu =>
            {
                if (khu is NhaPho nhaPho)
                {
                    return nhaPho.DienTich > 60 && nhaPho.NamXayDung >= 2019;
                }
                return false;
            });

            //5. Tìm kiếm nhà phố hoặc chung cư
            Console.WriteLine("\n5. Tim kiem nha pho hoac chung cu");
            cty.Find();

        }


        public class CongTyDaiPhu
        {
            List<KhuDat> list;

            public CongTyDaiPhu()
            {
                list = new List<KhuDat>();
            }

            public void Nhap()
            {
                Console.WriteLine("NHAP DANH SACH BAT DONG SAN: ");
                do
                {
                    KhuDat newKhuDat;
                    int choice = NhapSoNguyen("[0]: Thoat\t[1]: Khu dat\t[2]: Nha pho\t[3]: Chung cu\n");
                    if (choice == 0) break;
                    switch (choice)
                    {
                        case 2: newKhuDat = new NhaPho(); break;
                        case 3: newKhuDat = new ChungCu(); break;
                        default: newKhuDat = new KhuDat(); break;
                    }
                    newKhuDat.Nhap();
                    list.Add(newKhuDat);
                } 
                while (true);
            }

            public void Xuat(KhuDat.LOAI loai)
            {
                bool has = false;
                foreach (KhuDat khuDat in list)
                {
                    if (loai == KhuDat.LOAI.All || khuDat.loai == loai)
                    {
                        Console.WriteLine(khuDat);
                        has = true;
                    }
                }
                if (!has)
                {
                    Console.WriteLine("Khong co ket qua!");
                }
            }

            public void Xuat(DieuKieu dk)
            {
                bool has = false;

                foreach(KhuDat khuDat in list)
                {
                    if (dk(khuDat))
                    {
                        Console.WriteLine(khuDat);
                        has = true;
                    }
                }

                if (!has)
                {
                    Console.WriteLine("Khong co ket qua!");
                }
            }

            public double GiaBan(KhuDat.LOAI loai)
            {
                double gia = 0;
                foreach (KhuDat khudat in list)
                {
                    if (loai == KhuDat.LOAI.All || khudat.loai == loai)
                    {
                        gia += khudat.GiaBan;
                    }
                }
                return gia;
            }

            public void Find()
            {
                Console.Write("+ Dia diem: ");
                string diadiem = Console.ReadLine()?.Trim() ?? string.Empty;
                double gia = NhapSoThuc("+ Gia ban: ");
                double dienTich = NhapSoThuc("+ Dien tich: ");

                List<KhuDat> result = new List<KhuDat>();
                foreach (var khuDat in list)
                {
                    if (khuDat.loai == KhuDat.LOAI.KhuDat) continue;
                    if (string.IsNullOrWhiteSpace(diadiem) || string.IsNullOrWhiteSpace(khuDat.DiaDiem)) continue;
                    if (khuDat.DiaDiem.IndexOf(diadiem, StringComparison.OrdinalIgnoreCase) < 0) continue;
                    if (khuDat.GiaBan > gia) continue;
                    if (khuDat.DienTich < dienTich) continue;
                    result.Add(khuDat);
                }

                if (result.Count > 0)
                {
                    Console.WriteLine("KET QUA TIM KIEM: ");
                    foreach (KhuDat khu in result)
                    {
                        Console.WriteLine(khu);
                    }
                }
                else
                {
                    Console.WriteLine("KHONG TIM THAY KET QUA");
                }
            }
        }

        public class KhuDat
        {
            public string DiaDiem { get; protected set; }
            public double GiaBan { get; protected set; }
            public double DienTich { get; protected set; }

            public enum LOAI
            {
                KhuDat,
                NhaPho,
                ChungCu,
                All
            }

            public LOAI loai { get; protected set; }

            public KhuDat()
            {
                DiaDiem = string.Empty;
                GiaBan = 0;
                DienTich = 0;
                loai = LOAI.KhuDat;
            }

            public virtual void Nhap()
            {
                Console.WriteLine("NHAP KHU DAT:");
                NhapThongTinCoBan();
            }

            protected void NhapThongTinCoBan()
            {
                Console.Write("- Dia diem: ");
                DiaDiem = Console.ReadLine();
                GiaBan = NhapSoThuc("- Gia ban: ");
                DienTich = NhapSoThuc("- Dien tich: ");
            }

            protected virtual string ThongTinCoBan()
            {
                return "\n- Dia diem: " + DiaDiem +
                    "\n- Gia ban: " + GiaBan.ToString() +
                    "\n- Dien tich: " + DienTich.ToString();
            }

            public override string ToString()
            {
                return "KHU DAT:" +
                    ThongTinCoBan();
            }
        }

        public class NhaPho : KhuDat
        {
            public int NamXayDung { get; protected set; }
            int soTang;

            public NhaPho() : base()
            {
                NamXayDung = 0;
                soTang = 0;
                loai = LOAI.NhaPho;
            }

            public override void Nhap()
            {
                Console.WriteLine("NHAP NHA PHO:");
                base.NhapThongTinCoBan();
                NamXayDung = NhapSoNguyen("- Nam xay dung: ");
                soTang = NhapSoNguyen("- So tang: ");
            }

            public override string ToString()
            {
                return "NHA PHO: " + ThongTinCoBan() + 
                    "\n- Nam xay dung: " + NamXayDung.ToString() +
                    "\n- So tang: " + soTang.ToString();
            }
        }

        public class ChungCu : KhuDat
        {
            int tang;

            public ChungCu() : base()
            {
                tang = 0;
                loai = LOAI.ChungCu;
            }

            public override void Nhap()
            {
                Console.WriteLine("NHAP CHUNG CU:");
                base.NhapThongTinCoBan();
                tang = NhapSoNguyen("- So tang: ");
            }

            public override string ToString()
            {
                return "CHUNG CU: " + ThongTinCoBan() +
                    "\n- So tang: " + tang.ToString();
            }
        }

        public static int NhapSoNguyen(string thongBao)
        {
            int value;
            bool ok;
            do
            {
                Console.Write(thongBao);
                ok = int.TryParse(Console.ReadLine(), out value);
                if (!ok)
                {
                    Console.WriteLine("Gia tri khong hop le, vui long nhap lai!");
                }
            } while (!ok);
            return value;
        }

        public static double NhapSoThuc(string prompt)
        {
            double value;
            bool isSuccess;
            do
            {
                Console.Write(prompt);
                isSuccess = double.TryParse(Console.ReadLine(), out value);
                if (!isSuccess)
                {
                    Console.WriteLine("Gia tri khong hop le, vui long nhap so thuc!");
                }
            } while (!isSuccess);
            return value;
        }
    }
}
