public static class Arrays
{
    /// <summary>
    /// This function will produce an array of size 'length' starting with 'number' followed by multiples of 'number'.  For 
    /// example, MultiplesOf(7, 5) will result in: {7, 14, 21, 28, 35}.  Assume that length is a positive
    /// integer greater than 0.
    /// </summary>
    /// <returns>array of doubles that are the multiples of the supplied number</returns>
    public static double[] MultiplesOf(double number, int length)
    {
        // TODO Problem 1 Start
        // Remember: Using comments in your program, write down your process for solving this problem
        // step by step before you write the code. The plan should be clear enough that it could
        // be implemented by another person.
        //define a aux variable to track the multiple of started point.
    
        //declare an array of doubles with the specified length
        // 1. Input validation
    if (number == 0)
        throw new ArgumentException("parameter 'number' must be positive", nameof(number));

    if (length <= 0)
        throw new ArgumentException("parameter 'length' must be positive", nameof(length));

    // 2. Allocate result array
    double[] multiples = new double[length];

    // 3. Populate with multiples in a single pass
    for (int k = 1; k <= length; k++)
    {
        multiples[k - 1] = number * k;
    }

    // 4. Return the completed array
    return multiples;
    }

    /// <summary>
    /// Rotate the 'data' to the right by the 'amount'.  For example, if the data is 
    /// List<int>{1, 2, 3, 4, 5, 6, 7, 8, 9} and an amount is 3 then the list after the function runs should be 
    /// List<int>{7, 8, 9, 1, 2, 3, 4, 5, 6}.  The value of amount will be in the range of 1 to data.Count, inclusive.
    ///
    /// Because a list is dynamic, this function will modify the existing data list rather than returning a new list.
    /// </summary>
    public static void RotateListRight(List<int> data, int amount)
    {
        if (data == null)
        throw new ArgumentNullException(nameof(data));
        
        if (amount < 1 || amount > data.Count)
        throw new ArgumentOutOfRangeException(nameof(amount), "Amount must be between 1 and data.Count inclusive.");

    // Step 2: Determine the two parts of the list
    // Part 1: Last 'amount' elements to rotate to the front
    List<int> lastPart = data.GetRange(data.Count - amount, amount);

    // Part 2: The remaining elements from the start up to data.Count - amount
    List<int> firstPart = data.GetRange(0, data.Count - amount);

    // Step 3: Combine the two parts
    List<int> rotatedList = new List<int>();
    rotatedList.AddRange(lastPart);
    rotatedList.AddRange(firstPart);

    // Step 4: Copy rotated values back into the original list
    for (int i = 0; i < data.Count; i++)
    {
        data[i] = rotatedList[i];
    }
    }
}
