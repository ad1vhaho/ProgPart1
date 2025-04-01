using System;
using System.Media;
using System.Threading;

class CybersecurityChatbot
{
    static void Main()
    {
        // Play voice greeting
        PlayVoiceGreeting(@"C:\Users\adivh\source\repos\ProgPart1\ProgPart1\ProgGreet.wav"); 

        // console colors
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Clear(); // Clear screen for a fresh look

        // Display Ascii art
        DispAsciiArt();

        // Start chatbot interaction 
        ChatbotInteraction();
    }

    static void PlayVoiceGreeting(string filePath)
    {
        try
        {
            SoundPlayer player = new SoundPlayer(filePath);
            player.Play(); // Play the sound without delay.
        }
        catch (Exception e)
        {
            Console.WriteLine("[Error] Unable to play voice greeting: " + e.Message);
        }
    }

    static void DispAsciiArt()
    {
        string asciiArt = @"  
  CCCC    RRRR    Y   Y   PPPP   TTTTT   OOO    BBBBB    OOO   TTTTT  
 C        R   R    Y Y    P   P    T    O   O   B    B  O   O    T    
 C        RRRR      Y     PPPP     T    O   O   BBBBB   O   O    T
 C        R  R      Y     P        T    O   O   B    B  O   O    T
  CCCC    R   R     Y     P        T     OOO    BBBBB    OOO     T  
";
        Console.WriteLine(asciiArt);
    }

    static void ChatbotInteraction()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        string userName = string.Empty;

        // Ask for user's name and validate input
        while (string.IsNullOrWhiteSpace(userName))
        {
            Console.Write("👤 Enter your name: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            userName = Console.ReadLine();
            Console.ForegroundColor = ConsoleColor.Cyan;

            if (string.IsNullOrWhiteSpace(userName))
            {
                Console.WriteLine("🤖 Please enter your name so we can proceed.");
            }
        }

        Console.WriteLine($"\n💬 Welcome {userName}!, I am here to help you stay safe online.\n");

        while (true)
        {
            Console.Write("\n💡 Ask me anything about cybersecurity or type 'exit' to quit: ");
            Console.ForegroundColor = ConsoleColor.Yellow;
            string userInput = Console.ReadLine().ToLower();
            Console.ForegroundColor = ConsoleColor.Cyan;

            if (string.IsNullOrWhiteSpace(userInput))
            {
                Console.WriteLine("🤖 I didn’t quite understand that. Could you please ask your question again?");
                continue;
            }

            if (userInput == "exit")
            {
                Console.WriteLine("\n🔒 Goodbye! Stay safe online! 🔒");
                break;
            }

            // Simulates typing effect
            SimulateTyping(GenResponse(userInput));
        }
    }

    static string GenResponse(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return "🤖 I didn’t quite understand that. Could you please rephrase?";

        if (input.Contains("how are you"))
            return "🤖 I am just a bot, but I am functioning optimally!";

        if (input.Contains("your purpose"))
            return "🤖 I am here to educate you about online safety and cybersecurity best practices.";

        if (input.Contains("password safety"))
            return "🔑 Always use strong, unique passwords and enable two-factor authentication!";

        if (input.Contains("phishing"))
            return "⚠️ Beware of suspicious emails and links. Never share sensitive information online!";

        if (input.Contains("safe browsing"))
            return "🌍 Use HTTPS websites, avoid clicking unknown links, and update your browser regularly.";

        if (input.Contains("social engineering"))
            return "🚨 Social engineering manipulates users into revealing confidential info. Always verify requests!";

        if (input.Contains("encryption"))
            return "🔐 Encryption scrambles your data to make it unreadable without a key. Always encrypt sensitive information.";

        if (input.Contains("two-factor authentication") || input.Contains("2fa"))
            return "🔒 Two-Factor Authentication (2FA) adds an extra layer of security to your accounts. Always enable it!";

        if (input.Contains("public wi-fi") || input.Contains("free wifi"))
            return "⚠️ Public Wi-Fi is often unsecured. Avoid accessing sensitive accounts when connected to public networks.";


        return "🤖 That's an interesting question! Could you specify what you’d like to learn?";
    }

    static void SimulateTyping(string message)
    {
        foreach (char c in message)
        {
            Console.Write(c);
            Thread.Sleep(28); //setting the speed of typing effect
        }
        Console.WriteLine();
    }
}
