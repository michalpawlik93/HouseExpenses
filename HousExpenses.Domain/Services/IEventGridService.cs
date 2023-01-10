namespace HousExpenses.Domain.Services;

internal interface IEventGridService
{
    public Task Publish();
}
