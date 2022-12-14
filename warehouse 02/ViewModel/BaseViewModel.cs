namespace MonkeyFinder.ViewModel;
// [INotifyPropertyChanged]
public partial class BaseViewModel :ObservableObject
{
    public BaseViewModel()
    {
       
    }
    [ObservableProperty]
    //[AlsoNotifyChangeFor(nameof(IsNotBusy))]
    bool isBusy;
    [ObservableProperty]
    string title;

   /* public bool IsBusy
    {
        // get { return isBusy; }
        get => isBusy;
        set
        {
            if (isBusy == value) 
            { return; } 
            isBusy = value;
            // OnPropertyChanged(nameof(IsBusy));
            OnPropertyChanged();
            OnPropertyChanged(nameof(IsNotBusy));

        }

    }
    public string Title
    {
        get => title;
        set
        {
            if (title == value) 
                return;
            title = value;
            OnPropertyChanged();
        }
    } */

    public bool IsNotBusy => !IsBusy;
   /* public event PropertyChangedEventHandler PropertyChanged;
    public void OnPropertyChanged([CallerMemberName]string name = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    } */
}
