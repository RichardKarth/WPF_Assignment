
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MainApp.Shared.Models;
using MainApp.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MainApp.ViewModels;

public partial class EditViewModel : ObservableObject
{
    private readonly IServiceProvider _serviceProvider;
    private readonly ICustomerService _customerService;
    public EditViewModel(IServiceProvider serviceProvider, ICustomerService customerService)
    {
        _serviceProvider = serviceProvider;
        _customerService = customerService;
    }
    [ObservableProperty]
    private Customer customer = new();

    [RelayCommand]
    public void Save()
    {
        var result = _customerService.Update(Customer);
        if (result == Shared.Enums.StatusCodes.Success)
        {
            //Navigate back to Overview
            var viewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
            viewModel.CurrentViewModel = _serviceProvider.GetRequiredService<OverviewViewModel>();
        }
    }
    [RelayCommand]
    public void Close()
    {
        var viewModel = _serviceProvider.GetRequiredService<MainWindowViewModel>();
        viewModel.CurrentViewModel = _serviceProvider.GetRequiredService<OverviewViewModel>();
    }

}
