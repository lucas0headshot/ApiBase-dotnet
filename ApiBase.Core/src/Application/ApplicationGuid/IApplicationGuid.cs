using ApiBase.Core.src.Domain.View;
using ApiBase.Core.src.Infra.Query;

namespace ApiBase.Core.src.Application.ApplicationGuid
{
    public interface IApplicationGuid<TView> where TView : IdGuidView, new()
    {
        ConsultationView Get(QueryParams queryParams);
        object Get(Guid id);
        object Get(Guid id, List<string> fields);
    }
}
