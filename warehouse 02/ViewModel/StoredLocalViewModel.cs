using Microsoft.Maui.Networking;
using MonkeyFinder.Model;
using MonkeyFinder.Services;
using MonkeyFinder.Shared.Models;

namespace MonkeyFinder.ViewModel;

public partial class StoredLocalViewModel : BaseViewModel
{
    CoffeeService coffeeService;
    public ObservableCollection<Coffee> Coffees { get; } = new();

    // public Command GetMonkeysCommand { get; set; }
    IConnectivity connectivity;
    public StoredLocalViewModel(CoffeeService coffeeService, IConnectivity connectivity)
    {

        Title = "Local Coffee";
        this.coffeeService = coffeeService;
        this.connectivity = connectivity;
    }
    [RelayCommand]
    async Task GetCoffeesAsync()
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
            var coffees = await coffeeService.GetCoffee();
            if (Coffees.Count != 0)
                Coffees.Clear();
            foreach (var coffee in coffees)
                Coffees.Add(coffee);
        }
        catch (Exception ex)
        {
            Debug.WriteLine(ex);
            await Shell.Current.DisplayAlert("Error!", $"Unable to get coffees:{ex.Message}", "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

}