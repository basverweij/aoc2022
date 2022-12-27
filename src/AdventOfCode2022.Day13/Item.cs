namespace AdventOfCode2022.Day13;

sealed class Item
{
    public readonly bool IsList;

    public readonly int Value;

    public readonly Item? Parent;

    public readonly List<Item> List = new();

    public Item(
        int value)
    {
        IsList = false;

        Value = value;
    }

    public Item(Item? parent)
    {
        IsList = true;

        Parent = parent;
    }

    public override string ToString()
    {
        return IsList ?
            $"[{string.Join(",", List)}]" :
            Value.ToString();
    }
}