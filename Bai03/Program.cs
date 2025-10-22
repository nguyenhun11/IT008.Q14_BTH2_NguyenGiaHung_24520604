using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Bai03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. Tạo, nhập và xuất ma trận
            MaTran mat = new MaTran();
            mat.Nhap();
            mat.Print();

            //2. Tìm số nguyên trong ma trận
            int iToFind = NhapSoNguyen("\n2. Nhap so can tim: ");
            mat.FindFirstIf((x) => (x == iToFind)).Print();

            //3. Tìm các phần tử là số nguyên tố
            Console.WriteLine("\n3. Cac phan tu la so nguyen to: ");
            mat.Print((x) => IsPrime(x));

            //4. Tìm các dòng có nhiều số nguyên tố nhất
            List<int> rows = mat.MaxRowCountIf((x) => IsPrime(x));
            Console.WriteLine("\n4. Dong co so nguyen to nhieu nhat:");
            foreach (int r in rows)
            {
                Console.Write("- Dong " + (r + 1) + ":\t");
                mat.PrintRow(r);
            }
        }
        
        public delegate bool DieuKien(int i);

        public class MaTran
        {
            private List<List<int>> mat;
            public int Row
            {
                get
                {
                    return mat.Count;
                }
            }

            public int Col
            {
                get
                {
                    if (Row != 0)
                    {
                        return mat[0].Count;
                    }
                    else return 0;
                }
            }

            public MaTran(int r = 1, int c = 1, bool random = true, int maxVal = 100, int minVal = -100)
            {
                CreateMatrix(r, c, random, maxVal, minVal);
            }

            //Tạo ma trận
            private void CreateMatrix(int r = 1, int c = 1, bool random = true, int maxVal = 100, int minVal = -100)
            {
                mat = new List<List<int>>();
                if (r <= 0 || c < 1) return;
                Random rand = new Random();
                for (int i = 0; i < r; i++)
                {
                    List<int> row = new List<int>();
                    for (int j = 0; j < c; j++)
                    {
                        row.Add(random ? rand.Next(minVal, maxVal + 1) : 0);
                    }
                    mat.Add(row);
                }
            }

            public void Nhap()
            {
                Console.WriteLine("Tao ma tran:");
                int dong = NhapSoNguyen("Dong: ");
                int cot = NhapSoNguyen("Cot: ");
                int choice = NhapSoNguyen("[1]: Tao tu dong\t"
                    + "[0]: Tao thu cong\n");
                if (choice > 0)
                {
                    CreateMatrix(dong, cot);
                }
                else
                {
                    CreateMatrix(dong, cot, false);
                    for (int r = 0; r < dong; r++)
                    {
                        for (int c = 0; c < cot; c++)
                        {
                            Console.Write("Nhap [{0}][{1}]: ", r, c);
                            mat[r][c] = NhapSoNguyen("");
                        }
                    }
                }
            }

            //In ma trận
            public void Print()
            {
                for (int r = 0; r < Row; r++)
                {
                    PrintRow(r);
                }
            }

            //In dòng r trong ma trận
            public void PrintRow(int r)
            {
                if (r < 0 || r >= Row) return;
                foreach (int i in mat[r])
                {
                    Console.Write(i + "\t");
                }
                Console.WriteLine();
            }

            //In ra các số thỏa điều kiện dk
            public void Print(DieuKien dk)
            {
                foreach (var row in mat)
                {
                    foreach (int i in row)
                    {
                        if (dk(i))
                        {
                            Console.Write(i + " ");
                        }
                    }
                }
                Console.WriteLine();
            }

            //Vị trị trong ma trận
            public struct Position
            {
                public int row { get; private set; }
                public int col { get; private set; }
                public Position(int r, int c)
                {
                    row = r; col = c;
                }
                public void Print()
                {
                    Console.WriteLine("Dong {0} - cot {1}", row + 1, col + 1);
                }
            }

            //Giá giá trị một số trong vị trí
            public int GetValue(Position pos)
            {
                if (pos.row < 0 || pos.col < 0
                    || pos.row >= Row || pos.col >= Col)
                {
                    return -1;
                }
                return mat[pos.row][pos.col];
            }

            //Vị trí đầu tiên nếu tìm thấy số thỏa điều kiện dk
            public Position FindFirstIf(DieuKien dk)
            {
                for (int r = 0; r < Row; r++)
                {
                    for (int c = 0; c < Col; c++)
                    {
                        if (dk(mat[r][c]))
                        {
                            return new Position(r, c);
                        }
                    }
                }
                Console.Write("Khong tim thay: ");
                return new Position(-1, -1);
            }

            //Những dòng có nhiều số nhất thỏa điều kiện dk
            public List<int> MaxRowCountIf(DieuKien dk)
            {
                List<int> result = new List<int>();
                int maxCount = 0;

                for (int r = 0; r < Row; r++)
                {
                    int countRow = 0;
                    for (int c = 0; c < Col; c++)
                    {
                        if (dk(mat[r][c])) countRow++;
                    }

                    if (countRow > maxCount)
                    {
                        maxCount = countRow;
                        result.Clear();
                        result.Add(r);
                    }
                    else if (countRow == maxCount && countRow > 0)
                    {
                        result.Add(r);
                    }
                }
                return result;
            }
        }

        //Nhập số nguyên
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

        //Kiểm tra số nguyên tố
        public static bool IsPrime(int x)
        {
            if (x < 2) return false;
            int root = (int)Math.Sqrt(x);
            for(int i = 2; i <= root; i++)
            {
                if (x % i == 0) return false;
            }
            return true;
        }
    }
}
