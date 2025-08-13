public class Node
{
    public int Data { get; set; }
    public Node? Right { get; private set; }
    public Node? Left { get; private set; }

    public Node(int data)
    {
        this.Data = data;
    }

    public void Insert(int value)
    {
        if (value == Data) return; // block duplicates

        if (value < Data)
        {
            if (Left is null) Left = new Node(value);
            else Left.Insert(value);
        }
        else
        {
            if (Right is null) Right = new Node(value);
            else Right.Insert(value);
        }
    }

    public bool Contains(int value)
    {
        if (value == Data) return true;
        if (value < Data) return Left?.Contains(value) ?? false;
        return Right?.Contains(value) ?? false;
    }

    public int GetHeight()
    {
        int lh = Left?.GetHeight() ?? 0;
        int rh = Right?.GetHeight() ?? 0;
        return 1 + Math.Max(lh, rh);
    }
}