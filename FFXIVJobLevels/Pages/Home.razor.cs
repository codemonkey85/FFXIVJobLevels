namespace FFXIVJobLevels.Pages;

public partial class Home
{
    [Inject]
    private HttpClient? HttpClient { get; set; }

    private LodeStoneCharacter LodeStoneCharacter { get; set; } = new LodeStoneCharacter();

    private bool ButtonDisabled => LodeStoneCharacter.LodestoneId is null;

    private async Task OnButtonClick()
    {
        if (HttpClient is null || LodeStoneCharacter.LodestoneId is null)
        {
            return;
        }

        var response = await HttpClient.GetAsync($"character/{LodeStoneCharacter.LodestoneId}");

        if (response.IsSuccessStatusCode is false)
        {
            var responseMessage = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseMessage);
            return;
        }

        var item = JsonSerializer.Deserialize<dynamic>(
            response.Content.ReadAsStringAsync().Result);

        if (item is null)
        {
            return;
        }
    }
}

public class LodeStoneCharacter
{
    public long? LodestoneId { get; set; }
}