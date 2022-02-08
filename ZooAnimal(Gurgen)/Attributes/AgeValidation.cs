using System;
using System.Collections.Generic;
using System.Text;

namespace ZooAnimal_Gurgen_.Attributes
{

    class AgeValidation:IValidation
    {
        public bool Validation(int age)
        {
            Type type = typeof(Feeder);
            Object[] attributes = type.GetCustomAttributes(false);
            foreach (ValidationAttribute item in attributes)
            {
                if (age >= item.Age)
                {
                    return true;
                }
                else { return false; }

            }
            return true;
        }
    }
}
