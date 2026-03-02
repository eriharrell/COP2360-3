using System;

class Program
{
    static void Main()
    {
        Console.Write("Enter the first number: ");
        string input1 = Console.ReadLine();

        Console.Write("Enter the second number: ");
        string input2 = Console.ReadLine();

        DivideStrings(input1, input2);
    }

    static void DivideStrings(string s1, string s2)
    {
        try
        {
            int num1 = Convert.ToInt32(s1);
            int num2 = Convert.ToInt32(s2);

            int result = num1 / num2;

            Console.WriteLine($"Result: {result}");
        }
        catch (FormatException)
        {
            Console.WriteLine("Error: One of the inputs was not a valid number.");
        }
        catch (DivideByZeroException)
        {
            Console.WriteLine("Error: Cannot divide by zero.");
        }
        catch (OverflowException)
        {
            Console.WriteLine("Error: The number entered is too large or too small.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Unexpected error: {ex.Message}");
        }
    }
}
