using Application.Api.Filters;
using Application.Contracts.Models;
using Business_Logic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace Application.Api.Controllers
{
    /// <summary>
    /// методы работы с ToDo
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    [TypeFilter(typeof(ApiResultLoggerFilter))]
    public class TodoController : ControllerBase
    {
        private readonly ILogger<TodoController> _logger;
        private readonly ITodoService _todoService;

        public TodoController(ILogger<TodoController> logger, ITodoService todoService)
        {
            _logger = logger;
            _todoService = todoService;
        }

        /// <summary>
        /// получить все записи и комментарии к ним
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<GetAllRowsResponse> GetAllRows(CancellationToken token)
            => _todoService.GetAllRowsAsync(token);
        /// <summary>
        /// добавить новую запись
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public Task<long> InsertNewRow(InsertNewRowRequest request, CancellationToken token) =>
            _todoService.InsertNewRowAsync(request, token);
        /// <summary>
        /// получить запись по ее ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<GetRowDyIdResponse> GetRowById(long id, CancellationToken token) =>
            _todoService.GetRowByIdAsync(id, token);
        /// <summary>
        /// удалить запись по ее ID
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpDelete]
        public Task DeleteRowById(long id, CancellationToken token) => 
            _todoService.DeleteRowByIdAsync(id, token);
        /// <summary>
        /// обновить заголовок к записи по ее ID
        /// </summary>
        /// <param name="request"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPut]
        public Task UpdateHeaderByRowId(UpdateHeaderByRowIdRequest request, CancellationToken token) =>
            _todoService.UpdateHeaderByRowIdAsync(request, token);
        /// <summary>
        /// получить все комментарии по ID строки
        /// </summary>
        /// <param name="id"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet]
        public Task<GetCommentByRowIdResponse> GetCommentsByRowId(long id, CancellationToken token) =>
            _todoService.GetCommentsByRowIdAsync(id, token);
        /// <summary>
        /// добавить новый комментарий к строке 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="comment"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public Task AddNewCommentByRowId(long id, string comment, CancellationToken token)
            => _todoService.AddNewCommentByRowIdAsync(id, comment, token);

    }
}
