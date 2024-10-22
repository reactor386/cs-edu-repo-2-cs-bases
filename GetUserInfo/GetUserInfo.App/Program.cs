// -
using System;
using System.Text;

namespace GetUserInfo
{
/*
Необходимо создать метод, который заполняет данные с клавиатуры по пользователю (возвращает кортеж):
        Имя;
        Фамилия;
        Возраст;
        Наличие питомца;
        Если питомец есть, то запросить количество питомцев;
        Если питомец есть, вызвать метод, принимающий на вход количество питомцев и возвращающий массив их кличек (заполнение с клавиатуры);
        Запросить количество любимых цветов;
        Вызвать метод, который возвращает массив любимых цветов по их количеству (заполнение с клавиатуры);
        Сделать проверку, ввёл ли пользователь корректные числа: возраст, количество питомцев, количество цветов в отдельном методе;
        Требуется проверка корректного ввода значений и повтор ввода, если ввод некорректен;
        Корректный ввод: ввод числа типа int больше 0

Метод, который принимает кортеж из предыдущего шага и показывает на экран данные

Вызов методов из метода Main
*/

    internal class Program
    {
        private static int Main (string[] args)
        {
            Console.WriteLine("Hello, Username!");

            (string firstName, string lastName, int age, bool havePet) anketa;
            string[] petNames = [];
            string [] colorNames;

            do
            {
                anketa = GetGeneralDataFromConsole();
            } while (!CheckoutData(anketa.age, 3, "You have to be older"));

            if (anketa.havePet)
            {
                int nPets;
                do
                {
                    nPets = GetPetCountFromConsole();
                    if (nPets == 0)
                    {
                        anketa.havePet = false;
                    }
                } while (!CheckoutData(nPets, 1, "You have to have at least one pet"));
                petNames = GetPetNameFromConsole(nPets);
            }

            int nColors;
            do
            {
                nColors = GetColorCountFromConsole();
            } while (!CheckoutData(nColors, 2, "You have to like colors"));
            colorNames = GetColorNameFromConsole(nColors);

            Console.WriteLine("---");
            Console.WriteLine("First Name: {0}", anketa.firstName);
            Console.WriteLine("Last Name: {0}", anketa.lastName);
            Console.WriteLine("Age: {0}", anketa.age);
            Console.WriteLine("Have a Pet: {0}", anketa.havePet ? "yes" : "no");

            for (int i =0; i < petNames.Length; i++)
            {
                Console.WriteLine("Your pet #{0} has a name: {1}", i + 1, petNames[i]);
            }

            Console.WriteLine("Prefer colors: {0}", (colorNames.Length > 0) ? colorNames.Length : "no");

            for (int i =0; i < colorNames.Length; i++)
            {
                ConsoleColor prevBackground = Console.BackgroundColor;
                ConsoleColor prevForeground = Console.ForegroundColor;
                switch (colorNames[i])
                {
                    case "red":
                        Console.BackgroundColor = ConsoleColor.Red;
                        Console.ForegroundColor = ConsoleColor.Black;
                        break;
                    case "yellow":
                        Console.BackgroundColor = ConsoleColor.Yellow;
                        Console.ForegroundColor = ConsoleColor.Black;
                        break;
                    case "green":
                        Console.BackgroundColor = ConsoleColor.Green;
                        Console.ForegroundColor = ConsoleColor.Black;
                        break;
                    default:
                        Console.BackgroundColor = ConsoleColor.Gray;
                        Console.ForegroundColor = ConsoleColor.Black;
                        break;
                }
                Console.Write("Your favorite color #{0} is: {1}", i + 1, colorNames[i]);
                Console.BackgroundColor = prevBackground;
                Console.ForegroundColor = prevForeground;
                Console.Write("\n");
            }

            Console.WriteLine("---");
            Console.WriteLine("Bye! ");

            Console.ReadKey();
            return 0;
        }


