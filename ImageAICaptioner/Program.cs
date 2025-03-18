using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.AI;
using Microsoft.Extensions.Hosting;


var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services => 
    {
        services.AddChatClient(
            new OllamaChatClient(new Uri("http://localhost:11434"), "llava:7b"));
    })
    .Build();

var chatClient = host.Services.GetRequiredService<IChatClient>();

var imagesFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "images");
if (!Directory.Exists(imagesFolder))
{
    Console.WriteLine("No 'images' folder found! Add one with some images.");
    return;
}

var jpgFiles = Directory.GetFiles(imagesFolder, "*.jpg", SearchOption.TopDirectoryOnly);
var pngFiles = Directory.GetFiles(imagesFolder, "*.png", SearchOption.TopDirectoryOnly);
var imageFiles = jpgFiles.Concat(pngFiles).ToArray();

if (imageFiles.Length == 0)
{
    Console.WriteLine("No images in the 'images' folder. Add some and try again!");
    return;
}

Console.WriteLine("Welcome to the Image AI Caption Generator!");
Console.WriteLine("\nChoose an image to caption:");

for (var i = 0; i < imageFiles.Length; i++)
{
    Console.WriteLine($"{i + 1}. {Path.GetFileName(imageFiles[i])}");
}

Console.Write("\nEnter the number of your choice: ");

if (!int.TryParse(Console.ReadLine(), out var choice) 
    || choice < 1 || choice > imageFiles.Length)
{
    Console.WriteLine("Invalid choice. Exiting!");
    return;
}

var selectedImage = imageFiles[choice - 1];

try
{
    var prompt = new ChatMessage(ChatRole.User, "Describe this image in one sentence.");
    
    prompt.Contents.Add(
        new DataContent(
            File.ReadAllBytes(selectedImage),
                Path.GetExtension(selectedImage).ToLower() == ".png" ? "image/png" : "image/jpeg"));

    var response = await chatClient.GetResponseAsync(prompt);
    Console.WriteLine($"\nCaption: {response.Messages[0].Text}");
}
catch (Exception ex)
{
    Console.WriteLine($"Oops, something failed: {ex.Message}");
}

Console.WriteLine("Press any key to exit...");
Console.ReadKey();

