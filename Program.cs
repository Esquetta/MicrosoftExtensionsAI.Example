using Microsoft.Extensions.AI;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


#region Basic Chat Completion
//var builder = Host.CreateApplicationBuilder();

//builder.Services.AddChatClient(new OllamaChatClient(new Uri("http://localhost:11434"),"deepseek-r1"));

//var app = builder.Build();

//var chatClient = app.Services.GetRequiredService<IChatClient>();

//var chatCompletion = await chatClient.GetResponseAsync("1+213= ?");

//Console.WriteLine(chatCompletion.Text);
#endregion

#region chat with history

var builder = Host.CreateApplicationBuilder();
builder.Services.AddChatClient(new OllamaChatClient(new Uri("http://localhost:11434"), "deepseek-r1"));

var app = builder.Build();

var chatClient = app.Services.GetRequiredService<IChatClient>();

var chatHistory = new List<ChatMessage>();

while (true)
{
    Console.Write("Söyle: ");
    var prompt = Console.ReadLine();

    chatHistory.Add(new ChatMessage(ChatRole.User, prompt));
    var chatResponse = string.Empty;
    await foreach (var response in chatClient.GetStreamingResponseAsync(chatHistory))
    {
        Console.WriteLine(response.Text);
        chatResponse += response.Text;
    }
    chatHistory.Add(new ChatMessage(ChatRole.Assistant, chatResponse));
    Console.WriteLine();
}
Console.Read();

#endregion