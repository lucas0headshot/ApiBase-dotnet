using ApiBase.Core.Domain.Query;
using ApiBase.Core.Domain.View;
using ApiBase.Core.Infra.Query;

namespace ApiBase.Core.Application.ApplicationGuid
{
    public interface IApplicationGuid<TView> where TView : IdGuidView, new()
    {
        GetView Get(QueryParams queryParams);
        object Get(Guid id);
        object Get(Guid id, List<string> fields);
    }
}
