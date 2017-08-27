using Moq;
using NUnit.Framework;

namespace MoqCallback
{
    [TestFixture]
    internal class TicketGenTest
    {
        [Test]
        public void AGeneratedTicketHasTheCorrectName()
        {
            Person person = new Person { FirstName = "Bob", LastName = "Smith", Age = 18 };

            Person person2 = new Person { FirstName = "x", LastName = "y", Age = 18 };

            Mock<IPersonNameCleaner> _mockCleaner = new Mock<IPersonNameCleaner>();
            _mockCleaner.Setup(m => m.Clean(It.IsAny<Person>()))
                        .Callback((Person o) => { person2 = o; });

            TicketGenerator generator = new TicketGenerator(_mockCleaner.Object);

            Ticket ticket = generator.GenerateTicket(person);

            Assert.AreEqual("B Smith", ticket.Name);
        }
    }
}