using System.Collections.Generic;
using Cranium.WPF.Services.Strings;

namespace Cranium.WPF.Views.Data
{
    public partial class Questions
    {
        public Questions()
        {
            InitializeComponent();
        }

        protected override IEnumerable<ColumnHeader> ColumnNames { get; }
    }
}
