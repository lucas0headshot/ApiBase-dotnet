using ApiBase.Core.Common.DTOs;
using ApiBase.Core.Common.Extensions;
using ApiBase.Core.Common.Projection;
using ApiBase.Core.Common.Query;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ApiBase.Core.Api.Controllers.Base
{
    public class BaseApiController<TApplication> : ControllerBase
    {
        protected TApplication Application { get; set; }

        protected BaseApiController(TApplication application)
        {
            Application = application;
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public IActionResult Return<TResponse>(HttpStatusCode statusCode, TResponse response)
        {
            return new ObjectResult(response)
            {
                StatusCode = (int)statusCode
            };
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("ReturnErroWithObject")]
        public IActionResult ReturnError(ApiErrorResponse response)
        {
            return BadRequest(response);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("ReturnError")]
        public IActionResult ReturnError(string message = "", object content = null, int? errorCode = null)
        {
            if (errorCode.HasValue)
            {
                var error = new ApiErrorResponse
                {
                    Message = message,
                    Content = content,
                    ErrorType = "AppError" + errorCode
                };
                return ReturnError(error);
            }

            return ReturnError(new ApiErrorResponse
            {
                Message = message,
                Content = content
            });
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("ReturnErrorFromException")]
        public IActionResult ReturnError(Exception ex)
        {
            return ReturnError(ex.FlattenMessage());
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("ReturnSuccessWithObject")]
        public IActionResult ReturnSuccess(ApiSuccessResponse response)
        {
            return Ok(response);
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        [HttpGet("ReturnSuccess")]
        public IActionResult ReturnSuccess(string message = "", object content = null, int? requestCode = null)
        {
            return ReturnSuccess(new ApiSuccessResponse
            {
                Message = message,
                Content = content ?? new { },
                RequestCode = requestCode
            });
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public static QueryExecutionDTO BuildPaginatedQueryResult<T>(QueryParams queryParams, IQueryable<T> query) where T : class
        {
            var result = BuildQueryResult(queryParams, query);
            var paged = Paginate(result.Data, queryParams.page.GetValueOrDefault(), queryParams.limit.GetValueOrDefault(25));

            return new QueryExecutionDTO
            {
                Data = paged.AsQueryable(),
                Total = result.Total
            };
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public static QueryExecutionDTO BuildQueryResult<T>(QueryParams queryParams, IQueryable<T> query) where T : class
        {
            query = ApplyOrdering(queryParams, query);
            IQueryable<object> resultQuery;

            var fields = queryParams.GetFields();

            if (fields.Count > 0)
            {
                var propertyDict = typeof(T)
                    .GetProperties()
                    .Where(p => fields.Contains(p.Name))
                    .ToDictionary(p => p.Name, p => p.PropertyType);

                Type dynamicType = CustomTypeBuilder.CreateType(propertyDict);

                var selector = new ProjectionBuilder().Build<T>(dynamicType);
                resultQuery = query.Select(selector);
            }
            else
            {
                resultQuery = query.Cast<object>();
            }

            int total = resultQuery.Count();

            return new QueryExecutionDTO
            {
                Data = resultQuery,
                Total = total
            };
        }

        private static IQueryable<T> ApplyOrdering<T>(QueryParams queryParams, IQueryable<T> query)
        {
            var orderList = queryParams.GetSort() ?? new List<SortModel>
            {
                new SortModel { filterValue = "Id", direction = "asc" }
            };

            return new OrderByQuery().ApplySorting(query, orderList);
        }

        private static List<object> Paginate(IQueryable<object> query, int page, int limit)
        {
            return query.Skip((page - 1) * limit).Take(limit).ToList();
        }
    }
}
