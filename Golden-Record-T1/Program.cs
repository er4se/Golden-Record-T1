using System;
using System.IO;
using System.Linq;

class Program
{
    static void Main()
    {
        // Путь к папке файла
        string filePath;
        string inputFilePath;

        Console.Write(@"Введите путь к файлу БД: ");
        filePath = Console.ReadLine() + string.Empty; 
        Console.WriteLine();

        // Путь к исходному файлу
        inputFilePath = filePath + @"\ds_dirty_fin_202410041147.csv";

        // Путь к результативному файлу
        string outputFilePath = filePath + @"\output.csv";

        try
        {
            // Считывание всех строк из входного файла
            string[] lines = File.ReadAllLines(inputFilePath);

            // Проверка наличия хотя бы одной строки
            if (lines.Length > 0)
            {
                // Получение первой строки (заголовок)
                string header = lines[0];

                // Разделение заголовка на отдельные поля
                string[] fields = header.Split(',');

                // Если количество полей больше или равно 47, выбираем первые 47
                int columnCount = Math.Min(fields.Length, 47);

                // Открытие потока для записи в выходной файл
                using (StreamWriter writer = new StreamWriter(outputFilePath))
                {
                    // Записать заголовок
                    writer.WriteLine(String.Join(",", fields.Take(columnCount)));

                    // Обработка остальных строк
                    foreach (string line in lines.Skip(1))
                    {
                        // Разделить строку на поля
                        fields = line.Split(',');

                        // Записать первые 47 полей
                        writer.WriteLine(String.Join(",", fields.Take(columnCount)));
                    }
                }

                Console.WriteLine($"Файл {outputFilePath} успешно создан.");
            }
            else
            {
                Console.WriteLine("Исходный файл пуст.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при обработке файла: {ex.Message}");
        }
    }
}