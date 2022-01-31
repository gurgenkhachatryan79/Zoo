using System.Collections.Generic;
using ZooAnimal_Gurgen_.Animals;

namespace ZooAnimal_Gurgen_.Cages
{
    abstract class Cage
    {
        public List<Animal> animallist;
        public int Id { get; set; }
        public int Area { get; set; }
        public bool IsClosed { get; set; }
        public bool IsLightOn { get; set; }
        public bool IsEmpty { set; get; }
        public FoodPlate _FoodPlate;

        public Cage(int id, int area)
        {
            Id = id;
            Area = area;
            IsClosed = true;
            IsLightOn = false;
            IsEmpty = true;
            animallist = new List<Animal>();
            _FoodPlate = new FoodPlate();
        }
    }
}
