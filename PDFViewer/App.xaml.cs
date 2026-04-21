using PDFViewer.ViewModels;

namespace PDFViewer;

public partial class App : Application
{
    public static IServiceProvider? Service { get; set; }

    public App()
    {
        Service = Startup.Init();
        
        InitializeComponent();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        return new Window(new AppShell());
    }

}