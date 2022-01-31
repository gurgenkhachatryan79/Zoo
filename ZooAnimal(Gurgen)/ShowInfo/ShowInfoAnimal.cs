using System;
using ZooAnimal_Gurgen_.Animals;

namespace ZooAnimal_Gurgen_.ShowInfo
{
    class ShowInfoAnimal : IAnimalPrint
    {

        public void PrintAnimal(Animal animal)
        {
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("Type=" + animal.Name + "\nMaxStomachSize=" + animal.MaxStomachSize + "\nStomachSize=" + animal.StomachSize);
            Console.WriteLine("RedBorderOfDeat=" + animal.RedBorderOfDeath);
            Console.WriteLine(new string('-', 30));
        }
    }
}
