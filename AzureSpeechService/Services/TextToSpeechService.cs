using Microsoft.CognitiveServices.Speech;

namespace AzureSpeechService.Services;

public class TextToSpeechService
{
    private readonly string _subscriptionKey;
    private readonly string _region;

    public TextToSpeechService(IConfiguration configuration)
    {
        _subscriptionKey = configuration["AzureSpeech:SubscriptionKey"];
        _region = configuration["AzureSpeech:Region"];
    }

    public async Task<byte[]> ConvertTextToSpeechAsync(string text)
    {
        var config = SpeechConfig.FromSubscription(_subscriptionKey, _region);
        config.SpeechSynthesisVoiceName = "en-US-JennyNeural"; 

        using var synthesizer = new SpeechSynthesizer(config, null);
        using var result = await synthesizer.SpeakTextAsync(text);

        if (result.Reason == ResultReason.SynthesizingAudioCompleted)
        {
            return result.AudioData;
        }

        throw new Exception($"Speech synthesis failed: {result.Reason}");
    }
}
