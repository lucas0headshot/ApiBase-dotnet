using ApiBase.Domain.Query;
using ApiBase.Domain.View;
using ApiBase.Infra.Query;

namespace ApiBase.Application.ApplicationGuid
{
    public interface IApplicationGuid<TView> where TView : IdGuidView, new()
    {
        GetView Get(QueryParams queryParams);
        object Get(Guid id);
        object Get(Guid id, List<string> fields);
    }
}
