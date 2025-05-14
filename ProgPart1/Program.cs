using System;
using System.Media;
using System.Threading;
using System.Collections.Generic;
using System.Text.RegularExpressions;

class CybersecurityChatbot
{
    static Dictionary<string, List<string>> keywordResponses = new Dictionary<string, List<string>>()
{
    { "password", new List<string> {
        "🔑 Always use strong, unique passwords and enable two-factor authentication!",
        "🛡️ Never reuse passwords across different accounts. Consider using a password manager.",
        "🔒 Make sure your passwords are long, complex, and avoid using personal information."
    }},
    { "scam", new List<string> {
        "🚨 Always verify requests for personal information. Scammers often pretend to be trusted sources.",
        "⚠️ Be cautious of offers that seem too good to be true. They usually are!",
        "🔎 Double-check email addresses and phone numbers for legitimacy before responding."
    }},
    { "privacy", new List<string> {
        "🕵️ Limit what you share online. Always review privacy settings on social media accounts.",
        "🔐 Use encrypted messaging apps and secure browsers to protect your online activities.",
        "🛡️ Avoid oversharing personal information, especially your location and financial details."
    }},
    { "phishing", new List<string> {
        "⚠️ Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organisations.",
        "📧 Always verify the sender's email address carefully. Look for subtle misspellings.",
        "🔗 Hover over links before clicking to ensure they direct to legitimate sites."
    }},
    { "safe browsing", new List<string> {
        "🌍 Use HTTPS websites, avoid clicking unknown links, and update your browser regularly.",
        "🧭 Keep your browser extensions minimal and from trusted sources to prevent tracking.",
        "🔐 Clear your cookies and browsing history often to maintain privacy."
    }},
    { "social engineering", new List<string> {
        "🚨 Social engineering manipulates users into revealing confidential info. Always verify requests!",
        "👀 Be cautious of urgent messages pressuring immediate action—they’re often traps.",
        "💬 Never share login details over phone or text unless you're 100% sure of the source."
    }},
    { "encryption", new List<string> {
        "🔐 Encryption scrambles your data to make it unreadable without a key. Always encrypt sensitive information.",
        "🧩 Use end-to-end encrypted services when sharing private information.",
        "🔒 File and disk encryption is essential for protecting data on lost or stolen devices."
    }},
    { "two-factor authentication", new List<string> {
        "🔒 Two-Factor Authentication (2FA) adds an extra layer of security to your accounts. Always enable it!",
        "📱 Use an authenticator app for stronger protection than SMS-based codes.",
        "🛡️ 2FA protects your account even if your password gets compromised."
    }},
    { "2fa", new List<string> {
        "🔒 Two-Factor Authentication (2FA) adds an extra layer of security to your accounts. Always enable it!",
        "📱 Use an authenticator app for stronger protection than SMS-based codes.",
        "🛡️ 2FA protects your account even if your password gets compromised."
    }},
    { "public wi-fi", new List<string> {
        "⚠️ Public Wi-Fi is often unsecured. Avoid accessing sensitive accounts when connected to public networks.",
        "📡 Use a VPN when you must connect to public Wi-Fi for safer browsing.",
        "🔒 Disable auto-connect features to avoid joining rogue networks automatically."
    }},
    { "free wifi", new List<string> {
        "⚠️ Public Wi-Fi is often unsecured. Avoid accessing sensitive accounts when connected to public networks.",
        "📡 Use a VPN when you must connect to public Wi-Fi for safer browsing.",
        "🔒 Disable auto-connect features to avoid joining rogue networks automatically."
    }}
};


    static string userName = "";
    static string favoriteTopic = "";

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Start the greeting sound
        PlayVoiceGreeting(@"C:\Users\adivh\source\repos\ProgPart1\ProgPart1\ProgGreet.wav");

        Console.ForegroundColor = ConsoleColor.Cyan;
        DispAsciiArt(); 

        ChatbotInteraction(); 
    }


    static void PlayVoiceGreeting(string filePath)
    {
        try
        {
            SoundPlayer player = new SoundPlayer(filePath);
            player.Play();
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

        foreach (char c in asciiArt)
        {
            Console.Write(c);
            Thread.Sleep(3); 
        }
    }

    static void ChatbotInteraction()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;

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
            string userInput = Console.ReadLine()?.ToLower();
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

            // Error handling, sentiment detection, keyword detection
            string sentimentResponse = DetectSentiment(userInput);
            string keywordResponse = GenResponse(userInput);

            if (!string.IsNullOrEmpty(sentimentResponse))
            {
                SimulateTyping(sentimentResponse);
            }

            SimulateTyping(keywordResponse);

            if (!string.IsNullOrEmpty(favoriteTopic))
            {
                Console.Write("\n🧠 Would you like a tip related to your interest in " + favoriteTopic + "? (yes/no): ");
                Console.ForegroundColor = ConsoleColor.Yellow;
                string followUp = Console.ReadLine()?.ToLower();
                Console.ForegroundColor = ConsoleColor.Cyan;

                if (followUp == "yes" || followUp == "y")
                {
                    ProvideFavoriteTopicTip();
                }
            }
        }
    }

    static string GenResponse(string input)
    {
        foreach (var keyword in keywordResponses.Keys)
        {
            if (input.Contains(keyword))
            {
                if (string.IsNullOrEmpty(favoriteTopic))
                {
                    favoriteTopic = keyword;
                    return $"🤖 Great! I'll remember that you're interested in {keyword}. It's a crucial part of staying safe online.";
                }

                Random rand = new Random();
                var responses = keywordResponses[keyword];
                int index = rand.Next(responses.Count);
                return responses[index];
            }
        }

        // Basic known queries
        if (input.Contains("how are you"))
            return "🤖 I am just a bot, but I am functioning optimally!";

        if (input.Contains("your purpose"))
            return "🤖 I am here to educate you about online safety and cybersecurity best practices.";

        // Error handling: unknown input
        return "🤖 I'm not sure I understand. Can you try rephrasing?";
    }

    // ✳️ Sentiment Detection 
    static string DetectSentiment(string input)
    {
        if (Regex.IsMatch(input, @"\b(worried|anxious|scared|nervous|afraid|frustrated|unsure)\b"))
        {
            return "🤖 It's completely understandable to feel that way. Cyber threats can be overwhelming. Let me share some tips to help you stay safe.";
        }
        return "😊 Great question! It's always a good idea to stay informed about online safety. I'm here to help!";
    }


    static void ProvideFavoriteTopicTip()
    {
        if (keywordResponses.ContainsKey(favoriteTopic))
        {
            Random rand = new Random();
            var responses = keywordResponses[favoriteTopic];
            int index = rand.Next(responses.Count);
            SimulateTyping($"🔔 As someone interested in {favoriteTopic}, here's a tip: {responses[index]}");
        }
    }

    static void SimulateTyping(string message)
    {
        foreach (char c in message)
        {
            Console.Write(c);
            Thread.Sleep(28);
        }
        Console.WriteLine();
    }
}
