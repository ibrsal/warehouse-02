

using MonkeyFinder.Services;
using MonkeyFinder.Model;

namespace MonkeyFinder.ViewModel;

//Add QueryProperty
[QueryProperty(nameof(Monkey), "Monkey")]
public partial class MonkeyDetailsViewModel : BaseViewModel
{
    IMap map;
    public MonkeyDetailsViewModel(IMap map)
    {
        this.map = map;

    }

    [ObservableProperty]
    Monkey monkey;

    [RelayCommand]
    async Task OpenMap()
    {
        try
        {
            await map.OpenAsync(Monkey.Latitude, Monkey.Longitude, new MapLaunchOptions
            {
                Name = Monkey.Name,
                NavigationMode = NavigationMode.None
            });
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to launch maps: {ex.Message}");
            await Shell.Current.DisplayAlert("Error, no Maps app!", ex.Message, "OK");
        }
    }

    [RelayCommand]
    async Task SaveMonkey()
    {
        var anInstanceofCoffeeService = new CoffeeService();

        var name = Monkey.Name;
        var rosatr = Monkey.Details;
        

        try
        {
            await anInstanceofCoffeeService.AddCoffee(name, rosatr);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to Add item: {ex.Message}");
            await Shell.Current.DisplayAlert("Error, Error from Sqlite", ex.Message, "OK");
        }
    }
}