        // ask a user for general data
        private static (string firstName, string lastName, int age, bool havePet) GetGeneralDataFromConsole()
        {
            (string? firstName, string? lastName, int age, bool havePet) anketa
                = (firstName: string.Empty, lastName: string.Empty, age: 0, havePet: false);
            
            Console.WriteLine("Enter the First Name: ");
            anketa.firstName = Console.ReadLine();
            anketa.firstName = (anketa.firstName is null) ? string.Empty : anketa.firstName;
            anketa.firstName = (anketa.firstName == string.Empty) ? "{nothing}" : anketa.firstName;
            
            Console.WriteLine("Enter the Last Name: ");
            anketa.lastName = Console.ReadLine();
            anketa.lastName = (anketa.lastName is null) ? string.Empty : anketa.lastName;
            anketa.lastName = (anketa.lastName == string.Empty) ? "{nothing}" : anketa.lastName;

            bool isCorrect;

            do
            {
                Console.WriteLine("Enter the Age: ");
                isCorrect = int.TryParse(Console.ReadLine(), out anketa.age);
                isCorrect = isCorrect && (anketa.age > 0);
                if (!isCorrect)
                    Console.WriteLine($"error: the number (1 - {int.MaxValue}) is required ");
            } while (!isCorrect);
            
            do
            {
                Console.WriteLine("Do you have a pet? ");
                string? havePetAnswer = Console.ReadLine();
                isCorrect = bool.TryParse(havePetAnswer, out anketa.havePet);
                if (!isCorrect)
                {
                    havePetAnswer = (havePetAnswer is null) ? string.Empty : havePetAnswer;
                    havePetAnswer = havePetAnswer.ToLower();

                    switch (havePetAnswer)
                    {
                        case "yes":
                        case "y":
                        case "да":
                        case "д":
                            anketa.havePet = true;
                            isCorrect = true;
                            break;
                        case "no":
                        case "n":
                        case "нет":
                        case "н":
                            anketa.havePet = false;
                            isCorrect = true;
                            break;
                    }
                }
                if (!isCorrect)
                    Console.WriteLine("error: Yes (Да) or No (Нет) is required ");
            } while (!isCorrect);
            
            return (anketa.firstName, anketa.lastName, anketa.age, anketa.havePet);
        }


        private static bool CheckoutData(int intValueToCheck, int intMinValue, string msg)
        {
            bool res = true;

            if (intValueToCheck < intMinValue)
            {
                Console.WriteLine($" Sorry my friend. {msg} ");
                res = false;
            }

            if (!res)
            {
                Console.WriteLine("The task is to do it again ... ");
                Console.WriteLine(" ... ");
            }

            return res;
        }


        // ask a user for the count of pets
        private static int GetPetCountFromConsole()
        {
            int m = 0;
            bool isCorrect;
            
            do
            {
                Console.WriteLine("How many pet do you have? ");
                isCorrect = int.TryParse(Console.ReadLine(), out m);
                isCorrect = isCorrect && (m >= 0);
                if (!isCorrect)
                    Console.WriteLine($"error: the number (0 - {int.MaxValue}) is required ");
            } while (!isCorrect);

            if (m == 0)
                Console.WriteLine("You entered 0. It means you don't have any pet ");

            return m;
        }


        // ask a user for the count of prefered colors
        private static int GetColorCountFromConsole()
        {
            int m = 0;
            bool isCorrect;
            
            do
            {
                Console.WriteLine("How many colors do you prefer? ");
                isCorrect = int.TryParse(Console.ReadLine(), out m);
                isCorrect = isCorrect && (m >= 0);
                if (!isCorrect)
                    Console.WriteLine($"error: the number (0 - {int.MaxValue}) is required ");
            } while (!isCorrect);

            if (m == 0)
                Console.WriteLine("You entered 0. It means you don't prefer any color ");

            return m;
        }


        // ask a user for pet names
        private static string[] GetPetNameFromConsole(int m = 0)
        {
            string[] petNames = new string[m];
            
            for (int i =0; i < petNames.Length; i++)
            {
                Console.WriteLine("Enter the name of your pet #{0}: ", i + 1);
                string? petName = Console.ReadLine();
                petNames[i] = (petName is null) ? string.Empty : petName;
                petNames[i] = (petNames[i] == string.Empty) ? "{nothing}" : petNames[i];
            }

            return petNames;
        }


        // ask a user for colors
        private static string[] GetColorNameFromConsole(int m = 0)
        {
            string[] colorNames = new string[m];
            
            for (int i =0; i < colorNames.Length; i++)
            {
                Console.WriteLine("Enter the name of your favorite color #{0}: ", i + 1);
                string? colorName = Console.ReadLine();
                colorNames[i] = (colorName is null) ? string.Empty : colorName;
                colorNames[i] = (colorNames[i] == string.Empty) ? "{nothing}" : colorNames[i];
            }

            return colorNames;
        }

    }
}

