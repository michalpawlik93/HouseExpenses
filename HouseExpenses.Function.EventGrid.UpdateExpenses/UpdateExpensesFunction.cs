// Default URL for triggering event grid function in the local environment.
// http://localhost:7071/runtime/webhooks/EventGrid?functionName={functionname}
using HouseExpenses.Data.Services.Interfaces;
using HousExpenses.Domain.Events;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

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
        public async Task Run([EventGridTrigger] HouseUpdated @event)
        {
            _logger.LogInformation("Event received {HouseId} {subject}", @event.HouseId);
            var expenses = await _expensesCoreApiService.GetByHouseId(@event.HouseId);
            if (expenses.Any())
            {
                _logger.LogInformation("Expenses count to update: {expensesCount}", expenses.Count());
                var house = await _houseCoreApiService.GetById(@event.HouseId.ToString());
                if (house != null)
                {
                    foreach(var expense in expenses)
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
    //https://github.com/pmcilreavy/AzureEventGridSimulator
}
