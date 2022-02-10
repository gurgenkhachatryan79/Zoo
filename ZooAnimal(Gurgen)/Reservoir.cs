using System;
using System.Collections.Generic;
using ZooAnimal_Gurgen_.Animals;
using ZooAnimal_Gurgen_.Cages;
using ZooAnimal_Gurgen_.ShowInfo;

namespace ZooAnimal_Gurgen_
{
    class Reservoir
    {

        public void CreateCages()
        {
            DolphinCage dolphinCage = new DolphinCage(1, 100, new SwimmingPool(80));
            EagleCage eagleCage = new EagleCage(2, 150);
            ElephantCage elephantCage = new ElephantCage(3, 500);
            LionCage lionCage = new LionCage(4, 400);
            MonkeyCage monkeyCage = new MonkeyCage(5, 50);
            List<Cage> cages = new List<Cage>() { dolphinCage, eagleCage, elephantCage, lionCage, monkeyCage };

            Dolphin dolphin1 = new Dolphin(1, "Del1", Gender.Female, new DateTime(2020, 10, 10), dolphinCage);
            Dolphin dolphin2 = new Dolphin(2, "Del2", Gender.Female, new DateTime(2000, 10, 10), dolphinCage);
            Dolphin dolphin3 = new Dolphin(3, "Del3", Gender.Male, new DateTime(2020, 10, 10), dolphinCage);
            Eagle eagle1 = new Eagle(4, "Eag1", Gender.Male, new DateTime(2020, 07, 10), eagleCage);
            Eagle eagle2 = new Eagle(5, "Eag2", Gender.Female, new DateTime(2020, 07, 10), eagleCage);
            Eagle eagle3 = new Eagle(6, "Eag3", Gender.Male, new DateTime(2020, 07, 10), eagleCage);
            Elephant elephant1 = new Elephant(7, "Elep1", Gender.Female, new DateTime(2020, 05, 06), elephantCage);
            Lion lion1 = new Lion(8, "Lion1", Gender.Male, new DateTime(2021, 02, 04), lionCage);
            Lion lion2 = new Lion(9, "Lion2", Gender.Female, new DateTime(2021, 02, 04), lionCage);
            Monkey monkey1 = new Monkey(10, "Monk1", Gender.Female, new DateTime(2019, 03, 07), monkeyCage);
            Monkey monkey2 = new Monkey(11, "Monk2", Gender.Female, new DateTime(2019, 03, 07), monkeyCage);
            List<Animal> animalslist = new List<Animal>() { dolphin1, dolphin2, dolphin3, eagle1, eagle2, eagle3, elephant1, lion1, lion2, monkey1, monkey2 };


            Feeder feeder = new Feeder("Gurgen", 50, Gender.Male, animalslist, cages);

            foreach (var item in animalslist)
            {
                feeder.PutAnimalCage(item);
            }
            foreach (var item in cages)
            {
                new ShowInfoCage().PrintCage(item);
            }
        }

        public static void RemoveAnimal(Cage cage, int id)
        {
            for (int i = 0; i < cage.animallist.Count - 1; i++)
            {
                if (id == cage.animallist[i].Id)
                {
                    cage.animallist.Remove(cage.animallist[i]);
                    break;
                }
            }
        }
    }
}
