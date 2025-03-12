namespace RAG.EventRegistrationTask.Permissions;

public static class EventRegistrationTaskPermissions
{
    public const string GroupName = "EventRegistrationTask";

    //Product Group & Permissions
    public const string ProductGroupName = GroupName + ".Event";
    public const string CreateEditEventPermission = ProductGroupName + ".CreateEdit";
    public const string DeleteEventPermission = ProductGroupName + ".Delete";
    public const string GetEventPermission = ProductGroupName + ".Get";
    public const string ListEventPermission = ProductGroupName + ".List";
    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
}
