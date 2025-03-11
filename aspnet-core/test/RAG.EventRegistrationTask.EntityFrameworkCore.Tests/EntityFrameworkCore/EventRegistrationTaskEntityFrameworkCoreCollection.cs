﻿using Xunit;

namespace RAG.EventRegistrationTask.EntityFrameworkCore;

[CollectionDefinition(EventRegistrationTaskTestConsts.CollectionDefinitionName)]
public class EventRegistrationTaskEntityFrameworkCoreCollection : ICollectionFixture<EventRegistrationTaskEntityFrameworkCoreFixture>
{

}
