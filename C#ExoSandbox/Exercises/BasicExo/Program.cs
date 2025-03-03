using System;

namespace BasicExo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Calculate the integer division, modulo, and division of two integers.
            //Expected result for 5 and 2 → Integer division: 2, Modulo: 1, Division: 2.5.
            string result = DivideIntegers(5, 2);
            Console.WriteLine(result);

        }

        static string DivideIntegers(int a, int b)
        {
            float result = (float)a / b;


            return $"Interger division : {a / b} , Modulo : {a % b} , Division : {result}";
        }
    }
}