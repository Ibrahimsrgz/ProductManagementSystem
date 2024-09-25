namespace ProductManagementSystem.Currencies
{
    public static class CurrencyConsts
    {
        private const string DefaultSorting = "{0}Code asc";

        public static string GetDefaultSorting(bool withEntityName)
        {
            return string.Format(DefaultSorting, withEntityName ? "Currency." : string.Empty);
        }

        public const int CodeMaxLength = 3;
        public const int NameMaxLength = 50;
        public const int SymbolMaxLength = 10;
        public const int CountryMaxLength = 50;
    }
}