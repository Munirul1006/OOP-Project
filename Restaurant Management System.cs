using System;
using System.Collections.Generic;
// This project was complited entiely by Md. Munirul Islam(1006)
public class Person {
    public int Id { get; set; }
    public string Name { get; set; }

    public Person(int id, string name) 
    {
        Id = id;
        Name = name;
    }
}
// MenuItem class
public class MenuItem {
    public string Name { get; set; }
    public double Price { get; set; }

    // Constructor overloading
    public MenuItem(string name) 
    {
        Name = name;
        Price = 0;
    }
    public MenuItem(string name, double price) 
    {
        Name = name;
        Price = price;
    }

    // Operator overloading
    public static bool operator >(MenuItem a, MenuItem b) => a.Price > b.Price;
    public static bool operator <(MenuItem a, MenuItem b) => a.Price < b.Price;
    public override string ToString() => $"{Name} - {Price} Tk";
}

// Customer class
public class Customer : Person {
    public string Contact { get; set; }
    public Customer(int id, string name, string contact) : base(id, name) 
    {
        Contact = contact;
    }
    public Customer(Customer other) : base(other.Id, other.Name) 
    {
        Contact = other.Contact;
    }
}
// Employee class
public class Employee : Person {
    public string Position { get; set; }
    public Employee(int id, string name, string position) : base(id, name) 
    {
        Position = position;
    }
}
// Order class
public class Order {
    public Customer Customer { get; set; }
    public List<MenuItem> Items { get; set; }

    public Order(Customer customer) 
    {
        Customer = customer;
        Items = new List<MenuItem>();
    }
    public double Total 
    {
        get { return CalculateTotal(); }
    }

    private double CalculateTotal() 
    {
        double sum = 0;
        foreach (var item in Items) sum += item.Price;
        return sum;
    }
}

// Interfaces
public interface IBillable 
{
    void GenerateBill(Order order);
}
public interface IReportable {
    void GenerateReport();
}

// Restaurant class
public class Restaurant : IBillable, IReportable {
    public static double TotalSales = 0;
    private List<MenuItem> Menu = new List<MenuItem>();
    private List<Customer> Customers = new List<Customer>();
    private List<Employee> Employees = new List<Employee>();
    private List<Order> Orders = new List<Order>();

    // Menu management
    public void AddMenuItem(MenuItem item) => Menu.Add(item);
    public void UpdateMenuItem(string name, double newPrice) 
    {
        foreach (var item in Menu) {
            if (item.Name == name) {
                item.Price = newPrice;
                break;
            }
        }
    }
    public void RemoveMenuItem(string name) 
    {
        for (int i = 0; i < Menu.Count; i++) {
            if (Menu[i].Name == name) {
                Menu.RemoveAt(i);
                break;
            }
        }
    }

    // Customer management
    public void AddCustomer(Customer c) => Customers.Add(c);
    public void UpdateCustomer(int id, string newName, string newContact) 
    {
        foreach (var cust in Customers) {
            if (cust.Id == id) {
                cust.Name = newName;
                cust.Contact = newContact;
                break;
            }
        }
    }

    // Employee management
    public void AddEmployee(Employee e) => Employees.Add(e);
    public void UpdateEmployee(int id, string newName, string newPosition) 
    {
        foreach (var emp in Employees) {
            if (emp.Id == id) {
                emp.Name = newName;
                emp.Position = newPosition;
                break;
            }
        }
    }
    // Show Employees
    public void ShowEmployees()
     {
    Console.WriteLine("Employee List:");
        foreach (var emp in Employees) 
        {
         Console.WriteLine($"ID: {emp.Id}, Name: {emp.Name}, Position: {emp.Position}");
        }
    Console.WriteLine("\n");
    }

    // Order placement
    public void PlaceOrder(Order order) 
    {
        Orders.Add(order);
        TotalSales += order.Total;
    }

