using MonkeyFinder.View;

namespace MonkeyFinder;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));

        Routing.RegisterRoute(nameof(StoredLocal), typeof(StoredLocal));

    }
}

/* namespace warehouse_02;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
	}
} */
