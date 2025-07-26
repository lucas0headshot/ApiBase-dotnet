using System.ComponentModel;

namespace ApiBase.Core.src.Domain.Entities
{
    public class IdentifierGuid
    {
        public Guid Id { get; set; }

        public override string ToString()
        {
            return Id.ToString();
        }

        public string GetIdentificacaoPersonalizacao()
        {
            object[] customAttributes = GetType().GetCustomAttributes(typeof(DescriptionAttribute), inherit: true);
            if (customAttributes.Count() == 0)
            {
                return null;
            }

            try
            {
                return ((DescriptionAttribute)customAttributes[0]).Description;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
