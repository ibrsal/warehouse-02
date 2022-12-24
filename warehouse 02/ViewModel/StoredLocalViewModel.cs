using Microsoft.Maui.Networking;
using MonkeyFinder.Model;
using MonkeyFinder.Services;

namespace MonkeyFinder.ViewModel;

public partial class StoredLocalViewModel : BaseViewModel
{
    MonkeyService monkeyService;
    public ObservableCollection<Monkey> Monkeys { get; } = new();

    // public Command GetMonkeysCommand { get; set; }
    IConnectivity connectivity;
    public StoredLocalViewModel(MonkeyService monkeyService, IConnectivity connectivity, IGeolocation geolocation)
    {

        Title = "Monkey Finder";
        this.monkeyService = monkeyService;
        this.connectivity = connectivity;
    }

    async Task GetMonkeysAsync()
    {
        if (connectivity.NetworkAccess != NetworkAccess.Internet)
        {
            await Shell.Current.DisplayAlert("No Internet",
                $"Please check internet and try again", "OK");
            return;
        }
        if (IsBusy)
            return;
        try
        {
            IsBusy = true;
            var monkeys = await monkeyService.GetMonkeys();
            if (Monkeys.Count != 0)
                Monkeys.Clear();
            foreach (var monkey in monkeys)
                Monkeys.Add(monkey);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error!", $"Unable to get monkeys:{ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

}