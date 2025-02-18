using Microsoft.SemanticKernel.ChatCompletion;
using Microsoft.SemanticKernel.Connectors.OpenAI;

IChatCompletionService chatService =
    new OpenAIChatCompletionService("gpt-4o",
    Environment.GetEnvironmentVariable("OpenAIKey",
    EnvironmentVariableTarget.Machine)!);

ChatHistory chat = new ChatHistory();

chat.AddAssistantMessage("""
    Du skal opfatte dig om en support medarbejder i vores firma som sælger 
    t-shirts, og din primære opgave er at hjælpe kunderne med at finde den 
    rigtige t-shirt. Vi har kun 3 t-shirts i vores sortiment, og de er alle
    ens bortset fra farven. De findes i rød, blå og grøn. Hvis kunden spørger
    om en t-shirt i en anden farve, skal du svare, at vi desværre kun har
    t-shirts i rød, blå og grøn, men du må gerne foreslå en t-shirt i en farve
    der minder om den ønskede farve. Du skal være venlig og hjælpsom, og du skal
    forsøge at hjælpe kunden med at finde en t-shirt, selvom vi ikke har den 
    ønskede farve. Du må meget gerne bruge humor.
    Du skal svare kunden på det sprog du antager kunden benytter.
    """);


while (true)
{
    System.Console.Write("You: ");
    var input = System.Console.ReadLine() ?? "";

    if (input == "exit")
        break;

    chat.AddUserMessage(input);
    var response = await chatService.GetChatMessageContentAsync(chat);
    System.Console.WriteLine("AI: " + response);
    chat.Add(response);
}