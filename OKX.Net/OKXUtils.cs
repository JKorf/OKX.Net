using OKX.Net.Objects.Public;
using System.Collections.Concurrent;

namespace OKX.Net
{
    internal static class OKXUtils
    {
        private static ConcurrentDictionary<string, ConcurrentDictionary<string, long>> _symbolDictionary = new ConcurrentDictionary<string, ConcurrentDictionary<string, long>>();

        internal static long? GetSymbolCode(string environmentName, string name)
        {
            if (!_symbolDictionary.TryGetValue(environmentName, out var symbols))
                return null;

            var symbol = symbols.FirstOrDefault(s => s.Key.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            if (symbol.Value == 0)
                return null;

            return symbol.Value;
        }

        internal static void UpdateSymbolCodes(string environmentName, OKXInstrument[] data)
        {
            var environmentDict = _symbolDictionary.GetOrAdd(environmentName, x => new ConcurrentDictionary<string, long>());
            foreach(var item in data)
                environmentDict.TryAdd(item.Symbol, item.SymbolCode ?? 0);
        }
    }
}
