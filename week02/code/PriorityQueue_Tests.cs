using Microsoft.VisualStudio.TestTools.UnitTesting;

// Problem 2 - Write and run test cases and fix the code to match requirements.

[TestClass]
public class PriorityQueueTests
{
    [TestMethod]
    // Scenario: Add items with different priorities and ensure that the item with the highest priority is dequeued first.
    // Expected Result: The item with the highest priority (B) is dequeued first, followed by the next (C), then the lowest (A).
    // Defect(s) Found: TBD once code is tested.
    public void TestPriorityQueue_1()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("A", 1);  // lowest
        priorityQueue.Enqueue("B", 5);  // highest
        priorityQueue.Enqueue("C", 3);  // middle

        Assert.AreEqual("B", priorityQueue.Dequeue());
        Assert.AreEqual("C", priorityQueue.Dequeue());
        Assert.AreEqual("A", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Add two items with the same highest priority, verify FIFO order is maintained.
    // Expected Result: The first item added with high priority (X) is dequeued before the second (Y).
    // Defect(s) Found: TBD once code is tested.
    public void TestPriorityQueue_2()
    {
        var priorityQueue = new PriorityQueue();

        priorityQueue.Enqueue("X", 10);  // first, high
        priorityQueue.Enqueue("Y", 10);  // second, same priority
        priorityQueue.Enqueue("Z", 1);   // low

        Assert.AreEqual("X", priorityQueue.Dequeue());
        Assert.AreEqual("Y", priorityQueue.Dequeue());
        Assert.AreEqual("Z", priorityQueue.Dequeue());
    }

    [TestMethod]
    // Scenario: Try to dequeue from an empty queue.
    // Expected Result: Throws InvalidOperationException with "The queue is empty."
    // Defect(s) Found: TBD once code is tested.
    [ExpectedException(typeof(InvalidOperationException))]
    public void TestPriorityQueue_Empty()
    {
        var priorityQueue = new PriorityQueue();
        priorityQueue.Dequeue();
    }
}
