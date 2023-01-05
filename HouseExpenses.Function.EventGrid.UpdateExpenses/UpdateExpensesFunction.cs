// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using Azure.Messaging.EventGrid;
using HouseExpenses.Data.Models;
using HouseExpenses.Data.Services.Interfaces;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace HouseExpenses.Function.EventGrid.UpdateExpenses
{
    public class UpdateExpensesFunction
    {
        private readonly ILogger _logger;
        public readonly IExpensesCoreApiService _expensesCoreApiService;
        public readonly IHouseCoreApiService _houseCoreApiService;

        public UpdateExpensesFunction(ILoggerFactory loggerFactory, IExpensesCoreApiService expensesCoreApiService,
            IHouseCoreApiService houseCoreApiService)
        {
            _logger = loggerFactory.CreateLogger<UpdateExpensesFunction>();
            _expensesCoreApiService = expensesCoreApiService;
            _houseCoreApiService = houseCoreApiService;
        }

        [Function("UpdateExpensesOnHouseUpdate")]
        public async Task Run([EventGridTrigger] EventGridEvent @event)
        {
            _logger.LogInformation(JsonSerializer.Serialize(@event));
            var houseDao = JsonSerializer.Deserialize<HouseDao>(@event.Data.ToStream());
            if(houseDao != null)
            {
                _logger.LogInformation("Event received {HouseId} {subject}", houseDao.Id);
                var expenses = await _expensesCoreApiService.GetByHouseId(houseDao.Id);
                if (expenses.Any())
                {
                    _logger.LogInformation("Expenses count to update: {expensesCount}", expenses.Count());
                    var house = await _houseCoreApiService.GetById(houseDao.Id.ToString());
                    if (house != null)
                    {
                        foreach (var expense in expenses)
                        {
                            var jobHouse = expense.Jobs.First(j => j.House.Id == house.Id);
                            jobHouse.House = house;
                            await _expensesCoreApiService.UpdateAsync(expense.Id.ToString(), expense);
                            _logger.LogInformation("Expense updated. Id: {expenseId}", expense.Id);
                        }
                    }
                }
            }
        }
    }
}
