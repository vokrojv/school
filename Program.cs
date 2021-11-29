using System;

namespace taylor
{
    class Program
    {
        static void Main(string[] args)
        {
            //Kontrola počtu vstupů
            if (args.Length < 2)
            {
                Console.WriteLine("At least 2 parametrs are needed");
                System.Environment.Exit(0);
            }


            //Načtení vstupů
            string degreeInput = args[0];
            string calculationInput = args[1];
            double inputNumber;
            int degree;

            //načtení čísla pro které se pokoušíme vypočítat TP
            if (calculationInput.Equals("PI")) { inputNumber = Math.PI; }
            else if (calculationInput.Equals("E")) { inputNumber = Math.E; }
            else
            {
                try
                {
                    inputNumber = Convert.ToDouble(calculationInput);
                }
                catch (FormatException e)
                {
                    inputNumber = 0;
                    Console.WriteLine("Second parameter is not valid number");
                    System.Environment.Exit(0);
                }
            }

            //parsování stupně polynomu
            try
            {
                degree = Int32.Parse(degreeInput);
            }
            catch (FormatException e)
            {
                degree = 0;
                Console.WriteLine("First parameter is not valid integer");
                System.Environment.Exit(0);
            }

            int a = calculateEasyNumber(inputNumber);
            double aproximation = calculateRecursively(a, degree, inputNumber);
            Console.WriteLine("Sin(" + inputNumber + ") = " + aproximation + "\nCalculated with Taylors polynom of " + degreeInput + ". degree.");
        }


        //rekurzivni fce ktera přijme na vstup číslo, ke kterému se aproximuje,
        //počet stupňů Taylorova polynomu a hodnota pro kterou se pokoušíme vypočítat TP
        //použitý vzorec pro výpočet https://math.fel.cvut.cz/mt/txtc/4/txc3ca4e.htm
        static double calculateRecursively(int easyNumberToCalculate, int degree, double input)
        {
            double a = easyNumberToCalculate * (Math.PI / 4);
            //stop case pro rekurzi
            if (degree == 0)
            {
                return Math.Sin(a);
            }
            double tmp;

            int leftover = degree % 4;
            //kterou derivaci sin(x) použít
            switch (leftover)
            {
                case 0:
                    tmp = Math.Sin(a);
                    break;
                case 1:
                    tmp = Math.Cos(a);
                    break;
                case 2:
                    tmp = -Math.Sin(a);
                    break;
                case 3:
                    tmp = -Math.Cos(a);
                    break;
                default: return 0;
            }


            //výpočet faktoriálu
            long factorial = 1;
            for (int i = degree; i > 0; i--)
            {
                factorial *= i;
            }
            //dělení faktoriálem a násobení umocněnou závorkou ze vzorce
            tmp /= factorial;
            tmp *= Math.Pow((input - a), degree);


            //vrácení výsledku pro tento stupeň polynomu + stupeň předchozí ..... 
            return tmp + calculateRecursively(easyNumberToCalculate, degree - 1, input);

        }


        // hodnota ke ktere aproximujeme
        static int calculateEasyNumber(double input)
        {
            double tmp = input / (Math.PI / 4);
            int roundedTmp = (int)Math.Round(tmp);
            return roundedTmp;


        }

    }
}
