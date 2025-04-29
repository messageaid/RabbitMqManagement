// ReSharper disable ClassNeverInstantiated.Global
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider adding the 'required' modifier or declaring as nullable.

namespace MessageAid.RabbitMqManagement;

public class SetPermissionRequest
{
    public string Configure { get; set; } = "";
    public string Write { get; set; } = "";
    public string Read { get; set; } = "";
}

public record RabbitMqAdminExtensionResponse(string Javascript);

public class RabbitMqFeatureFlagResponse
{
    public string Name { get; set; }
    public string Desc { get; set; }
    public string DocUrl { get; set; }
    public string State { get; set; }
    public string Stability { get; set; }
    public string ProvidedBy { get; set; }
}


public class RabbitMqDeprecatedFeatureResponse
{
    public string Name { get; set; }
    public string Desc { get; set; }
    public string DeprecationPhase { get; set; }
    public string DocUrl { get; set; }
    public string ProvidedBy { get; set; }
}

public class CreateUserRequest
{
    public string? Password { get; set; }
    public string? PasswordHash { get; set; }
    public string? HashingAlgorithm { get; set; }

    /// <summary>
    /// comma seperated list of tags
    /// </summary>
    public string Tags { get; set; } = "";

    public void AddTag(RoleTags tag)
    {
        var str = tag.ToString().ToLower();
        Tags += $",{str}";
    }
}


public enum RoleTags
{
    Administrator,
    Monitoring,
    Management
}

public class UserResponse
{
    public string Name { get; set; } = "";
}