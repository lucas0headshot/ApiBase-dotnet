using ApiBase.Api.Base;
using ApiBase.Core.Application.ApplicationGuid;
using ApiBase.Core.Domain.DTOs;
using ApiBase.Core.Domain.View;
using ApiBase.Core.Infra.Query;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiBase.Api.BaseGuid
{
    public class ControllerGuid<TApplication, TView> : BaseApiController<TApplication> where TApplication : IApplicationGuid<TView> where TView : IdGuidView, new()
    {
        protected ControllerGuid(TApplication application) : base(application) { }

        [HttpGet]
        public virtual IActionResult GetList([FromQuery] QueryParams queryParams)
        {
            try
            {
                var result = base.Application.Get(queryParams);

                return Respond(HttpStatusCode.OK, new ApiPaginatedResponse
                {
                    Content = result.Content,
                    Total = result.Total
                });
            }
            catch (Exception ex)
            {
                return RespondError(ex);
            }
        }

        [HttpGet("{id}")]
        public virtual IActionResult GetById([FromRoute] Guid id, [FromQuery] QueryField queryField)
        {
            try
            {
                List<string> list = queryField.GetFields();
                object content = (list.Any() ? base.Application.Get(id) : base.Application.Get(id, list));

                return Respond(HttpStatusCode.OK, content);
            }
            catch (Exception ex)
            {
                return RespondError(ex);
            }
        }
    }
}
