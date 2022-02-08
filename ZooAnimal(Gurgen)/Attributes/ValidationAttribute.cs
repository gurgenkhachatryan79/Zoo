using System;
using System.Collections.Generic;
using System.Text;

namespace ZooAnimal_Gurgen_.Attributes
{
    [AttributeUsage(AttributeTargets.All,AllowMultiple =false)]
    class ValidationAttribute:System.Attribute
    {
        public int Age { set; get; }
        public ValidationAttribute(int age)
        {
            Age= age;
        }
        public int Id { set; get; }
    }
}
