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
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<EventRegistrationTaskResource>(name);
    }
}
