using Microsoft.JSInterop;

namespace OpenAlexBrowser.App;

internal class FilesInterop: IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;

    public FilesInterop(IJSRuntime jsRuntime)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/OpenAlexBrowser.App/filesJsInterop.js").AsTask());
    }

    public async ValueTask DownloadFile(string fileName, Stream fileStream)
    {
        var module = await moduleTask.Value;
        using var streamRef = new DotNetStreamReference(stream: fileStream);

        await module.InvokeVoidAsync("downloadFileFromStream", fileName, streamRef);
    }

    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            var module = await moduleTask.Value;
            await module.DisposeAsync();
        }
    }
}
