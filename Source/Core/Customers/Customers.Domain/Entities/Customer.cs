using Customer.Domain.Events;
using Customer.Domain.VOs;

namespace Customer.Domain.Entities;

public class Customer : Aggregate
{
	// Constructor
	public Customer(Guid id, string fullName, DateTime birthDate, string email, string phoneNumber, Address address)
	{
		Id = id;
		FullName = fullName;
		BirthDate = birthDate;
		Email = email;
		PhoneNumber = phoneNumber;
		Address = address;
	}

	// Properties
	public string FullName { get; private set; }
	public DateTime BirthDate { get; private set; }
	public string PhoneNumber { get; private set; }
	public Address Address { get; private set; }
	public string Email { get; private set; }

	// Static Factory Method
	public static Customer Create(string fullName, DateTime birthDate, string email, string phoneNumber,
		Address address)
	{
		var customer = new Customer(Guid.NewGuid(), fullName, birthDate, email, phoneNumber, address);
		customer.AddEvent(new CustomerCreatedEvent(customer.Id, customer.FullName, customer.Email));
		return customer;
	}

	// Methods
	private void SetAddress(Address address)
	{
		Address = address;
		AddEvent(new AddressUpdatedEvent(Id, Address.GetFullAddress()));
	}

	public void Update(string phoneNumber, Address address)
	{
		PhoneNumber = phoneNumber;
		SetAddress(address);
		AddEvent(new CustomerUpdatedEvent(Id, PhoneNumber, Address));
	}
}