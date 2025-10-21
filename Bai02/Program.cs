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
                string dir = Console.ReadLine();

                // Nếu nhập không trống thì cập nhật mainDir
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

                Console.WriteLine("Tiep tuc?\n" +
                    "[1]: Yes\t[0]: No");
                isContinue = int.Parse(Console.ReadLine());
            }
            while (isContinue > 0);
        }
    }
}
