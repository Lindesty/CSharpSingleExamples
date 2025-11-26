using CommunityToolkit.Mvvm.ComponentModel;

namespace _4_EFCoreWithSqliteInWPF.ViewModels;

public class ViewModelBase : ObservableObject
{



    public virtual string ViewModelName => this.GetType().FullName ?? nameof(ViewModelBase);
    public virtual string DisplayViewModelName => "ViewModelBase(this class not override this func)";
}