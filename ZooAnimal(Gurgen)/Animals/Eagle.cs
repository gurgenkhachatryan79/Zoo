using System;
using System.Collections.Generic;
using ZooAnimal_Gurgen_.AnimalInterfaces;
using ZooAnimal_Gurgen_.Cages;
using ZooAnimal_Gurgen_.Foods;

namespace ZooAnimal_Gurgen_.Animals
{
    class Eagle : Animal, IFly
    {
        public Eagle(int id, string name, Gender gender, DateTime birthday,Cage cage) : base(id, name, gender, birthday,cage)
        {
            Foods = new List<Food>() { new Meat(), new Fish() };
        }

        public void Fly()
        {
            Console.WriteLine("Eagle flying");
        }

        public override void Voice()
        {
            Console.WriteLine("Eagle voice");
        }
    }
}
