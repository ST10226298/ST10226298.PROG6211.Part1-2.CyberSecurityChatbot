using System;

namespace CyberSecurityChatbot.Logic
{
    public class RandomResponder
    {
        private readonly string[] _fallbackResponses;
        private readonly Random _random;

        public RandomResponder()
        {
            _fallbackResponses = new[]
            {
                "Can you tell me more about that?",
                "That's interesting! Want to hear a tip about online safety?",
                "I'm not sure I understood, but I can give you a cybersecurity tip!",
                "Cybersecurity is important! Let me know if you want some tips.",
                "Feel free to ask me about phishing, passwords, or online scams."
            };
            _random = new Random();
        }

        public string GetRandomResponse()
        {
            int index = _random.Next(_fallbackResponses.Length);
            return _fallbackResponses[index];
        }
    }
}
