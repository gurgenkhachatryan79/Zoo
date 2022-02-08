using System;
using System.Collections.Generic;
using System.Text;
using ZooAnimal_Gurgen_.Animals;

namespace ZooAnimal_Gurgen_.Attributes
{
    class IdValidation : IValidation
    {
        public bool Validation(int input)
        {
            Type type = typeof(Animal);
            Object[] attributes = type.GetCustomAttributes(false);
            foreach (ValidationAttribute item in attributes)
            {
                if (input >item.Id)
                {
                    return true;
                }
                else { return false; }

            }
            return true;
        }

        
    }
}
