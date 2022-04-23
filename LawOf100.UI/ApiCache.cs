using Microsoft.AspNetCore.Components;

namespace LawOf100.UI;

public class ApiCache : IDisposable
{
    public PersistentComponentState State { get; }

    public Dictionary<string, object> Results { get; set; } = new();
    public PersistingComponentStateSubscription Subscription { get; private set; }

    public ApiCache(PersistentComponentState state) => State = state;

    public async Task<T> Resolve<T>(Func<Task<T>> apiCall) where T : class
    {
        // this will need to be moved up the hierarchy
        var key = apiCall.Method.Name;
        T data;

        Subscription = State.RegisterOnPersisting(() => OnPersisting<T>(key));

        if (!State.TryTakeFromJson<T>(key, out var restored))
        {
            data = await apiCall();
        }
        else
        {
            data = restored!;
        }

        if (Results.ContainsKey(key))
            Results[key] = data;
        else
            Results.Add(key, data);
        
        return data;
    }

    private Task OnPersisting<T>(string key) where T : class
    {
        State.PersistAsJson(key, Results[key] as T);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        Subscription.Dispose();
    }
}
