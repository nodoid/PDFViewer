using System.Collections.ObjectModel;
using System.Reflection;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PDFViewer.Helpers;
using PDFViewer.Interfaces;

namespace PDFViewer.ViewModels;

public partial class PDFReaderViewModel : ObservableObject
{
    #nullable enable
    IListAssets? assets => Startup.ServiceProvider.GetService<IListAssets>();
    #nullable disable
    
    ObservableCollection<string> filenames { get; set; } = new ObservableCollection<string>();
    
    [ObservableProperty]
    ObservableCollection<string> routes;

    [ObservableProperty]
    string selectedRoute;

    [ObservableProperty]
    int selectedRouteValue;

    [ObservableProperty]
    double heightSize;

    [RelayCommand]
    void ChooseTimetable(int choice)
    {
        SelectedRoute = $"Timetables/{filenames[choice]}";
        SelectedRouteValue = choice;
    }

    public PDFReaderViewModel() => HeightSize = DeviceDisplay.Current.MainDisplayInfo.Height - 50;

    public void Setup()
    { 
        // we need to find the pdf files in the Raw directory. yes, I could
        // just enter them by hand, but that is restrictive and means that
        // if I add any more to that directory, they won't be picked up

        filenames = assets.ListAssets("Timetables").ToObservable();
        
        // test there is something there
        if (filenames.Count != 0)
        {
            // all good
            SelectedRoute = $"Timetables/{filenames[0]}";
            SelectedRouteValue = 0;
        }

        // this could be improved upon to take a plain text file, read the contents and add to the Routes
        // collection.
        Routes =
        [
            "Crosville H20 - 25", "Joint Operators 89",
            "MPTE 61", "MPTE 66/88"
        ];
    }
}