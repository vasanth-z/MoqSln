using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

[TestFixture]
public class When_creating_a_gold_status_customer
{
    [Test]
    public void A_special_save_routine_should_be_used()
    {


        //Arrange
        Mock<ICustomerRepository> mockCustomerRepository = new Mock<ICustomerRepository>();
        Mock<ICustomerStatusFactory> mockCustomerStatusFactory = new Mock<ICustomerStatusFactory>();
        CustomerViewModel customerViewModel = new CustomerViewModel()
        {
            FirstName = "Elvis"  ,
            LastName = "Presley"  ,
            Status = CustomerStatus.Gold
        };

        mockCustomerStatusFactory.Setup(
                  c => c.CreateFrom(It.Is<CustomerViewModel>(u => u.Status == CustomerStatus.Gold)))
                  .Returns(CustomerStatus.Gold);


        CustomerService customerService = new CustomerService(mockCustomerRepository.Object, mockCustomerStatusFactory.Object);

        //Act
        customerService.Create(customerViewModel);

        //Assert
        mockCustomerRepository.Verify( x => x.SaveSpecial(It.IsAny<Customer>()));

    }
}

public class Customer
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public CustomerStatus StatusLevel { get; set; }

    public Customer(string firstName, string lastName)
    {
        FirstName = firstName;
        LastName = lastName;
    }
}

public enum CustomerStatus
{
    Bronze,
    Gold,
    Platinum
}

public class CustomerViewModel
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public CustomerStatus Status { get; set; }
}

public interface ICustomerRepository
{
    void SaveSpecial(Customer customer);
    void Save(Customer customer);
}

public interface ICustomerStatusFactory
{
    CustomerStatus CreateFrom(CustomerViewModel customerToCreate);
}

public class CustomerService
{
    private readonly ICustomerRepository _customerRepository;
    private readonly ICustomerStatusFactory _customerStatusFactory;

    public CustomerService(ICustomerRepository customerRepository, ICustomerStatusFactory customerStatusFactory)
    {
        _customerRepository = customerRepository;
        _customerStatusFactory = customerStatusFactory;
    }

    public void Create(CustomerViewModel customerToCreate)
    {
        var customer = new Customer( customerToCreate.FirstName, customerToCreate.LastName);
        customer.StatusLevel =  _customerStatusFactory.CreateFrom(customerToCreate);
        if (customer.StatusLevel == CustomerStatus.Gold)
        {
            _customerRepository.SaveSpecial(customer);
        }
        else
        {
            _customerRepository.Save(customer);
        }
    }
}

