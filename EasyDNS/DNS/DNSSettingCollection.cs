using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace theDiary.EasyDNS.Windows
{
    [Serializable]
    public class DNSSettingCollection
        : IList<DNSSetting>,
        ISerializable,
        IEnumerable<DNSSetting>
    {
        public DNSSettingCollection()
            : base()
        {
            this.collection = new SortedList<string, DNSSetting>();
        }

        private DNSSettingCollection(SerializationInfo info, StreamingContext context)
        {
            this.collection = (SortedList<string, DNSSetting>)info.GetValue("items", typeof(SortedList<string, DNSSetting>));
        }
        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("items", this.collection);
        }

        private readonly object syncObject = new object();
        private volatile SortedList<string, DNSSetting> collection;

        public DNSSetting this[int index]
        {
            get
            {
                return this.collection.Values[index];
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                this.collection.Values[index] = value;
            }
        }


        public bool IsReadOnly => false;

        public int Count
        {
            get
            {
                if (this.collection == null)
                    return 0;
                return this.collection.Count;
            }
        }

        public DNSSetting this[string name]
        {
            get
            {
                if (this.collection.ContainsKey(name))
                    return this[name];

                return null;
            }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(value));

                if (!name.Equals(value.Name, StringComparison.OrdinalIgnoreCase))
                {
                    this.collection.Remove(name);
                    name = value.Name;
                }

                this.Add(value);

            }
        }
        public void Add(DNSSetting item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (this.collection.ContainsKey(item.Name))
            {
                this[item.Name] = item;
            }
            else
            {
                this.collection.Add(item.Name, item);
            }
        }

        public bool Contains(DNSSetting item)
        {
            if (item == null || string.IsNullOrWhiteSpace(item.Name))
                throw new ArgumentNullException(nameof(item));

            return this.collection.ContainsKey(item.Name);
        }

        public void CopyTo(DNSSetting[] array, int arrayIndex)
        {
            ((IList<DNSSetting>)this.collection.Values).CopyTo(array, arrayIndex);
        }

        public int IndexOf(DNSSetting item)
        {
            return this.collection.IndexOfValue(item);
        }

        public void Insert(int index, DNSSetting item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            ((IList<DNSSetting>)this.collection.Values).Insert(index, item);
        }

        public bool Remove(DNSSetting item)
        {
            if (item == null)
                return false;

            return this.collection.Remove(item.Name);
        }

        public IEnumerator<DNSSetting> GetEnumerator()
        {
            return this.collection.Values.GetEnumerator();
        }

        public void RemoveAt(int index)
        {
            this.collection.RemoveAt(index);
        }

        public void Clear()
        {
            this.collection.Clear();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.collection.Values.GetEnumerator();
        }
    }

    public class DNSSettingComparer
        : IComparer<string>
    {
        public DNSSettingComparer()
            : base()
        {

        }
        public int Compare(string x, string y)
        {
            return string.Compare(x, y, true);
        }
    }
    /*[Serializable]
    public class DNSSettingCollection
        : IEnumerable<DNSSetting>,
        IDictionary<string, DNSSetting>,
        IList<DNSSetting>,
        System.Runtime.Serialization.ISerializable
    {
        #region Constructors
        public DNSSettingCollection()
            : base()
        {
            this.collection = new SortedList<string, DNSSetting>();
        }

        public DNSSettingCollection(IEnumerable<DNSSetting> items)
            : this()
        {
            if (items == null)
                return;

            foreach (var item in items)
                this.collection.Add(item.Name, item);
        }

        private DNSSettingCollection(SerializationInfo info, StreamingContext context)
        {
            this.collection = (Dictionary<string, DNSSetting>) info.GetValue("items", typeof(Dictionary<string, DNSSetting>));
        }
        #endregion

        #region Private Declarations
        private SortedList<string, DNSSetting> collection;
        private readonly object syncObject = new object();
        #endregion

        #region Public Properties
        public int Count
        {
            get
            {
                return this.collection.Count;
            }
        }
        
        ICollection<string> IDictionary<string, DNSSetting>.Keys => throw new NotImplementedException();

        ICollection<DNSSetting> IDictionary<string, DNSSetting>.Values => throw new NotImplementedException();

        int ICollection<KeyValuePair<string, DNSSetting>>.Count => throw new NotImplementedException();

        bool ICollection<KeyValuePair<string, DNSSetting>>.IsReadOnly => throw new NotImplementedException();

        DNSSetting IDictionary<string, DNSSetting>.this[string key] { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public DNSSetting this[string key]
        {
            get
            {
                if (this.collection.ContainsKey(key))
                    return this.collection[key];

                return null;
            }
            set
            {
                if (!this.collection.ContainsKey(key))
                {
                    this.collection.Add(key, value);
                    
                }
                else
                {
                    if (!key.Equals(value.Name))
                    {
                        this.collection.Remove(key);
                        this[value.Name] = value;
                    }
                    else
                    {
                        this.collection[key].PrimaryDNS = value.PrimaryDNS;
                        this.collection[key].SecondaryDNS = value.SecondaryDNS;
                    }
                }
            }
        }

        #endregion

        public void Cleanup()
        {
            if (this.collection.ContainsKey(string.Empty))
                this.collection.Remove("");
        }

        public void Add(DNSSetting value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            this[value.Name] = value;
        }

        public void Remove(DNSSetting value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            this.Remove(value.Name);
        }

        public bool Remove(string key)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentNullException(nameof(key));

            return this.collection.Remove(key);
        }

        public IEnumerator<DNSSetting> GetEnumerator()
        {
            return this.collection.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        int IList.Add(object value)
        {
            this.Add((DNSSetting)value);
            return this.Count -1;
        }

        bool IList.Contains(object value)
        {
            return this.collection.ContainsKey(((DNSSetting)value).Name);
        }

        public void Clear()
        {
            this.collection.Clear();
        }

        int IList.IndexOf(object value)
        {
            throw new NotImplementedException();
        }

        void IList.Insert(int index, object value)
        {
            
        }

        void IList.Remove(object value)
        {
            this.Remove(((DNSSetting)value).Name);
        }

        void IList.RemoveAt(int index)
        {
            
        }

        void ICollection.CopyTo(Array array, int index)
        {
            
        }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("items", this.collection);
        }

        public bool ContainsKey(string key)
        {
            return this.collection.ContainsKey(key);
        }

        void IDictionary<string, DNSSetting>.Add(string key, DNSSetting value)
        {
            this.collection.Add(key, value);
        }

        bool IDictionary<string, DNSSetting>.TryGetValue(string key, out DNSSetting value)
        {
            return ((IDictionary<string, DNSSetting>)this.collection).TryGetValue(key, out value);
        }

        void ICollection<KeyValuePair<string, DNSSetting>>.Add(KeyValuePair<string, DNSSetting> item)
        {
            ((ICollection<KeyValuePair<string, DNSSetting>>)this.collection).Add(item);
        }

        bool ICollection<KeyValuePair<string, DNSSetting>>.Contains(KeyValuePair<string, DNSSetting> item)
        {
            return ((ICollection<KeyValuePair<string, DNSSetting>>)this.collection).Remove(item);
        }

        void ICollection<KeyValuePair<string, DNSSetting>>.CopyTo(KeyValuePair<string, DNSSetting>[] array, int arrayIndex)
        {
            ((ICollection<KeyValuePair<string, DNSSetting>>)this.collection).CopyTo(array, arrayIndex);
        }

        bool ICollection<KeyValuePair<string, DNSSetting>>.Remove(KeyValuePair<string, DNSSetting> item)
        {
            return ((ICollection<KeyValuePair<string, DNSSetting>>)this.collection).Remove(item);
        }

        IEnumerator<KeyValuePair<string, DNSSetting>> IEnumerable<KeyValuePair<string, DNSSetting>>.GetEnumerator()
        {
            return ((IEnumerator<KeyValuePair<string, DNSSetting>>)this.collection);
        }*/

}
