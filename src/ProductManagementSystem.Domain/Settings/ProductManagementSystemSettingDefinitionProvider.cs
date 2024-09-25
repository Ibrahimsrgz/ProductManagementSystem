using Volo.Abp.Settings;

namespace ProductManagementSystem.Settings;

public class ProductManagementSystemSettingDefinitionProvider : SettingDefinitionProvider
{
    public override void Define(ISettingDefinitionContext context)
    {
        //Define your own settings here. Example:
        //context.Add(new SettingDefinition(ProductManagementSystemSettings.MySetting1));
    }
}
