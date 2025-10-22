using System;
using System.IO;


namespace Bai02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int isContinue;
            string oldDir = "";
            string mainDir = "";
            do
            {
                Console.Write("Nhap duong dan thu muc: " + mainDir);
                //Đường dẫn tương đối từ vị trí trước đó
                string dir = Console.ReadLine();

                //Cập nhật đường dẫn chính
                if (!string.IsNullOrWhiteSpace(dir))
                {
                    oldDir = mainDir;
                    if (dir[dir.Length - 1] == '/')
                    {
                        mainDir += dir;
                    }
                    else
                    {
                        mainDir += dir + "/";
                    }
                }
                
                //Nếu tìm thấy thư mục
                if (Directory.Exists(mainDir))
                {
                    DirectoryInfo di = new DirectoryInfo(mainDir);
                    DirectoryInfo[] subDirs = di.GetDirectories();
                    FileInfo[] files = di.GetFiles();

                    foreach (DirectoryInfo dirInfo in subDirs)
                    {
                        Console.WriteLine("{0}\t<DIR>\t{1}",
                            dirInfo.LastWriteTime.ToString("dd/MM/yyyy  HH:mm"),
                            dirInfo.Name);
                    }

                    foreach (FileInfo file in files)
                    {
                        Console.WriteLine("{0}\t{1}\t{2}",
                            file.LastWriteTime.ToString("dd/MM/yyyy  HH:mm"),
                            file.Length,
                            file.Name);
                    }
                }
                else
                {
                    Console.WriteLine("Khong thay thu muc");
                    mainDir = oldDir;
                }

                //Tiếp tục tìm hoặc thoát chương trình
                Console.WriteLine("Tiep tuc?\n" +
                    "[1]: Yes\t[0]: No");
                isContinue = int.Parse(Console.ReadLine());
            }
            while (isContinue > 0);
        }
    }
}
