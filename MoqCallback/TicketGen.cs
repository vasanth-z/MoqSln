namespace MoqCallback
{
    public interface IPersonNameCleaner
    {
        void Clean(Person person);
    }

    public class Person
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }

    public class Ticket
    {
        public string Name { get; set; }
    }

    public class TicketGenerator
    {
        private IPersonNameCleaner _cleaner;

        public TicketGenerator(IPersonNameCleaner cleaner)
        {
            _cleaner = cleaner;
        }

        public Ticket GenerateTicket(Person person)
        {
            _cleaner.Clean(person);
            return new Ticket
            {
                Name = string.Format("{0} {1}", person.FirstName, person.LastName)
            };
        }
    }
}