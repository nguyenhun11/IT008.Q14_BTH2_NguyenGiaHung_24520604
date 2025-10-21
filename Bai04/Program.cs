using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Bai04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("1. Nhap hai phan so: ");
            PhanSo a = new PhanSo();
            PhanSo b = new PhanSo();
            a.Nhap();
            b.Nhap();

            Console.WriteLine("\nTong: " + a + " + " + b + " = " + (a + b));
            Console.WriteLine("Hieu: " + a + " - " + b + " = " + (a - b));
            Console.WriteLine("Tich: " + a + " * " + b + " = " + (a * b));
            Console.WriteLine("Thuong: " + a + " / " + b + " = " + (a / b));

            int n = NhapSoNguyen("\n2. Nhap so luong phan so: ");
            List<PhanSo> list = new List<PhanSo>();
            for(int i = 0; i < n; i++)
            {
                Console.WriteLine("Phan so " + (i+1) + ":");
                PhanSo newPhanSo = new PhanSo();
                newPhanSo.Nhap();
                list.Add(newPhanSo);
            }

            list.Sort();
            Console.WriteLine("\nPhan so lon nhat: " + list[n - 1]);
            Console.Write("Day phan so da sap xep: ");
            foreach (PhanSo ps in list)
            {
                Console.Write(ps + " ");
            }
        }

        public class PhanSo : IComparable
        {
            public int Tu { get; set; }
            private int mau;
            public int Mau
            {
                get
                {
                    return mau;
                }

                set
                {
                    if (value == 0) throw new DivideByZeroException("Mau khac 0 !");
                    mau = value;
                }
            }

            public PhanSo(int t = 0, int m = 1)
            {
                Tu = t;
                try
                {
                    Mau = m;
                }
                catch (DivideByZeroException ex)
                {
                    Console.WriteLine(ex.Message);
                    Mau = 1;
                }
                RutGon();
            }

            public override string ToString()
            {
                if (Mau == 1)
                    return $"{Tu}";
                return $"{Tu}/{Mau}";
            }

            public void Nhap()
            {
                Tu = NhapSoNguyen("Tu: ");

                bool hopLe = false;
                while (!hopLe)
                {
                    try
                    {
                        Mau = NhapSoNguyen("Mau: ");
                        hopLe = true;
                    }
                    catch (DivideByZeroException ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
                RutGon();
            }

            private int GCD(int a, int b)
            {
                while (b != 0)
                {
                    int temp = b;
                    b = a % b;
                    a = temp;
                }
                return a;
            }

            private void RutGon()
            {
                if (Tu == 0)
                {
                    Mau = 1;
                    return;
                }
                int gcd = GCD(Math.Abs(Tu), Math.Abs(Mau));
                Tu /= gcd;
                Mau /= gcd;

                if (Mau < 0)
                {
                    Tu = -Tu;
                    Mau = -Mau;
                }
            }



            public static PhanSo operator +(PhanSo a, PhanSo b)
            {
                return new PhanSo(b.Tu * a.Mau + b.Mau * a.Tu, b.Mau * a.Mau);
            }

            public static PhanSo operator *(PhanSo a, PhanSo b)
            {
                return new PhanSo(a.Tu * b.Tu, a.Mau * b.Mau);
            }

            public static PhanSo operator -(PhanSo a, PhanSo b)
            {
                return a + b * new PhanSo(-1);
            }

            public static PhanSo operator /(PhanSo a, PhanSo b)
            {
                return new PhanSo(a.Tu * b.Mau, a.Mau * b.Tu);
            }

            public static explicit operator double(PhanSo p)
            {
                return (double)p.Tu / (double)p.Mau;
            }

            public int CompareTo(object obj)
            {
                if (obj == null) return 1;

                if (obj is PhanSo otherPhanSo)
                {
                    // So sánh dựa trên giá trị double của phân số
                    double thisValue = (double)this.Tu / this.Mau;
                    double otherValue = (double)otherPhanSo.Tu / otherPhanSo.Mau;

                    // Sử dụng phương thức CompareTo có sẵn của kiểu double
                    return thisValue.CompareTo(otherValue);
                }
                else
                {
                    throw new ArgumentException("Doi tuong so sanh khong phai la PhanSo!");
                }
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

    }
}
