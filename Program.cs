using System;// Vegetable.cs
abstract class Vegetable
{
    public string Name { get; set; }
    public double Weight { get; set; }
    public double CalorieContent { get; protected set; }

    public Vegetable(string name, double weight)
    {
        Name = name;
        Weight = weight;
    }

    public virtual double CalculateCalorieContent()
    {
        return CalorieContent * Weight / 100;
    }
}

// LeafyVegetable.cs
class LeafyVegetable : Vegetable
{
    public LeafyVegetable(string name, double weight) : base(name, weight)
    {
        CalorieContent = 15;
    }
}

// RootVegetable.cs
class RootVegetable : Vegetable
{
    public RootVegetable(string name, double weight) : base(name, weight)
    {
        CalorieContent = 35;
    }
}

// Cucumber.cs
class Cucumber : Vegetable
{
    public bool IsSalted { get; set; }

    public Cucumber(string name, double weight, bool isSalted) : base(name, weight)
    {
        IsSalted = isSalted;
        CalorieContent = 10;
    }

    public override double CalculateCalorieContent()
    {
        if (IsSalted)
        {
            return base.CalculateCalorieContent() * 1.2;
        }
        return base.CalculateCalorieContent();
    }
}

// Salad.cs
class Salad
{
    private Vegetable[] vegetables;

    public Salad(Vegetable[] vegetables)
    {
        this.vegetables = vegetables;
    }

    public double CalculateTotalCalorieContent()
    {
        double totalCalories = 0;
        foreach (Vegetable veggie in vegetables)
        {
            totalCalories += veggie.CalculateCalorieContent();
        }
        return totalCalories;
    }

    public Vegetable[] SortByParameter(string parameter)
    {
        switch (parameter.ToLower())
        {
            case "name":
                return vegetables.OrderBy(v => v.Name).ToArray();
            case "weight":
                return vegetables.OrderBy(v => v.Weight).ToArray();
            case "calories":
                return vegetables.OrderBy(v => v.CalculateCalorieContent()).ToArray();
            default:
                throw new ArgumentException("Invalid sorting parameter");
        }
    }

    public Vegetable[] FindByParameter(string parameter, object value)
    {
        switch (parameter.ToLower())
        {
            case "name":
                return vegetables.Where(v => v.Name == (string)value).ToArray();
            case "weight":
                return vegetables.Where(v => v.Weight == (double)value).ToArray();
            case "salted":
                return vegetables.Where(v => v is Cucumber && ((Cucumber)v).IsSalted == (bool)value).ToArray();
            default:
                throw new ArgumentException("Invalid search parameter");
        }
    }
}

// Program.cs
class Program
{
    static void Main(string[] args)
    {
        Vegetable[] veggies = new Vegetable[] {
            new LeafyVegetable("Spinach", 100),
            new LeafyVegetable("Lettuce", 200),
            new RootVegetable("Carrot", 150),
            new Cucumber("Cucumber", 120, true)
        };

        Salad mySalad = new Salad(veggies);
        Console.WriteLine($"Total calorie content: {mySalad.CalculateTotalCalorieContent()}");

        Vegetable[] sortedByName = mySalad.SortByParameter("name");
        Console.WriteLine("Sorted by name:");
        foreach (Vegetable veggie in sortedByName)
        {
            Console.WriteLine(veggie.Name);
        }
    }
}