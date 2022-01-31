using System;
using System.IO;

namespace ZooAnimal_Gurgen_.Message
{
    class MessageMeneger : IMessage
    {
        public void WriteMessage(string text)
        {
            
            //Դեռ սա չեմ փոխել
            string pathDirectory = "C://Users//Toshiba//Desktop//C#//Digitain//Level2//Week1//ZooAnimal(Gurgen)//ZooAnimal(Gurgen)//Message//ErrorMessage";
            string pathFile = pathDirectory + "//text.txt";


            DirectoryInfo directoryInfo = new DirectoryInfo(pathDirectory);
            if (!directoryInfo.Exists)
            {
                directoryInfo.Create();
            }
            try
            {
                using (StreamWriter streamWriter = new StreamWriter(pathFile, true))
                    streamWriter.WriteLine(text + " Datetime=" + DateTime.Now);
            }
            catch (DirectoryNotFoundException exd) { Console.WriteLine(exd.Message); }
            catch (FileNotFoundException ex) { Console.WriteLine(ex.Message); }
        }
    }
}