    // Billing section
    public void GenerateBill(Order order)
     {
        Console.WriteLine($"\nTotal Bill for {order.Customer.Name}: ");
        //Console.WriteLine($"Customer: {order.Customer.Name}");
        foreach (var item in order.Items) 
        {
            Console.WriteLine($"{item.Name} - {item.Price} Tk");
        }
        Console.WriteLine($"TOTAL AMOUNT: {order.Total} Taka Only");
        Console.WriteLine("\n");
    }

    // Report generation
    public void GenerateReport() {
        Console.WriteLine("\nDAILY SALES REPORT: ");
        Console.WriteLine($"Total Sales: {TotalSales} Taka");

        Dictionary<string, int> itemCount = new Dictionary<string, int>();
        foreach (var order in Orders) 
        {
            foreach (var item in order.Items) 
            {
                if (itemCount.ContainsKey(item.Name))
                    itemCount[item.Name]++;
                else
                    itemCount[item.Name] = 1;
            }
        }

        string popularItem = null;
        int maxCount = 0;
        foreach (var kvp in itemCount)
         {
            if (kvp.Value > maxCount) 
            {
                maxCount = kvp.Value;
                popularItem = kvp.Key;
            }
        }

        if (popularItem != null)
            Console.WriteLine($"Most Popular Item: {popularItem} ({maxCount} orders)");
        Console.WriteLine("\n");
    }

    // Menu display
    public void ShowMenu() {
        Console.WriteLine("\n--- MENU ---");
        foreach (var item in Menu) Console.WriteLine(item);
        Console.WriteLine("\n");
    }
}
// Main Program
// - Md. Munirul Islam(1006)
public class Program 
{
    public static void Main(string[] args) {
        Restaurant restaurant = new Restaurant();

        // Add Menu Items
        restaurant.AddMenuItem(new MenuItem("Kacchi", 350.00));
        restaurant.AddMenuItem(new MenuItem("Tehari", 200.00));
        restaurant.AddMenuItem(new MenuItem("Biryani", 250.00));
        restaurant.AddMenuItem(new MenuItem("Khechuri", 150.00));
        restaurant.AddMenuItem(new MenuItem("Mojo 250ml", 20.00));
        restaurant.AddMenuItem(new MenuItem("Mojo 500ml", 40.00));
        restaurant.AddMenuItem(new MenuItem("Water 500ml", 30.00));
        restaurant.ShowMenu();

        // Add Customer Section
        Customer cust1 = new Customer(1, "Bithi", "91004");
        Customer cust2 = new Customer(2, "Sabiha", "91380");
        Customer cust3 = new Customer(3, "Ayesha", "91024");
        restaurant.AddCustomer(cust1);
        restaurant.AddCustomer(cust2);
        restaurant.AddCustomer(cust3);
        

        // Add Employee Section
        Employee emp1 = new Employee(1, "Sajjad", "Chef");
        Employee emp2 = new Employee(2, "Saikat", "Waiter");
        Employee emp3 = new Employee(3, "Munirul", "Manager");
        restaurant.AddEmployee(emp1);
        restaurant.AddEmployee(emp2);
        restaurant.AddEmployee(emp3);

        // Order Placment and billing section
        Order order1 = new Order(cust1);
        order1.Items.Add(new MenuItem("Kacchi", 350.00));
        order1.Items.Add(new MenuItem("Mojo 500ml", 40.00));
        restaurant.PlaceOrder(order1);
        restaurant.GenerateBill(order1);

        Order order2 = new Order(cust2);
        order2.Items.Add(new MenuItem("Tehari", 200.00));
        order2.Items.Add(new MenuItem("Mojo 250ml", 20.00));
        order2.Items.Add(new MenuItem("Water 500ml", 30.00));
        restaurant.PlaceOrder(order2);
        restaurant.GenerateBill(order2);

        Order order3 = new Order(cust3);
        order3.Items.Add(new MenuItem("Khechuri", 150.00));
        order3.Items.Add(new MenuItem("Mojo 250ml", 20.00));
        restaurant.PlaceOrder(order3);
        restaurant.GenerateBill(order3);


        // Generate Daily Report
        restaurant.GenerateReport();
        // Show Employee List
        restaurant.ShowEmployees();
    }
}