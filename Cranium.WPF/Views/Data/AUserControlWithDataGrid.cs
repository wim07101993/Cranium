using System.Collections.Generic;
using System.Linq;
using Cranium.WPF.Services.Strings;

namespace Cranium.WPF.Views.Data
{
    public abstract class AUserControlWithDataGrid : UserControlWithStrings
    {
        protected abstract IEnumerable<ColumnHeader> ColumnNames { get; }

        protected override void OnStringsChanged(Strings strings, string propertyName)
        {
            base.OnStringsChanged(strings, propertyName);

            if (ColumnNames == null)
                return;

            var headersToUpdate = propertyName == null
                ? ColumnNames
                : ColumnNames.Where(x => x?.PropertyName == propertyName);

            foreach (var columnHeader in headersToUpdate)
                columnHeader.Column.Header = columnHeader.HeaderValue(strings);
        }
    }
}