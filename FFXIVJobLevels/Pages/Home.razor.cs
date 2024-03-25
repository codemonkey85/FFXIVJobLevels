namespace FFXIVJobLevels.Pages;

public partial class Home
{
    [Inject]
    private HttpClient? HttpClient { get; set; }

    private long? lodestoneId;

    private bool ButtonDisabled => lodestoneId is null;

    private async Task OnButtonClick()
    {
        if (HttpClient is null || lodestoneId is null)
        {
            return;
        }

        var response = await HttpClient.GetAsync($"character/{lodestoneId}");

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
