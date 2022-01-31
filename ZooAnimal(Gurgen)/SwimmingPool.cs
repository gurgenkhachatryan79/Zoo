namespace ZooAnimal_Gurgen_
{
    class SwimmingPool
    {
        public int Area { get; set; }
        public bool isEmpty { get; set; }

        public SwimmingPool(int area)
        {
            Area = area;
            isEmpty = false;
        }
    }
}
