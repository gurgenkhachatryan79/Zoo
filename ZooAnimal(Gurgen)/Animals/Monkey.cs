using System;
using System.Collections.Generic;
using ZooAnimal_Gurgen_.Cages;
using ZooAnimal_Gurgen_.Foods;

namespace ZooAnimal_Gurgen_.Animals
{
    class Monkey : Animal
    {
        public Monkey(int id, string name, Gender gender, DateTime birthday, Cage cage) : base(id, name, gender, birthday, cage)
        {
            this.Foods = new List<Food>() { new Banana(), new Grain(), new Fish() };
        }

        public override void Voice()
        {
            Console.WriteLine("Monkey voice");
        }
    }
}
