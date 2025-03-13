using System;

namespace RAG.EventRegistrationTask.Events.ValueObjects
{
    // Value Object: Capacity
    public class Capacity
    {
        public int? Value { get; private set; }

        public Capacity() { }
        public Capacity(int? value)
        {
            if (value.HasValue && value.Value <= 0)
            {
                throw new ArgumentException("Capacity must be greater than 0.");
            }

            Value = value;
        }

        public bool IsFull(int currentRegistrationsCount) => Value.HasValue && currentRegistrationsCount >= Value.Value;
    }
}
