using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Map<Key, Value>
{
    public Dictionary<Key, Value> dict { get; private set; } = new Dictionary<Key, Value>();

    public bool Has(Key key) { return dict.ContainsKey(key); }
    public bool TryGet(Key key, out Value value)
    {
        bool ret = false;
        value = default(Value);
        if (Has(key))
        {
            ret = true;
            value = dict[key];
        }
        return ret;
    }
    public void Add(Key key, Value value) { if (!Has(key)) { dict.Add(key, value); } }
    public void Remove(Key key) { if (Has(key)) { dict.Remove(key); } }
    public void Clear() { dict.Clear(); }
}