using System;
using System.Collections.Generic;
using System.Text;

namespace ZooAnimal_Gurgen_.Log
{
    interface ILogger
    {
        void LogError(string text);
        void LogWarning(string text);
        void LogInformation(string text);
    }
}
