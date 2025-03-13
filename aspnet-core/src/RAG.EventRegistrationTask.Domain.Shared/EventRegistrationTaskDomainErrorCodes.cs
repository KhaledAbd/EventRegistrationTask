namespace RAG.EventRegistrationTask;

public static class EventRegistrationTaskDomainErrorCodes
{
    public const string NAMESPACE = "EventRegistrationTask.Events:";

    /* You can add your business exception error codes here, as constants */
    public const string INVALID_EVENT_NAME_EN = NAMESPACE + "InvalidNameEn";
    public const string INVALID_EVENT_NAME_AR = NAMESPACE + "InvalidNameAr";
    public const string INVALID_EVENT_CAPACITY = NAMESPACE + "InvalidCapacity";
    public const string INVALID_EVENT_START_DATE = NAMESPACE + "InvalidStartDate";
    public const string INVALID_EVENT_END_DATE = NAMESPACE + "InvalidEndDate";
    public const string INVALID_EVENT_LINK = NAMESPACE + "InvalidLink";
    public const string INVALID_EVENT_ORGANIZER = NAMESPACE + "InvalidOrganizer";
    public const string INVALID_EVENT_STREET = NAMESPACE + "InvalidStreet";
    public const string INVALID_EVENT_GOVERNMENT = NAMESPACE + "InvalidGovernment";
    public const string INVALID_EVENT_CITY = NAMESPACE + "InvalidCity";

    public const string Event_NOT_FOUND = NAMESPACE + "Event_NOT_FOUND";
    public const string Event_NOT_AVAILABLE_CAPACITY = NAMESPACE + "Event_NOT_AVAILABLE_CAPACITY";

    public const string EVENT_EXECEED_PERIOD_CANCALATION = NAMESPACE + "Event_Execeed_Period_Cancaltion";
}
