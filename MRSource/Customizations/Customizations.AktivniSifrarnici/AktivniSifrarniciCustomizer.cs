using Customizations.Core.Attributes;

namespace AktivniSifrarnici
{

    [Customization(CustomizationKey = "AktivniSifrarnici")]
    public class AktivniSifrarniciCustomizer : Customizations.Core.Customizer
    {
        [MethodActivationCustomization(ActivationKey = "CreateTimeLine")]
        public void CreateTimeLine()
        {
            var module = new AktivniSifrarniciDBModule();
            module.CreateTimeLine();
        }
    }
}
