using System;
using System.Collections.Generic;
using System.Timers;
using ZooAnimal_Gurgen_.Attributes;
using ZooAnimal_Gurgen_.Animals;
using ZooAnimal_Gurgen_.Cages;
using ZooAnimal_Gurgen_.Foods;
using ZooAnimal_Gurgen_.ShowInfo;
using System.Linq;

namespace ZooAnimal_Gurgen_
{
   [Validation(30)]
    class Feeder
    {
        string _name;
        List<Animal> _animals;
        List<Cage> _cages;
        Timer _timerfeeding { get; set; } = new Timer();
        Timer _timerprint { get; set; } = new Timer();
        public string Name
        {
            set
            {
                if (value[0] < 90 && value[0] > 122)
                {
                    Console.WriteLine("invalid name");
                }
                else { _name = value; }
            }
            get { return _name; }
        }
        public Gender _Gender { set; get; }
        public int Age { set; get; }

        public Feeder() { }
        public Feeder(string name,int age, Gender gender, List<Animal> animals, List<Cage> cages)
        {
            _name = name;
            if (new AgeValidation().Validation(age))
            { Age = age; }
            else
            {
                Age = 0;
            }
            _Gender = gender;
            _animals = animals;
            _cages = cages;

            _timerfeeding.Interval = 7000;
            _timerfeeding.Elapsed += _timerfeeding_Elapsed;
            _timerfeeding.Start();

            _timerprint.Interval = 3000;
            _timerprint.Elapsed += _timerprint_Elapsed;
            _timerprint.Start();
        }

        public delegate void Deleg(Cage cage);
        public event Deleg EventGoToFeed;
        public event Deleg EventLeaveTheCage;

        public void PutAnimalCage(Animal animal)
        {
            for (int i = 0; i < _cages.Count; i++)
            {
                if (_cages[i].Id == animal.Id)
                {
                    _cages[i].animallist.Add(animal); _cages[i].IsEmpty = false;
                }
            }
        }

        private void _timerfeeding_Elapsed(object sender, ElapsedEventArgs e)
        {
            for (int i = 0; i < 2; i++)
            {
                GoesTheCage(_cages[i]);
                new ShowInfoCage().PrintCage(_cages[i]);

                new Animal().GoesFoodPlate(_cages[i]);

                LeaveTheCage(_cages[i]);
                new ShowInfoCage().PrintCage(_cages[i]);

                new ShowInfoAnimal().PrintAnimal(_cages[i].animallist[0]);
            }
        }

        private void _timerprint_Elapsed(object sender, ElapsedEventArgs e)
        {
            for (int i = 0; i < 2; i++)
            {
                new ShowInfoAnimal().PrintAnimal(_cages[i].animallist[0]);
            }
        }

        public void LeaveTheCage(Cage cage)
        {
            EventLeaveTheCage = null;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Feeder leave " + cage.GetType().Name);
            Console.ResetColor();
            EventLeaveTheCage += TurnOnLight;
            EventLeaveTheCage += OpenTheDoor;
            EventLeaveTheCage(cage);
        }

        public void GoesTheCage(Cage cage)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Feeder goes " + cage.GetType().Name);
            Console.ResetColor();

            EventGoToFeed = null;
            EventGoToFeed += OpenTheDoor;
            EventGoToFeed += TurnOnLight;
            EventGoToFeed += new Animal().AnimalGoesToTheDoor;
            EventGoToFeed += PutFeedCage;
            EventGoToFeed(cage);
        }

        public void OpenTheDoor(Cage cage)
        {
            if (cage.IsClosed) { cage.IsClosed = false; }
            else { cage.IsClosed = true; }
        }

        public void TurnOnLight(Cage cage)
        {
            if (cage.IsLightOn) { cage.IsLightOn = false; }
            else { cage.IsLightOn = true; }
        }

        public void PutFeedCage(Cage item)
        {
            Console.WriteLine(new string('*', 30));
            if (!item.IsEmpty)
            {
                int lallStomachSize = CageAllAnimalStomachSize(item);
                if (item.animallist[0].RedBorderOfDeath < 0)
                {
                    PutFoodPlate(item, item.animallist[0].Foods[0], lallStomachSize);
                }
            }
        }

        public void PutFoodPlate(Cage cage, Food food, int foodcount)
        {
            cage._FoodPlate.Foods.Add(food);
            cage._FoodPlate.FoodCount += foodcount;
        }

        public int CageAllAnimalStomachSize(Cage cage)
        {
            int lresult = 0;
            foreach (var item in cage.animallist)
            {
                lresult += item.MaxStomachSize;
            }
            return lresult;
        }

       
    }
}
