using CustomList;

namespace CustomLits
{
    class Program
    {
        static void Main(string[] args)
        {
            var china = new ChinaList<int>();
            china.OnExpand += (sender, args) => Console.WriteLine("China expanded 0_0");
            china.Add(1);
            china.Add(2);
            china.Add(3);
            china.Add(4);

            var sortedChina = china.OrderBy(x=>x);

            foreach (var num in sortedChina)
            {
                Console.WriteLine(num);
            }
            Console.ReadKey();
        }
    }
}