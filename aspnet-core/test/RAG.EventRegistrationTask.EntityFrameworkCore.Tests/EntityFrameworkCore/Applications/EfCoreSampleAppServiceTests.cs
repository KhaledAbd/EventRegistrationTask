using RAG.EventRegistrationTask.Samples;
using Xunit;

namespace RAG.EventRegistrationTask.EntityFrameworkCore.Applications;

[Collection(EventRegistrationTaskTestConsts.CollectionDefinitionName)]
public class EfCoreSampleAppServiceTests : SampleAppServiceTests<EventRegistrationTaskEntityFrameworkCoreTestModule>
{

}
