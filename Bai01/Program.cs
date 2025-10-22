using System;

namespace Bai01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. Nhập tháng và năm
            int month;
            do
            {
                month = NhapSoNguyen("Nhap thang: ");
                if (month < 1 || month > 12)
                {
                    Console.Write("Thang khong hop le, nhap lai thang: ");
                }
                else break; 
            }
            while (true);

            int year;
            do
            {
                year = NhapSoNguyen("Nhap nam: ");
                if (year < 1 || year > 9999)
                {
                    Console.Write("Nam khong hop le, vui long nhap lai: ");
                }
                else break;
            } while (true);


            //2. In ra lịch
            Console.WriteLine("====================================================");
            Console.WriteLine("Sun\tMon\tTue\tWed\tThu\tFri\tSat");
            Console.WriteLine("====================================================");

            //Lấy số ngày trong tháng
            int daysInMonth = DateTime.DaysInMonth(year, month);
           
            //Xử lý ngày 1 của tháng
            DateTime firstDate = new DateTime(year, month, 1);
            int firstDayOfWeek = (int)firstDate.DayOfWeek;
            for(int i = 0; i < firstDayOfWeek; i++)
            {
                Console.Write("\t");
            }
            Console.Write(1);

            int currentDayOfWeek = firstDayOfWeek;
            for(int day = 2; day <= daysInMonth; day++)
            {
                if (currentDayOfWeek == 6)
                {
                    Console.Write("\n" + day);
                    currentDayOfWeek = 0;
                }
                else
                {
                    Console.Write("\t" + day);
                    currentDayOfWeek++;
                }
            }
        }

        //Hàm nhập số nguyên
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
