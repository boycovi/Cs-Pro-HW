namespace BinaryTree
{
    public class BinarySearchTree
    {
        public Node Root { get; private set; }
        
        //add
        public void Add(int value)
        {
            Root = AddRecursive(Root, value);
        }

        private Node AddRecursive(Node node, int value)
        {
            if (node is null) return new Node(value);

            if (value < node.Value) node.Left = AddRecursive(node.Left, value);
            if (value > node.Value) node.Right = AddRecursive(node.Right, value);
            return node;
        }
        
        //find
        public Node Find(int value) => FindRecursive(Root, value);
        public Node FindMin(Node node) => node.Left is null ? node : FindMin(node.Left);
        private Node FindRecursive(Node node, int value)
        {
            if (value == node.Value) return node;
            if (value < node.Value) return FindRecursive(node.Left, value);

            return FindRecursive(node.Right, value);
        }
        
        //remove
        public void Remove(int value)
        {
            Root = RemoveRecursive(Root, value);
        }

        private Node RemoveRecursive(Node node, int value)
        {

            if (value < node.Value) node.Left = RemoveRecursive(node.Left, value);
            if (value > node.Value) node.Right = RemoveRecursive(node.Right, value);

            else
            {
                if (node.Left is null) return node.Right;
                if (node.Right is null) return node.Left;


                Node smallestValueNode = FindMin(node.Right);
                node.Value = smallestValueNode.Value;
                node.Right = RemoveRecursive(node.Right, smallestValueNode.Value);
            }

            return node;
        }

        //preOrder
        public List<int> PreOrderTraversal()
        {
            List<int> values = new List<int>();
            PreOrderTraversalRecursive(Root, values);
            return values;
        }

        private void PreOrderTraversalRecursive(Node node, List<int> values)
        {
            if (node is not null)
            {
                values.Add(node.Value);
                PreOrderTraversalRecursive(node.Left, values);
                PreOrderTraversalRecursive(node.Right, values);
            }
        }
        
        //inOrder
        public List<int> InOrderTraversal()
        {
            List<int> values = new List<int>();
            InOrderTraversalRecursive(Root, values);
            return values;
        }

        private void InOrderTraversalRecursive(Node node, List<int> values)
        {
            if (node is not null)
            {
                InOrderTraversalRecursive(node.Left, values);
                values.Add(node.Value);
                InOrderTraversalRecursive(node.Right, values);
            }
        }

        //level
        public List<int> LevelOrderTraversal()
        {
            List<int> values = new List<int>();

            Queue<Node> nodes = new Queue<Node>();
            nodes.Enqueue(Root);

            while (nodes.Count > 0)
            {
                Node node = nodes.Dequeue();
                values.Add(node.Value);

                if (node.Left is not null) nodes.Enqueue(node.Left);
                if (node.Right is not null) nodes.Enqueue(node.Right);
            }

            return values;
        }

        //save
        public void SaveToFile(string path)
        {
            using (StreamWriter writer = new StreamWriter(path))
            SaveToFileRecursive(Root, writer);
        }

        private void SaveToFileRecursive(Node node, StreamWriter writer)
        {
            if (node is null)
            {
                writer.WriteLine("null");
                return;
            }

            writer.WriteLine(node.Value);
            SaveToFileRecursive(node.Left, writer);
            SaveToFileRecursive(node.Right, writer);
        }

        //load
        public void LoadFromFile(string path)
        {
            using (StreamReader reader = new StreamReader(path))
            Root = LoadFromFileRecursive(reader);
        }

        private Node LoadFromFileRecursive(StreamReader reader)
        {
            string value = reader.ReadLine();
            if (value is "null") return null;

            Node node = new Node(int.Parse(value));
            node.Left = LoadFromFileRecursive(reader);
            node.Right = LoadFromFileRecursive(reader);

            return node;
        }
    }
}