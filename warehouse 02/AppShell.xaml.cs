namespace MonkeyFinder;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));

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
