using System;
using System.Linq;

namespace Linq
{
    class Program
    {
        static void Main(string[] args)
        {
            //1
            string[] startA = StartsWithA(new string[] { "Apple", "Banana", "Watermelon", "Apricot", "Orange", "ARBUZ)))", "Grapes" });
            foreach (string s in startA)
            {
                Console.WriteLine(s);
            }

            //2
            int[] a = { 1, 2, 3, 4, 5 };
            int[] b = { 1, 3, 6, 5, 8 };
            int[] common = FindCommon(a, b);

            Console.Write("Common elements:");
            foreach (int i in common)
                Console.Write($"{i},");
            Console.WriteLine("\n");

            //3
            Student[] students =
            {
                new Student { FirstName = "Artem", LastName = "Artemov", Grade = 85 },
                new Student { FirstName = "Danylo", LastName = "Boghdanov", Grade = 92 },
                new Student { FirstName = "Maria", LastName = "Arsenieva", Grade = 78 },
                new Student { FirstName = "Andriy", LastName = "Shevchenko", Grade = 95 },
                new Student { FirstName = "Volodymyr", LastName = "Zelenkyi", Grade = 88 }
            };
            Student topStudent = TopGradeStudent(students);
            Console.WriteLine($"Student with the highest grade is {topStudent.FirstName} {topStudent.LastName} with grade of {topStudent.Grade}");

            //4
            Product[] products =
            {
                new Product { Name = "Laptop", Price = 1200, Category = "Electronics" },
                new Product { Name = "Smartphone", Price = 800, Category = "Electronics" },
                new Product { Name = "T-shirt", Price = 20, Category = "Clothing" },
                new Product { Name = "Jeans", Price = 50, Category = "Clothing" },
                new Product { Name = "Book", Price = 30, Category = "Books" },
                new Product { Name = "Headphones", Price = 250, Category = "Electronics" }
            };
            GetAveragePriceByCategory(products);

            //5
            Employee[] employees =
            {
                new Employee { FirstName = "John", LastName = "Doe", BirthDate = new DateTime(1996, 5, 15), EmploymentDate = new DateTime(2017, 7, 1) },
                new Employee { FirstName = "Jane", LastName = "Smith", BirthDate = new DateTime(1995, 10, 8), EmploymentDate = new DateTime(2023, 3, 12) },
                new Employee { FirstName = "Mike", LastName = "Johnson", BirthDate = new DateTime(1990, 3, 25), EmploymentDate = new DateTime(2020, 9, 5) },
                new Employee { FirstName = "Emily", LastName = "Brown", BirthDate = new DateTime(1988, 12, 10), EmploymentDate = new DateTime(2014, 1, 20) },
                new Employee { FirstName = "Chris", LastName = "Davis", BirthDate = new DateTime(1991, 7, 20), EmploymentDate = new DateTime(2016, 11, 3) }
            };

            Employee[] TopEmployees = GetOverFiveYearsEmployee(employees);
            Console.Write("Super Cool Employees:");
            foreach (Employee employee in TopEmployees)
            {
                Console.Write($"{employee.FirstName} {employee.LastName}, ");
            }

            //6
            Book[] books =
            {
                new Book { Title = "Dune", Author = "Frank Herbert", ReleaseDate = 1965, Genre = "Novel" },
                new Book { Title = "Neuromancer", Author = "William Gibson", ReleaseDate = 1984, Genre = "Novel" },
                new Book { Title = "Snow Crash", Author = "Neal Stephenson", ReleaseDate = 1992, Genre = "Sci-Fi" },
                new Book { Title = "Ready Player One", Author = "Ernest Cline", ReleaseDate = 2011, Genre = "Sci-Fi" },
                new Book { Title = "The Hunger Games", Author = "Suzanne Collins", ReleaseDate = 2008, Genre = "Dystopian" },
                new Book { Title = "The Martian", Author = "Andy Weir", ReleaseDate = 2011, Genre = "Sci-Fi" }
            };

            Book[] CoolBooks = GetSciFiOver2010(books);
            Console.Write("\nSci-Fi books released after 2010: ");
            foreach (Book book in CoolBooks)
            {
                Console.Write($"{book.Title}, ");
            }

            //7
            Customer[] customers =
            {
                new Customer { FirstName = "John", LastName = "Doe", Address = "123 Main St", Order = new int[] { 500, 700, 1200 } },
                new Customer { FirstName = "Jane", LastName = "Smith", Address = "456 Oak St", Order = new int[] { 800, 1100, 900 } },
                new Customer { FirstName = "Mike", LastName = "Johnson", Address = "789 Elm St", Order = new int[] { 600, 300, 50 } },
                new Customer { FirstName = "Emily", LastName = "Brown", Address = "101 Pine St", Order = new int[] { 400, 200, 250 } },
                new Customer { FirstName = "Chris", LastName = "Davis", Address = "222 Maple St", Order = new int[] { 500, 400, 100 } }
            };

            Customer[] TopCustomers = GetTopCustomer(customers);
            Console.WriteLine("\nTop Customers:");
            foreach (Customer customer in TopCustomers)
            {
                Console.WriteLine($"\n{customer.FirstName} {customer.LastName} with order:");
                foreach (var orders in customer.Order)
                {
                    Console.Write($"{orders}, ");
                }
            }
        }
        static string[] StartsWithA(string[] words)
        {
            return words.Where(word => word.StartsWith("A")).ToArray();
        }

        static int[] FindCommon(int[] a, int[] b)
        {
            return a.Intersect(b).ToArray();
        }

        static Student TopGradeStudent(Student[] students)
        {
            return students.OrderByDescending(s => s.Grade).First();
        }

        static void GetAveragePriceByCategory(Product[] products)
        {
            var grouped = products.GroupBy(p => p.Category);
            foreach (var product in grouped)
            {
                double averagePrice = product.Average(product => product.Price);
                Console.WriteLine($"Category: {product.Key}, Average Price: {averagePrice}");
            }
        }

        static Employee[] GetOverFiveYearsEmployee(Employee[] employees)
        {
            return employees.Where(e=> DateTime.Now.Year - e.EmploymentDate.Year >= 5).ToArray();
        }

        static Book[] GetSciFiOver2010(Book[] books)
        {
            return books.Where(b=>b.Genre == "Sci-Fi" && b.ReleaseDate>=2010).ToArray();
        }

        static Customer[] GetTopCustomer(Customer[] customers)
        {
            return customers.Where(c=>c.Order.Sum()>=1000).ToArray();
        }
    }
}