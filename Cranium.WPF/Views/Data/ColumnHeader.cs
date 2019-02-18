using System;
using System.Windows.Controls;
using Cranium.WPF.Services.Strings;

namespace Cranium.WPF.Views.Data
{
    public class ColumnHeader
    {
        public DataGridColumn Column { get; set; }
        public Func<Strings, string> HeaderValue { get; set; }
        public string PropertyName { get; set; }
    }
}
