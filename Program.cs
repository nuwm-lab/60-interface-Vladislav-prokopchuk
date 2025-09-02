//спроба 2 
using System;
using System.Linq;

// Абстрактний клас для працівників
public abstract class Employee
{
    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string Institution { get; set; }

    // Абстрактні методи
    public abstract void EnterData();
    public abstract bool IsSymmetricalLastName();

    // Конструктор
    public Employee(string lastName, string firstName, string institution)
    {
        LastName = lastName;
        FirstName = firstName;
        Institution = institution;
    }

    // Деструктор
    ~Employee()
    {
        Console.WriteLine($"Employee {FirstName} {LastName} is being destroyed.");
    }

    // Загальний метод для виведення деталей
    public abstract string GetDetails();
}

// Інтерфейс для працівників
public interface IEmployee
{
    string LastName { get; set; }
    string FirstName { get; set; }
    string Institution { get; set; }

    void EnterData();
    bool IsSymmetricalLastName();
    string GetDetails();
}

// Клас "Практикант", що реалізує абстрактний клас
public class Praktikant : Employee, IEmployee
{
    // Конструктор для практиканта
    public Praktikant(string lastName, string firstName, string institution)
        : base(lastName, firstName, institution) { }

    // Реалізація абстрактних методів
    public override void EnterData()
    {
        Console.WriteLine("Enter Praktikant data:");
        Console.WriteLine($"Last name: {LastName}, First name: {FirstName}, Institution: {Institution}");
    }

    public override bool IsSymmetricalLastName()
    {
        string reversed = new string(LastName.ToCharArray().Reverse().ToArray());
        return LastName.Equals(reversed, StringComparison.OrdinalIgnoreCase);
    }

    public override string GetDetails()
    {
        return $"{FirstName} {LastName} from {Institution}";
    }
}

// Клас "Працівник фірми", що розширює "Практикант"
public class PratsivnikFirmy : Praktikant
{
    public DateTime HireDate { get; set; }
    public string Position { get; set; }

    // Конструктор для працівника фірми
    public PratsivnikFirmy(string lastName, string firstName, string institution, DateTime hireDate, string position)
        : base(lastName, firstName, institution)
    {
        HireDate = hireDate;
        Position = position;
    }

    // Перевизначення методу для введення даних
    public override void EnterData()
    {
        base.EnterData();
        Console.WriteLine($"Hire date: {HireDate}, Position: {Position}");
    }

    // Метод для визначення стажу роботи
    public int GetWorkExperience()
    {
        return DateTime.Now.Year - HireDate.Year;
    }

    public override string GetDetails()
    {
        return $"{FirstName} {LastName} works as {Position} at {Institution}. Hired on {HireDate.ToShortDateString()}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Демонстрація роботи класів
        Console.WriteLine("Choose object type (1 - Praktikant, 2 - PratsivnikFirmy):");
        string choice = Console.ReadLine();

        IEmployee employee;
        if (choice == "2")
        {
            // Створення об'єкта працівника фірми
            employee = new PratsivnikFirmy(
                lastName: "Ivanov",
                firstName: "Ivan",
                institution: "Kyiv University",
                hireDate: DateTime.Parse("2020-01-01"),
                position: "Software Developer"
            );
        }
        else
        {
            // Створення об'єкта практиканта
            employee = new Praktikant(
                lastName: "Olegov",
                firstName: "Oleg",
                institution: "Lviv Polytechnic"
            );
        }

        // Виклик методів
        employee.EnterData();
        Console.WriteLine($"Is last name symmetrical? {employee.IsSymmetricalLastName()}");

        // Виведення деталей працівника
        Console.WriteLine($"Employee details: {employee.GetDetails()}");

        // Якщо це працівник фірми, визначити стаж роботи
        if (employee is PratsivnikFirmy pratsivnik)
        {
            Console.WriteLine($"Work experience: {pratsivnik.GetWorkExperience()} years");
        }

        // Демонстрація знищення об'єкта
        employee = null;
        GC.Collect(); // Примусове виклик зборщика сміття, щоб викликати деструктор
        GC.WaitForPendingFinalizers();
    }
}
