﻿public class Customer
{
    public string FullName => $"{FirstName} {LastName}";

    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public int LoyaltyPoints { get; set; }
}