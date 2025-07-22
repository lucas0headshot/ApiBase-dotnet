using System.Runtime.Serialization;

namespace Core.Exceptions
{
    [Serializable]
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityName, object key)
             : base($"{entityName} with key '{key}' was not found.") { }

        protected EntityNotFoundException(SerializationInfo info, StreamingContext context)
            : base(info, context) { }

    }
}
