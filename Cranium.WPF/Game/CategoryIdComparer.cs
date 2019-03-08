using System.Collections.Generic;
using Cranium.WPF.Data.Category;

namespace Cranium.WPF.Game
{
    public class CategoryIdComparer : IEqualityComparer<Category>
    {
        public bool Equals(Category x, Category y) => x != null && x.Id == y?.Id;

        public int GetHashCode(Category category) => category.Id.GetHashCode();
    }
}