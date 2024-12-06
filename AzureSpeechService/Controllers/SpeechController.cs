using AzureSpeechService.Services;
using Microsoft.AspNetCore.Mvc;

namespace AzureSpeechService.Controllers;

public class SpeechController : Controller
{
    private readonly TextToSpeechService _textToSpeechService;

    public SpeechController(TextToSpeechService textToSpeechService)
    {
        _textToSpeechService = textToSpeechService;
    }

    [HttpPost]
    public async Task<IActionResult> Synthesize(string text)
    {
        try
        {
            var audioData = await _textToSpeechService.ConvertTextToSpeechAsync(text);
            return File(audioData, "audio/wav", "output.wav");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    public IActionResult Index()
    {
        return View();
    }
}
