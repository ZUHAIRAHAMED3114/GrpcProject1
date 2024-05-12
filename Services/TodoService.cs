using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using ToDoGrpc.Data;
using ToDoGrpc.Models;
using ToDoGrpc.Protos;

namespace ToDoGrpc.Services
{
    public class TodoService : TodoIt.TodoItBase
    {
        private readonly AddDbContext _dbContext;

        public TodoService(AddDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<CreateToDoResponse> CreateToDo(CreateToDoRequest request, ServerCallContext context) {
            if (request.Title == string.Empty || request.Description == string.Empty)
                throw new RpcException(new Status(StatusCode.InvalidArgument, "You Must Supply a Valid Object"));

            var toDoItem = new TodoItem
            {
                Title = request.Title,
                Description = request.Description,
            };

            await _dbContext.AddAsync(toDoItem);
            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(new CreateToDoResponse { Id = toDoItem.Id }); 

        }

        public override Task<GetAllResponse> ListToDo(GetAllRequest request, ServerCallContext context)
        {
            var response = new GetAllResponse();

            // Fetch todo items from the database
            var todoItems = _dbContext.TodoItems;

            foreach (var todoItem in todoItems)
            {
                response.ToDo.Add(new ReadToDoResponse
                {
                    Id = todoItem.Id,
                    Title = todoItem.Title,
                    Descripton = todoItem.Description,
                    ToDoStatus = todoItem.ToDoStatus
                });
            }

            return Task.FromResult(response);
        }

        public override Task<ReadToDoResponse> ReadToDo(ReadToDoRequst request, ServerCallContext context)
        {
            return base.ReadToDo(request, context);
        }

        public override Task<UpdateToDoReponse> UpdateToDo(UpdateToDoRequest request, ServerCallContext context)
        {
            return base.UpdateToDo(request, context);
        }

        public override Task<DeleteToDoResponse> DeleteToDo(DeleteToDoRequst request, ServerCallContext context)
        {
            return base.DeleteToDo(request, context);
        }

    }
}
