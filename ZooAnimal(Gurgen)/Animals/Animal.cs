using System;
using System.Collections.Generic;
using System.Timers;
using ZooAnimal_Gurgen_.Cages;
using ZooAnimal_Gurgen_.Foods;
using ZooAnimal_Gurgen_.Log;
using ZooAnimal_Gurgen_.Attributes;

namespace ZooAnimal_Gurgen_.Animals
{
    [Validation(0, Id = 0)]
    class Animal
    {
        //  protected int id;
        protected string name;
        private Timer _timerhungry { get; set; } = new Timer();
        protected DateTime birthday;
        protected int maxStomachSize;
        /*  public int Id
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
          }*/
        public int Id { get; set; }
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

        public event EventHandler<AnimalEventArgs> EatEvent;

        public Animal() { }
        public Animal(int id, string name, Gender gender, DateTime birthday, Cage cage)
        {
            if (new IdValidation().Validation(id))
            {
                Id = id;
            }
            else { Id = 0; }

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
                        new Logger().LogInformation(item.GetType().Name + " it's my food Thank you,I will eat ");
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
                new Logger().LogInformation(item.GetType().Name);
            }
            Console.WriteLine(new string('-', 30));
        }

        public void OnGoesFoodPlate(Cage cage)
        {
            EatEvent += AnimalGoesToTheFoodPlate;
            EatEvent += CanEat;
            EatEvent += AnimalToLeave;
            AnimalEventArgs e = new AnimalEventArgs();
            e.cage = cage;
            if (EatEvent != null)
            {
                EatEvent(this, e);
            }
        }

        public void AnimalGoesToTheDoor(object sender, FeederEventArgs e)
        {
            foreach (var item in e.cage.animallist)
            {
                if (item.Died() != AnimalStatus.IsDied && e.cage.animallist != null)
                {
                    Console.WriteLine(item.Name + " goes to the door");
                    new Logger().LogInformation(item.Name + " goes to the door");
                }
            }
        }

        public void AnimalGoesToTheFoodPlate(object sender, AnimalEventArgs e)
        {
            foreach (var item in e.cage.animallist)
            {
                if (item.Died() != AnimalStatus.IsDied && e.cage.animallist != null)
                { Console.WriteLine(item.Name + " goes to the FoodPlate"); }
                new Logger().LogInformation(item.Name + " goes to the FoodPlate");
            }
        }

        public void AnimalToLeave(object sender, AnimalEventArgs e)
        {
            foreach (var item in e.cage.animallist)
            {
                if (item.Died() != AnimalStatus.IsDied && e.cage.animallist != null)
                { Console.WriteLine(item.GetType().Name + "  to leave"); }
                new Logger().LogInformation(item.GetType().Name + "  to leave");
            }
        }

        public virtual void CanEat(object sender, AnimalEventArgs e)
        {
            if (AnimalEatFoodOrNot(e.cage, e.cage._FoodPlate.Foods) == AnimalStatus.DidnotEat)
            {
                if (e.cage.IsEmpty) { Console.WriteLine("cage is empty"); }
                else
                {
                    Console.WriteLine(GetType().Name + "   This is not my food ,I do not eat it, I like to eat");
                    new Logger().LogWarning("This is not my food ,I do not eat it, I like to eat");
                    MyLikeFoods(e.cage);
                }
            }
            else
            {
                foreach (var item in e.cage.animallist)
                {
                    if ((e.cage._FoodPlate.FoodCount + item.StomachSize * e.cage.animallist.Count) >= item.MaxStomachSize * e.cage.animallist.Count)
                    {
                        e.cage._FoodPlate.FoodCount = (e.cage._FoodPlate.FoodCount + item.StomachSize * e.cage.animallist.Count) - item.MaxStomachSize * e.cage.animallist.Count;
                        item.StomachSize = item.MaxStomachSize;
                    }
                    else
                    {
                        item.StomachSize += e.cage._FoodPlate.FoodCount;
                        item.RedBorderOfDeath = 0;
                        e.cage._FoodPlate.FoodCount = 0;
                    }
                }
            }
        }

        public AnimalStatus Died()
        {
            if (RedBorderOfDeath < -MaxStomachSize * 0.3)
            {
                new Logger().LogError("The " + name + " is a Dead");
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
                    new Logger().LogWarning(name + " The animal is completely hungry and may die");
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
