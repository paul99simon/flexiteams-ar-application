using System.Collections;

namespace FlexiTeams.Util;

public class Map<T1, T2> : IEnumerable where T1 : notnull where T2 : notnull 
{
    
    public int Count => _forward.Count;

    private readonly Dictionary<T1, T2> _forward = new();
    private readonly Dictionary<T2, T1> _reverse = new();
    
    public Indexer<T1, T2> Forward { get; private set;}
    public Indexer<T2, T1> Reverse { get; private set;}
    
    public Map() {
        Forward = new Indexer<T1, T2>(_forward);
        Reverse = new Indexer<T2, T1>(_reverse);
    }
    
    public void Add(T1 key1, T2 key2)
    {
        if (key1 == null || String.IsNullOrWhiteSpace(key1.ToString())) throw new ArgumentNullException();
        if (key2 == null || String.IsNullOrWhiteSpace(key2.ToString())) throw new ArgumentNullException();
        if (_forward.ContainsKey(key1)) throw new ArgumentException("Map already contains key pair {" + key1 + ", "+ _forward[key1] +"}");
        if( _reverse.ContainsKey(key2)) throw new ArgumentException("Map already contains key pair {" + _reverse[key2] + ", "+ key2 +"}");
        
        _forward.Add(key1, key2);
        _reverse.Add(key2, key1);
    }
    
    public IEnumerator GetEnumerator()
    {
        return _forward.GetEnumerator();
    }
    
    public override string ToString()
    {
        return _forward.ToString();
    }

    public class Indexer<T3, T4> where T3 : notnull
    {
        private Dictionary<T3, T4> _dictionary;

        public Indexer(Dictionary<T3, T4> dictionary)
        {
            _dictionary = dictionary;
        }

        public T4 this[T3 index]
        {
            get { return _dictionary[index]; }
            set { _dictionary[index] = value; }
        }
    }

}