namespace GoogleAnalytics.Ga4.DotNet.Sdk;
/// <summary>
/// Indicates that a type and all derived types are used to serve HTTP API responses.
/// <para>
/// Controllers decorated with this attribute are configured with features and behavior targeted at improving the
/// developer experience for building APIs.
/// </para>
/// <para>
/// When decorated on an assembly, all controllers in the assembly will be treated as controllers with API behavior.
/// For more information, see <see href="https://docs.microsoft.com/aspnet/core/web-api/#apicontroller-attribute">ApiController attribute</see>.
/// </para>
/// </summary>
[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class GoogleAnalyticsApiControllerAttribute : GoogleAnalyticsControllerAttribute
{
}

/// <summary>
/// Indicates that the type and any derived types that this attribute is applied to
/// are considered a controller by the default controller discovery mechanism, unless
/// <see cref="NonControllerAttribute"/> is applied to any type in the hierarchy.
/// </summary>
[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
public class GoogleAnalyticsControllerAttribute : Attribute
{
}