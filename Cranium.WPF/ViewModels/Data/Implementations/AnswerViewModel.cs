using Cranium.WPF.Models;
using Cranium.WPF.Services.Strings;
using Cranium.WPF.ViewModels.Implementations;

namespace Cranium.WPF.ViewModels.Data.Implementations
{
    public class AnswerViewModel : AViewModelBase, IAnswerViewModel
    {
        private Answer _model;

        public AnswerViewModel(IStringsProvider stringsProvider)
            : base(stringsProvider)
        {
        }

        public Answer Model
        {
            get => _model;
            set => SetProperty(ref _model, value);
        }
    }
}