using System;
using ZooAnimal_Gurgen_.Cages;

namespace ZooAnimal_Gurgen_.ShowInfo
{
    class ShowInfoCage : ICagePrint
    {
        public void PrintCage(Cage item)
        {
            Console.WriteLine("CageType=" + item.GetType().Name + "\n-----------" + "\nCageID=" + item.Id + "\nIsEmpty=" + item.IsEmpty);
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("CageIsclosed=" + item.IsClosed + "\nIsLightOn=" + item.IsLightOn);
            Console.Write("FoodPlate=");
            foreach (var itemfood in item._FoodPlate.Foods)
            {
                Console.Write(itemfood.GetType().Name + " ");
            }
            Console.WriteLine("AllCount=" + item._FoodPlate.FoodCount);
            Console.ResetColor();

            for (int i = 0; i < item.animallist.Count; i++)
            {
                Console.WriteLine(item.animallist[i].Name);
            }
            Console.WriteLine(new string('*', 30));

        }
    }
}
