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

        var EventGroup = context.AddGroup(EventRegistrationTaskPermissions.EventGroupName, L("EventRegistrationTask.Events"));
        EventGroup.AddPermission(EventRegistrationTaskPermissions.CreateEditEventPermission, L("Permission:Events:CreateEditEvent"));
        EventGroup.AddPermission(EventRegistrationTaskPermissions.DeleteEventPermission, L("Permission:Events:DeleteEvent"));
        EventGroup.AddPermission(EventRegistrationTaskPermissions.GetEventPermission, L("Permission:Events:GetEvent"));
        EventGroup.AddPermission(EventRegistrationTaskPermissions.ListEventPermission, L("Permission:Events:ListEvent"));
        EventGroup.AddPermission(EventRegistrationTaskPermissions.ActiveEventPermission, L("Permission:Events:Active"));

        var EventRegistrations = context.AddGroup(EventRegistrationTaskPermissions.EventRegistratioGroupName, L("EventRegistrationTask.EventRegistrations"));
        EventRegistrations.AddPermission(EventRegistrationTaskPermissions.EventRegistrationView, L("Permission:EventRegistrations:EventRegistrationView"));
    }

    private static LocalizableString L(string name)
    {
        return LocalizableString.Create<EventRegistrationTaskResource>(name);
    }
}
