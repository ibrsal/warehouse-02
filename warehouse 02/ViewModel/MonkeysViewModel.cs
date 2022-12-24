using MonkeyFinder.Services;
using MonkeyFinder.View;

namespace MonkeyFinder.ViewModel;

public partial class MonkeysViewModel : BaseViewModel
{
    MonkeyService monkeyService;                
    public ObservableCollection<Monkey> Monkeys { get; } = new ();

    // public Command GetMonkeysCommand { get; set; }
    IConnectivity connectivity;
    IGeolocation geolocation;           
    public MonkeysViewModel(MonkeyService monkeyService,IConnectivity connectivity,IGeolocation geolocation)
    {
        Title = "Monkey Finder";
        this.monkeyService = monkeyService;
        this.connectivity = connectivity;
        this.geolocation = geolocation;
       /* GetMonkeysCommand = new Command(async () => await GetMonkeysAsync());
        GetMonkeysCommand.Execute;
       */
    }
    [RelayCommand]
    async Task GetClosestMonkey()
    {
        if (IsBusy || Monkeys.Count == 0)
            return;
        try
        {
            // get cached location or get real location
            var location = await geolocation.GetLastKnownLocationAsync();
            if (location == null)
            {
                location = await geolocation.GetLocationAsync(new GeolocationRequest
                {
                    DesiredAccuracy= GeolocationAccuracy.Medium,
                    Timeout= TimeSpan.FromSeconds(30)
                });
            }
            // find closest monkey to us
            var first = Monkeys.OrderBy(m =>location.CalculateDistance(
                new Location(m.Latitude,m.Longitude),DistanceUnits.Miles))
                .FirstOrDefault();
            await Shell.Current.DisplayAlert("", first.Name + " " + first.Location, "OK");

        }
        catch(Exception ex)
        {
            Debug.WriteLine($"Unable to query location: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
    }
    [RelayCommand]
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
                Monkeys.Clear ();
            foreach(var monkey in monkeys)
                Monkeys.Add (monkey);
        }
        catch(Exception ex) 
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error!", $"Unable to get monkeys:{ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    async Task GoToDetails(Monkey monkey)
    {
        if (monkey == null)
            return;

        await Shell.Current.GoToAsync(nameof(DetailsPage), true, new Dictionary<string, object>
        {
            {"Monkey", monkey }
        });
    }


    [RelayCommand]
    async Task GoToStoredLocal()
    {
        await Shell.Current.GoToAsync(nameof(StoredLocal));
    }

}
