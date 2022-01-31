using System;
using System.Collections.Generic;
using ZooAnimal_Gurgen_.AnimalInterfaces;
using ZooAnimal_Gurgen_.Cages;
using ZooAnimal_Gurgen_.Foods;

namespace ZooAnimal_Gurgen_.Animals
{
    class Dolphin : Animal, ISwim
    {
        public Dolphin(int id, string name, Gender gender, DateTime birthday,Cage cage ) : base(id, name, gender, birthday,cage)
        {
           Foods = new List<Food> { new Fish(), new Squid() };
        }

        public void Swim()
        {
            Console.WriteLine("Dolphin swiming");
        }

        public override void Voice()
        {
            Console.WriteLine("Dolphin voice");
        }
    }
}
