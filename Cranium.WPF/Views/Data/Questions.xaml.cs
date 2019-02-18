using System.Collections.Generic;
using Cranium.WPF.Services.Strings;

namespace Cranium.WPF.Views.Data
{
    public partial class Questions
    {
        public Questions()
        {
            InitializeComponent();
            ColumnNames = new[]
            {
                new ColumnHeader
                {
                    Column = TaskColuumn,
                    PropertyName = nameof(Strings.Task),
                    HeaderValue = x => x.Task
                },
            };
        }

        protected override IEnumerable<ColumnHeader> ColumnNames { get; }
    }
}
