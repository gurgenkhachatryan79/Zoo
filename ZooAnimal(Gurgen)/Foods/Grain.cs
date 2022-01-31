using System;

namespace ZooAnimal_Gurgen_.Foods
{
    class Grain : Food
    {
        public Grain() { }
        public Grain(int count, DateTime expirationDate) : base(count, expirationDate)
        { }
    }
}
