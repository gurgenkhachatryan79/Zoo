using System;
using System.Collections.Generic;
using System.Timers;
using ZooAnimal_Gurgen_.Cages;
using ZooAnimal_Gurgen_.Foods;
using ZooAnimal_Gurgen_.Log;

namespace ZooAnimal_Gurgen_.Animals
{

    class Animal
    {
        protected int id;
        protected string name;
        private Timer _timerhungry { get; set; } = new Timer();
        protected DateTime birthday;
        protected int maxStomachSize;
        public int Id
        {
            set
            {
                if (value < 1)
                    //{ throw new ArgumentException(paramName: nameof(value), message: "Id cannot be <1"); }
                    Console.WriteLine("Id cannot be <1");
                else
                { id = value; }
            }

            get { return id; }
        }
        public string Name
        {
            set
            {
                if (value[0] < 90 && value[0] > 122)
                {
                    Console.WriteLine("invalid name");
                }
                else { name = value; }
            }
            get { return name; }
        }
        public Gender _Gender { get; set; }
        public DateTime Birthday
        {
            set
            {
                if ((value > DateTime.Now) && value.Year < (DateTime.Now.Year - 100))
                    Console.WriteLine("invalid birthday");
                else { birthday = value; }
            }
            get { return birthday; }
        }
        public int MaxStomachSize
        {
            set
            {
                if (value < 0 || value > 100)
                    Console.WriteLine("invalid maxStomachSize");
                else maxStomachSize = value;
            }
            get { return maxStomachSize; }
        }
        public List<Food> Foods { get; set; }
        public int StomachSize { get; set; }
        public int RedBorderOfDeath { get; set; }
        public bool IsLive { set; get; }
        public Logger logger;
        public int StomachSpace(DateTime birthday)
        {
            if ((DateTime.Now.Year - birthday.Year) < 5)
                return 25;
            else
                return 50;
        }
        public Cage MyCage { set; get; }

        public delegate void Deleg(Cage cage);
        public event Deleg EatEvent;

        public Animal() { }
        public Animal(int id, string name, Gender gender, DateTime birthday, Cage cage)
        {
            Id = id;
            Name = name;
            _Gender = gender;
            Birthday = birthday;
            DateTime nowtime = DateTime.Now;
            MaxStomachSize = StomachSpace(birthday);
            StomachSize = 0;
            RedBorderOfDeath = 0;
            IsLive = true;
            logger = Logger.CreateLogObject();
            MyCage = cage;

            _timerhungry.Interval = 1000;
            _timerhungry.Elapsed += _timerhungry_Elapsed;
            _timerhungry.Start();
        }

        private void _timerhungry_Elapsed(object sender, ElapsedEventArgs e)
        {
            QuenchHunger();
        }

        public virtual void Voice() { }

        AnimalStatus AnimalEatFoodOrNot(Cage cage, List<Food> cagefoods)
        {
            foreach (var item in cagefoods)
            {
                for (int i = 0; i < cage.animallist[0].Foods.Count; i++)
                {
                    if (item.GetType().Name == cage.animallist[0].Foods[i].GetType().Name)
                    {
                        logger.LogInformation(item.GetType().Name + " it's my food Thank you,I will eat ");
                        return AnimalStatus.EatTheFood;
                    }
                }
            }
            return AnimalStatus.DidnotEat;
        }

        void MyLikeFoods(Cage cage)
        {
            foreach (var item in cage.animallist[0].Foods)
            {
                //Console.WriteLine(item.GetType().Name);
                logger.LogInformation(item.GetType().Name);
            }
            Console.WriteLine(new string('-', 30));
        }

        public void GoesFoodPlate(Cage cage)
        {
            EatEvent = null;
            EatEvent += AnimalGoesToTheFoodPlate;
            EatEvent += CanEat;
            EatEvent += AnimalToLeave;
            EatEvent(cage);
        }

        public void AnimalGoesToTheDoor(Cage cage)
        {
            foreach (var item in cage.animallist)
            {
                if (item.Died() != AnimalStatus.IsDied && cage.animallist != null)
                {
                    Console.WriteLine(item.Name + " goes to the door");
                    logger.LogInformation(item.Name + " goes to the door");
                }
            }
        }

        public void AnimalGoesToTheFoodPlate(Cage cage)
        {
            foreach (var item in cage.animallist)
            {
                if (item.Died() != AnimalStatus.IsDied && cage.animallist != null)
                { Console.WriteLine(item.Name + " goes to the FoodPlate"); }
                logger.LogInformation(item.Name + " goes to the FoodPlate");
            }
        }

        public void AnimalToLeave(Cage cage)
        {
            foreach (var item in cage.animallist)
            {
                if (item.Died() != AnimalStatus.IsDied && cage.animallist != null)
                { Console.WriteLine(item.GetType().Name + "  to leave"); }
                logger.LogInformation(item.GetType().Name + "  to leave");
            }
        }

        public virtual void CanEat(Cage cage)
        {
            if (AnimalEatFoodOrNot(cage, cage._FoodPlate.Foods) == AnimalStatus.DidnotEat)
            {
                if (cage.IsEmpty) { Console.WriteLine("cage is empty"); }
                else
                {
                    Console.WriteLine(GetType().Name + "   This is not my food ,I do not eat it, I like to eat");
                    logger.LogWarning("This is not my food ,I do not eat it, I like to eat");
                    MyLikeFoods(cage);
                }
            }
            else
            {
                foreach (var item in cage.animallist)
                {
                    if ((cage._FoodPlate.FoodCount + item.StomachSize * cage.animallist.Count) >= item.MaxStomachSize * cage.animallist.Count)
                    {
                        cage._FoodPlate.FoodCount = (cage._FoodPlate.FoodCount + item.StomachSize * cage.animallist.Count) - item.MaxStomachSize * cage.animallist.Count;
                        item.StomachSize = item.MaxStomachSize;
                    }
                    else
                    {
                        item.StomachSize += cage._FoodPlate.FoodCount;
                        item.RedBorderOfDeath = 0;
                        cage._FoodPlate.FoodCount = 0;
                    }
                }
            }
        }

        public AnimalStatus Died()
        {
            if (RedBorderOfDeath < -MaxStomachSize * 0.3)
            {
                logger.LogError("The " + name + " is a Dead");
                IsLive = false;
                return AnimalStatus.IsDied;
            }
            else
            { IsLive = true; }
            return AnimalStatus.Isalive;
        }

        public void QuenchHunger()
        {
            if (Died() == AnimalStatus.Isalive)
            {
                if (StomachSize == 0)
                {
                    RedBorderOfDeath--;
                    logger.LogWarning(name + " The animal is completely hungry and may die");
                }
                else
                {
                    RedBorderOfDeath = 0;
                    if (MyCage._FoodPlate.FoodCount > 0)
                        MyCage._FoodPlate.FoodCount--;
                    else
                        StomachSize--;
                }
            }
        }
    }
}
