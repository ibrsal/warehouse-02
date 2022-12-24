namespace MonkeyFinder;

public partial class StoredLocal : ContentPage
{
	public StoredLocal(StoredLocalViewModel viewModel)
	{
		InitializeComponent();

        BindingContext = viewModel;
    }
}

