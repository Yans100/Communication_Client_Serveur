using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace SocketServer.Services
{
    internal class ValidationService : IValidationService
    {
        public bool Validate(object obj)
        {
            var props = obj.GetType().GetProperties();

            foreach (var prop in props)
            {
                var att = prop.GetCustomAttribute<StringLengthAttribute>();
                if (att is null)
                    continue;

                if (!att.IsValid(prop.GetValue(obj)))
                    return false;
            }

            return true;
        }
    }
}
