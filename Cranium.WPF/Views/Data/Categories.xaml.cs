using System.Collections.Generic;
using Cranium.WPF.Services.Strings;

namespace Cranium.WPF.Views.Data
{
    public partial class Categories 
    {
        public Categories()
        {
            InitializeComponent();
            ColumnNames = new[]
            {
                new ColumnHeader
                {
                    Column = NameColumn,
                    PropertyName = nameof(Strings.Name),
                    HeaderValue = x => x.Name
                },
                new ColumnHeader
                {
                    Column = ExplanationColumn,
                    PropertyName = nameof(Strings.Explanation),
                    HeaderValue = x => x.Explanation
                },
            };
        }

        protected override IEnumerable<ColumnHeader> ColumnNames { get; }
    }
}
