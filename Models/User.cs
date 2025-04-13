using System;
using System.Collections.Generic;

namespace FoodProducts.Models;

public partial class User
{
    public int Id { get; set; }

    public string LastName { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string? Patronymic { get; set; }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public byte RoleId { get; set; }

    public virtual Role Role { get; set; } = null!;
}
