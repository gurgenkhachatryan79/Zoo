using System.Collections.Generic;
using ZooAnimal_Gurgen_.Foods;

namespace ZooAnimal_Gurgen_
{
    
    class FoodPlate
    {
        public List<Food> Foods;
        public int FoodCount;

        public FoodPlate()
        {
            Foods = new List<Food>();
            FoodCount = 0;
        }

    }
}
