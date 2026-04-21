using CommunityToolkit.Maui.Alerts;
using MauiNativePdfView.Abstractions;
using System.ComponentModel;

namespace PDFViewer.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ViewModel.PropertyChanged += ViewModelOnPropertyChanged;
        ViewModel.Setup();
    }

    void ViewModelOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == "SelectedRoute")
        {
            MainThread.InvokeOnMainThreadAsync(() => pdfview.Source = PdfSource.FromAsset(ViewModel.SelectedRoute));
        }
    }

    void Picker_OnSelectedIndexChanged(object? sender, EventArgs e)
    {
        ViewModel.ChooseTimetableCommand.Execute(picker.SelectedIndex);
    }

    void pdfview_Error(object sender, MauiNativePdfView.Abstractions.PdfErrorEventArgs e)
    {
        if (IsLoaded)
            DisplayAlertAsync("Error", $"Failed to load PDF: {e.Message}, filename = {ViewModel.SelectedRoute}", "OK");
    }

    void pdfview_DocumentLoaded(object sender, MauiNativePdfView.Abstractions.DocumentLoadedEventArgs e)
    {
        Toast.Make($"PDF loaded with {e.PageCount} pages").Show();
    }
}