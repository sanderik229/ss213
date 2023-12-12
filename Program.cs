using System;
using System.Diagnostics;
using System.IO;

public class TaskManager
{
    public static void ShowFolderContents(string path)
    {
        try
        {
            while (true)
            {
                string[] directories = Directory.GetDirectories(path);
                string[] files = Directory.GetFiles(path);

                Console.Clear();

                foreach (string directory in directories)
                {
                    DirectoryInfo directoryInfo = new DirectoryInfo(directory);
                    Console.WriteLine($"{directory}          {directoryInfo.CreationTime}          {directoryInfo.Extension}");
                }

                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    Console.WriteLine($"{file}          {fileInfo.CreationTime}          {fileInfo.Extension}");
                }

                DriveInfo driveInfo = new DriveInfo(Path.GetPathRoot(path));
                Console.WriteLine($"Свободное место на диске: {driveInfo.AvailableFreeSpace / (1024 * 1024 * 1024)} GB");
                Console.WriteLine($"Всего места на диске: {driveInfo.TotalSize / (1024 * 1024 * 1024)} GB");

                int position = ArrowMenu.ShowMenu(0, directories.Length + files.Length);

                if (position == -1)
                {
                    return;
                }
                else if (position < directories.Length)
                {
                    ShowFolderContents(directories[position]);
                }
                else
                {
                    Process.Start(new ProcessStartInfo { FileName = files[position - directories.Length], UseShellExecute = true });
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("An error occurred: " + ex.Message);
        }
        finally
        {
            // Финальные действия, если необходимо
        }
    }
}

public class ArrowMenu
{
    public static int ShowMenu(int start, int end)
    {
        int currentPosition = start;
        while (true)
        {
            Console.Clear();
            for (int i = start; i < end; i++)
            {
                if (i == currentPosition)
                {
                    Console.Write("> ");
                }
                else
                {
                    Console.Write("  ");
                }
                Console.WriteLine($"Item {i}"); // Замените на свои пункты меню
            }

            ConsoleKeyInfo keyInfo = Console.ReadKey();
            if (keyInfo.Key == ConsoleKey.UpArrow)
            {
                currentPosition = Math.Max(start, currentPosition - 1);
            }
            else if (keyInfo.Key == ConsoleKey.DownArrow)
            {
                currentPosition = Math.Min(end - 1, currentPosition + 1);
            }
            else if (keyInfo.Key == ConsoleKey.Enter)
            {
                return currentPosition;
            }
        }
    }
}

class Program
{
    static void Main()
    {
        TaskManager.ShowFolderContents("/");
    }
}
