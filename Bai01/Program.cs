using System;

namespace Bai01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //1. Nhập tháng và năm
            Console.Write("Nhap thang: ");
            int month;
            do
            {
                month = int.Parse(Console.ReadLine());
                if (month < 1 || month > 12)
                {
                    Console.Write("Thang khong hop le, nhap lai thang: ");
                }
                else break; 
            }
            while (true);
            Console.Write("Nhap nam: ");
            int year = int.Parse(Console.ReadLine());

            //2. In ra lịch
            Console.WriteLine("====================================================");
            Console.WriteLine("Sun\tMon\tTue\tWed\tThu\tFri\tSat");
            Console.WriteLine("====================================================");

            int daysInMonth = DateTime.DaysInMonth(year, month);
           
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
    }
}
