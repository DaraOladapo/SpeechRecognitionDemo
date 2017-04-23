using System;
using Microsoft.CognitiveServices.SpeechRecognition;

namespace SpeechRecognitionDemo
{
    class Program
    {
        static void Main()
        {
            // Initialization
            //var micClient = SpeechRecognitionServiceFactory.CreateMicrophoneClient(SpeechRecognitionMode.LongDictation, "en-US", "04a349ccb0bd4c4eb6d026aa5b363a7a");
            var micClient = SpeechRecognitionServiceFactory.CreateMicrophoneClient(SpeechRecognitionMode.LongDictation, "en-US", "4b790b2bf55a48898759b747e0709371");

            micClient.OnMicrophoneStatus += (s, e) =>
            {
                Console.WriteLine("[{0}]", (e.Recording) ? "MIC ON" : "MIC OFF");
            };

            micClient.OnConversationError += (s, e) =>
            {
                Console.Error.WriteLine(e.SpeechErrorText);
            };

            micClient.OnPartialResponseReceived += (s, e) =>
            {
                Console.Write(e.PartialResult);
                Console.SetCursorPosition(0, Console.CursorTop);
            };

            micClient.OnResponseReceived += (s, e) =>
            {
                if (e.PhraseResponse.RecognitionStatus == RecognitionStatus.RecognitionSuccess)
                {
                    Console.WriteLine(e.PhraseResponse.Results[0].DisplayText);
                }
            };

            micClient.StartMicAndRecognition();
        }
    }
}
