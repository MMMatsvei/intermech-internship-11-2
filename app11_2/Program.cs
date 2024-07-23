class Program
{
    static object lockObject = new object();
    static string outputFile = "../../../file4.txt";

    static void Main(string[] args)
    {

        string[] inputFiles = { "../../../file1.txt", "../../../file2.txt", "../../../file3.txt" };

        if (File.Exists(outputFile))
        {
            File.Delete(outputFile);
        }

        Thread[] threads = new Thread[inputFiles.Length];

        for (int i = 0; i < inputFiles.Length; i++)
        {
            int j = i;
            threads[i] = new Thread(() => ProcessFile(inputFiles[j]));
            threads[i].Start();
        }

        foreach (var thread in threads)
        {
            thread.Join();
        }

    }

    static void ProcessFile(string inputFile)
    {
        try
        {
            string text = File.ReadAllText(inputFile);

            lock (lockObject)
            {
                using (StreamWriter streamWriter = new StreamWriter(outputFile, true))
                {
                    streamWriter.WriteLine(text);
                    streamWriter.WriteLine();
                }
            }

            Console.WriteLine($"Файл {inputFile} записан.");
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
