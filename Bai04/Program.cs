using System;
using System.Collections.Generic;

namespace Bai04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. Nhập hai phân số
            Console.WriteLine("1. Nhap hai phan so: ");
            PhanSo a = new PhanSo();
            PhanSo b = new PhanSo();
            a.Nhap();
            b.Nhap();

            Console.WriteLine();
            Console.WriteLine($"Tong: {a} + {b} = {a + b}");
            Console.WriteLine($"Hieu: {a} - {b} = {a - b}");
            Console.WriteLine($"Tich: {a} * {b} = {a * b}");
            try
            {
                Console.WriteLine($"Thuong: {a} / {b} = {a / b}");
            }
            catch (DivideByZeroException ex)
            {
                Console.WriteLine("Khong chia duoc cho 0 !");
            }

            int n = NhapSoNguyen("\n2. Nhap so luong phan so: ");
            List<PhanSo> list = new List<PhanSo>();
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine($"Phan so {i + 1}:");
                PhanSo newPhanSo = new PhanSo();
                newPhanSo.Nhap();
                list.Add(newPhanSo);
            }

            list.Sort();
            Console.WriteLine($"\nPhan so lon nhat: {list[n - 1]}");
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
                get => mau;
                set
                {
                    if (value == 0)
                        throw new DivideByZeroException("Mau khac 0 !");
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

            private static int GCD(int a, int b)
            {
                a = Math.Abs(a);
                b = Math.Abs(b);
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

                int gcd = GCD(Tu, Mau);
                Tu /= gcd;
                Mau /= gcd;

                if (Mau < 0)
                {
                    Tu = -Tu;
                    Mau = -Mau;
                }
            }

            public override string ToString()
            {
                if (Mau == 1)
                    return $"{Tu}";
                return $"{Tu}/{Mau}";
            }

            // Toán tử
            public static PhanSo operator +(PhanSo a, PhanSo b)
            {
                return new PhanSo(a.Tu * b.Mau + b.Tu * a.Mau, a.Mau * b.Mau);
            }

            public static PhanSo operator -(PhanSo a, PhanSo b)
            {
                return new PhanSo(a.Tu * b.Mau - b.Tu * a.Mau, a.Mau * b.Mau);
            }

            public static PhanSo operator *(PhanSo a, PhanSo b)
            {
                return new PhanSo(a.Tu * b.Tu, a.Mau * b.Mau);
            }

            public static PhanSo operator /(PhanSo a, PhanSo b)
            {
                if (b.Tu == 0)
                    throw new DivideByZeroException("Khong the chia cho phan so bang 0!");
                return new PhanSo(a.Tu * b.Mau, a.Mau * b.Tu);
            }

            public int CompareTo(object obj)
            {
                if (obj is PhanSo p)
                {
                    double x = (double)Tu / Mau;
                    double y = (double)p.Tu / p.Mau;
                    return x.CompareTo(y);
                }
                throw new ArgumentException("Doi tuong khong phai PhanSo!");
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
                    Console.WriteLine("Gia tri khong hop le, vui long nhap lai!");
            } while (!ok);
            return value;
        }
    }
}
