namespace Data;

public class IdentityUser
{
    // Unique identifier for the user
    public int Id { get; set; }

    // User name for this user
    public string UserName { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }

    // Normalized version of the user name
    public string NormalizedUserName { get; set; }

    // Email address for the user
    public string Email { get; set; }

    // Normalized version of the email address
    public string NormalizedEmail { get; set; }

    // True if the email address has been confirmed
    public bool EmailConfirmed { get; set; }

    // A random value that must change whenever a user is persisted to the store
    public string SecurityStamp { get; set; }

    // A salted and hashed representation of the password for the user
    public string PasswordHash { get; set; }

    // A telephone number for the user
    public string PhoneNumber { get; set; }

    // True if the phone number has been confirmed
    public bool PhoneNumberConfirmed { get; set; }

    // Is two-factor authentication enabled for this user
    public bool TwoFactorEnabled { get; set; }

    // DateTime in UTC when lockout ends, any time in the past is considered not locked out.
    public DateTimeOffset? LockoutEnd { get; set; }

    // Is lockout enabled for this user
    public bool LockoutEnabled { get; set; }

    // Used to record failures for the purposes of lockout
    public int AccessFailedCount { get; set; }

    // Additional properties can be added here as needed
}
