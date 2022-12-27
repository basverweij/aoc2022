using AdventOfCode2022.Day13;

internal class ItemComparer :
    IComparer<Item>
{
    public int Compare(Item? left, Item? right)
    {
        if (left == null || right == null)
        {
            throw new NotImplementedException();
        }

        if (!left.IsList && !right.IsList)
        {
            if (left.Value < right.Value)
            {
                return -1;
            }

            if (left.Value > right.Value)
            {
                return 1;
            }

            return 0;
        }

        if (!left.IsList && right.IsList)
        {
            left = new(left)
            {
                List =
            {
                left,
            },
            };

            return Compare(
                left,
                right);
        }

        if (left.IsList && !right.IsList)
        {
            right = new(right)
            {
                List =
            {
                right,
            },
            };

            return Compare(
                left,
                right);
        }

        // both are lists

        for (var i = 0; i < left.List.Count; i++)
        {
            if (i >= right.List.Count)
            {
                return 1;
            }

            var isInRightOrder = Compare(
                left.List[i],
                right.List[i]);

            if (isInRightOrder != 0)
            {
                return isInRightOrder;
            }
        }

        return left.List.Count == right.List.Count ?
            0 :
            -1;
    }
}
