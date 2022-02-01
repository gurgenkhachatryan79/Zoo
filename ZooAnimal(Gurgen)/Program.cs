using System;


namespace ZooAnimal_Gurgen_
{
    class Program
    {       
        static void Main(string[] args)
        {
            new Reservoir().CreateCages();
            string input;
            do
            {
                input = Console.ReadLine();
            }
            while (input != "e");
        }
    }
}
