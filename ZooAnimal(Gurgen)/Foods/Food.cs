using System;

namespace ZooAnimal_Gurgen_.Foods
{
  abstract class Food
    {
        public int Count { set; get ; }
        public DateTime ExpirationDate { set; get; }
        public Food() { }
        public Food(int count,DateTime expirationDate)
        {
            Count = count;
            ExpirationDate = expirationDate;
        }
    }
}
