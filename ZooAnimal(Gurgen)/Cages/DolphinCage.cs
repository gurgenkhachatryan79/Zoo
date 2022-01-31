namespace ZooAnimal_Gurgen_.Cages
{
    class DolphinCage : Cage
    {
        SwimmingPool _SwimmingPool;
        public DolphinCage(int id, int area, SwimmingPool swimmingPool) : base(id, area)
        {
            _SwimmingPool = swimmingPool;          
        }
    }
}
