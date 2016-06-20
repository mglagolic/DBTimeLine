namespace Customizations.Core.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class, Inherited = false, AllowMultiple = true)]
    public sealed class CustomizationAttribute : System.Attribute
    {
        public string CustomizationKey { get; set; }
        //public string ActivationKey { get; set; }
    }

    [System.AttributeUsage(System.AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
    public sealed class MethodActivationCustomizationAttribute : System.Attribute
    {
        public string ActivationKey { get; set; }
    }
}
