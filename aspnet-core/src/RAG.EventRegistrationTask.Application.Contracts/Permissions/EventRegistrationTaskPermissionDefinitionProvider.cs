using RAG.EventRegistrationTask.Localization;
using Volo.Abp.Authorization.Permissions;
using Volo.Abp.Localization;

namespace RAG.EventRegistrationTask.Permissions;

public class EventRegistrationTaskPermissionDefinitionProvider : PermissionDefinitionProvider
{
    public override void Define(IPermissionDefinitionContext context)
    {
        var myGroup = context.AddGroup(EventRegistrationTaskPermissions.GroupName);
        //Define your own permissions here. Example:
        //myGroup.AddPermission(EventRegistrationTaskPermissions.MyPermission1, L("Permission:MyPermission1"));

        var productGroup = context.AddGroup(EventRegistrationTaskPermissions.ProductGroupName, L("EventRegistrationTask.Products"));
        productGroup.AddPermission(EventRegistrationTaskPermissions.CreateEditEventPermission, L("Permission:Products:CreateEditProduct"));
        productGroup.AddPermission(EventRegistrationTaskPermissions.DeleteEventPermission, L("Permission:Products:DeleteProduct"));
        productGroup.AddPermission(EventRegistrationTaskPermissions.GetEventPermission, L("Permission:Products:GetProduct"));
        productGroup.AddPermission(EventRegistrationTaskPermissions.ListEventPermission, L("Permission:Products:ListProduct"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<EventRegistrationTaskResource>(name);
    }
}
