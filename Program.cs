using System;
using System.Collections.Generic;
using System.Linq;

class Expense 
{
    public string Description { get; set;}
    public decimal Amount {get; set;}
    public string Category {get; set;}

    public Expense(string description, decimal amount, string category)
    {
           Description = description;
           Amount = amount;
           Category = category;
    }
 
}

class Program 
{
    static void Main(string [] args)
    {
        List<Expense> budget = new List<Expense> ();
        int menu = 0;
        while (menu != 4)
        {
            ShowMenu();
            menu = GetMenuChoice();
            switch(menu)
            {
                case 1:
                AddBudget(budget);
                break;
                case 2:
                ViewBudget(budget);
                break;
                case 3:
                ViewReport(budget);
                break;
                case 4:
                Console.WriteLine("Goodbye!.");
                break;
                default:
                Console.WriteLine("Invalid Option");
                break;
            }
        }
    }
    static void ShowMenu()
    {
        Console.WriteLine("Welcome To Budget Tracker");
        Console.WriteLine("Press 1 To Add Your Budget");
        Console.WriteLine("Press 2 To View Your Budget");
        Console.WriteLine("Press 3 To Sum Your Budget");
        Console.WriteLine("Press 4 To Exit");
    }
    static int GetMenuChoice()
    {
        bool valid = int.TryParse(Console.ReadLine(), out int choice);
        return valid ? choice : -1;
    }
    static void AddBudget(List<Expense> budget)
    {
        Console.WriteLine("Enter budget description");
        string description = Console.ReadLine();
        Console.WriteLine("Enter budget amount");
        bool ValidAmount = decimal.TryParse(Console.ReadLine(), out decimal amount);
        if (!ValidAmount)
        {
            Console.WriteLine("Please enter a valid amount");
            return;
        }
        Console.WriteLine("Enter budget Category");
        string category = Console.ReadLine();

        budget.Add(new Expense(description, amount, category));
    }
    static void ViewBudget(List<Expense> budget)
    {
        if ( budget.Count == 0)
        {
            Console.WriteLine("No budget added yet");
            return;
        }
        else
        {
            Console.WriteLine("Your budgets are listed below");
            foreach (Expense b in budget)
            {
                Console.WriteLine($"{b.Description} - {b.Amount} - {b.Category}");
            }
        }
    }
    static void ViewReport(List<Expense> budget)
    {
        if ( budget.Count == 0)
        {
            Console.WriteLine("No budget added yet");
            return;
        }
        else
        {
            var CategoryTotal = budget
            .GroupBy( e => e.Category)
            .Select(g => new { Category = g.Key, Total = g.Sum(e => e.Amount) })
            .OrderByDescending(x => x.Total);

            foreach(var group in CategoryTotal)
            {
                Console.WriteLine($"{group.Category} - {group.Total:C}");
            }
        }
    }
}           