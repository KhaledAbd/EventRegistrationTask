using RAG.EventRegistrationTask.Samples;
using Xunit;

namespace RAG.EventRegistrationTask.EntityFrameworkCore.Domains;

[Collection(EventRegistrationTaskTestConsts.CollectionDefinitionName)]
public class EfCoreSampleDomainTests : SampleDomainTests<EventRegistrationTaskEntityFrameworkCoreTestModule>
{

}
