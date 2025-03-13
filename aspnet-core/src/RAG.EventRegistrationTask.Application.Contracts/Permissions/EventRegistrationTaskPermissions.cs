namespace RAG.EventRegistrationTask.Permissions;

public static class EventRegistrationTaskPermissions
{
    public const string GroupName = "EventRegistrationTask";

    //Product Group & Permissions
    public const string EventGroupName = GroupName + ".Event";
    public const string CreateEditEventPermission = EventGroupName + ".CreateEdit";
    public const string DeleteEventPermission = EventGroupName + ".Delete";
    public const string GetEventPermission = EventGroupName + ".Get";
    public const string ListEventPermission = EventGroupName + ".List";
    public const string ActiveEventPermission = EventGroupName + ".Active";

    public const string EventRegistratioGroupName = GroupName + ".EventRegistration";

    public const string EventRegistrationView = EventRegistratioGroupName + ".View";
    //Add your own permission names. Example:
    //public const string MyPermission1 = GroupName + ".MyPermission1";
}
