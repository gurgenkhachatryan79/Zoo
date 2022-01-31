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
            Dolphin dolphin2 = new Dolphin(1, "Del2", Gender.Female, new DateTime(2020, 10, 10), dolphinCage);
            Dolphin dolphin3 = new Dolphin(1, "Del3", Gender.Male, new DateTime(2020, 10, 10), dolphinCage);
            Eagle eagle1 = new Eagle(2, "Eag1", Gender.Male, new DateTime(2010, 07, 10), eagleCage);
            Eagle eagle2 = new Eagle(2, "Eag2", Gender.Female, new DateTime(2010, 07, 10), eagleCage);
            Eagle eagle3 = new Eagle(2, "Eag3", Gender.Male, new DateTime(2010, 07, 10), eagleCage);
            Elephant elephant1 = new Elephant(3, "Elep1", Gender.Female, new DateTime(2020, 05, 06), elephantCage);
            Lion lion1 = new Lion(4, "Lion1", Gender.Male, new DateTime(2021, 02, 04), lionCage);
            Lion lion2 = new Lion(4, "Lion2", Gender.Female, new DateTime(2021, 02, 04), lionCage);
            Monkey monkey1 = new Monkey(5, "Monk1", Gender.Female, new DateTime(2019, 03, 07), monkeyCage);
            Monkey monkey2 = new Monkey(5, "Monk2", Gender.Female, new DateTime(2019, 03, 07), monkeyCage);

            List<Animal> animalslist = new List<Animal>() { dolphin1, dolphin2, dolphin3, eagle1, eagle2, eagle3, elephant1, lion1, lion2, monkey1, monkey2 };
         

            Feeder feeder = new Feeder("Gurgen", new DateTime(2000, 10, 10), Gender.Male, animalslist, cages);
            foreach (var item in animalslist)
            {
                feeder.PutAnimalCage(item);
            }
        }
    }
}
