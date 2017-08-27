using Moq;
using NUnit.Framework;
using System;

public interface IFileWriter
{
    string FileName { get; set; }
    void WriteLine(string line);
  
}

public class Order
{
    public int OrderId { get; set; }
    public decimal OrderTotal { get; set; }
}

public class OrderWriter
{
    private readonly IFileWriter fileWriter;

    public OrderWriter(IFileWriter fileWriter)
    {
        this.fileWriter = fileWriter;
    }

    public void WriteOrder(Order order)
    {
        string s = "";
        fileWriter.FileName = order.OrderId + ".txt";
        fileWriter.WriteLine(String.Format("{0},{1}", order.OrderId, order.OrderTotal));
        string ss = "";
    }

   
}
[TestFixture]
public class When_an_order_is_to_be_written_to_disk
{
    [Test]
    public void it_should_pass_data_to_file_writer()
    {
        Mock<IFileWriter> mockFileWriter = new Mock<IFileWriter>();


  
        OrderWriter orderWriter = new OrderWriter(mockFileWriter.Object);

        Order order = new Order();
        order.OrderId = 1001;
        order.OrderTotal = 10.53M;
        orderWriter.WriteOrder(order);
        // mockFileWriter.Verify(fw => fw.WriteLine("1001,10.53"), Times.Exactly(1));
        mockFileWriter.VerifySet(fw => fw.FileName = "1001.txt");
    }
}