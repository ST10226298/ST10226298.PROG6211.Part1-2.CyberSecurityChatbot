using System;
using System.IO;
using System.Media;
using System.Threading.Tasks;
using CyberSecurityChatbot.Logic;

namespace CyberSecurityChatbot
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            // Play welcome audio asynchronously
            var playAudioTask = Task.Run(() => PlayWelcomeAudio());

            // Display ASCII art immediately
            ShowAsciiArt();

            // Wait for audio to finish before continuing
            playAudioTask.Wait();

            // Ask for user name
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\nPlease enter your name: ");
            Console.ResetColor();
            string userName = Console.ReadLine() ?? "User";

            // Initialize memory and sentiment components
            var memoryManager = new MemoryManager();
            var sentimentAnalyzer = new SentimentAnalyzer();

            // Create conversation manager with userName
            var conversationManager = new ConversationManager(userName, memoryManager, sentimentAnalyzer);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"\nHi {userName}, I'm your Cybersecurity Awareness Chatbot. Type 'exit' to leave at any time.");
            Console.WriteLine("You can ask me about topics like 'password', 'phishing', 'privacy', 'vpn', 'malware', '2fa', 'scam', 'firewall', 'breach', or 'encryption'.\n");

            Console.ResetColor();

            // Main chat loop
            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("You: ");
                Console.ResetColor();

                string? userInput = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(userInput))
                    continue;

                if (userInput.Trim().Equals("exit", StringComparison.OrdinalIgnoreCase))
                    break;

                string response = conversationManager.GenerateResponse(userInput);

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Bot: " + response + "\n");
                Console.ResetColor();
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Goodbye! Stay safe online.");
            Console.ResetColor();
        }

        static void PlayWelcomeAudio()
        {
            string audioPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "welcome.wav");
            if (File.Exists(audioPath))
            {
                using (var player = new SoundPlayer(audioPath))
                {
                    player.PlaySync();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[Audio file not found]");
                Console.ResetColor();
            }
        }

        static void ShowAsciiArt()
        {
            string artPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "ascii_art.txt");
            if (File.Exists(artPath))
            {
                string asciiArt = File.ReadAllText(artPath);
                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.WriteLine(asciiArt);
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("[ASCII art not found]");
                Console.ResetColor();
            }
        }
    }
}
