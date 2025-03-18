using BinaryTree;
using System;
using System.Collections.Generic;

public class Program
{
    public static void Main(string[] args)
    {
        BinarySearchTree bst = new BinarySearchTree();

        Random rnd = new Random();
        List<int> numbers = new List<int>();

        for (int i = 0; i < 10; i++)
        {
            int num = rnd.Next(1, 100);
            numbers.Add(num);
            bst.Add(num);
        }

        Console.WriteLine("Added numbers: " + string.Join(", ", numbers));

        int searchValue = numbers[rnd.Next(0, 10)];
        Node foundNode = bst.Find(searchValue);
        Console.WriteLine($"Found node with value {searchValue}: {(foundNode != null ? "Yes" : "No")}");


        Console.WriteLine($"Removed minimum value ({bst.FindMin(bst.Root).Value})");
        bst.Remove(bst.FindMin(bst.Root).Value);

        Console.WriteLine("Preorder traversal: " + string.Join(", ", bst.PreOrderTraversal()));
        Console.WriteLine("Inorder traversal: " + string.Join(", ", bst.InOrderTraversal()));
        Console.WriteLine("Level-order traversal: " + string.Join(", ", bst.LevelOrderTraversal()));

        string filePath = "bst.txt";
        bst.SaveToFile(filePath);
        Console.WriteLine($"\nTree saved to file: {filePath}");

        BinarySearchTree loadedBst = new BinarySearchTree();
        loadedBst.LoadFromFile(filePath);
        Console.WriteLine("Loaded tree from file.\n");

        Console.WriteLine("Preorder traversal of loaded tree: " + string.Join(", ", loadedBst.PreOrderTraversal()));
        Console.WriteLine("Inorder traversal: " + string.Join(", ", loadedBst.InOrderTraversal()));
        Console.WriteLine("Level-order traversal: " + string.Join(", ", loadedBst.LevelOrderTraversal()));
    }
}