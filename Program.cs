class Program
{
    static void Main(string[] args)
    {
        if (args.Length > 0)
        {
            string input = args[0];
            Console.WriteLine("Argument received: " + input);
        }
        else
        {
            Console.WriteLine("No arguments received.");
        }
    }
}