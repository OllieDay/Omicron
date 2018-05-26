using System.Collections.Generic;

namespace Omicron
{
	internal sealed class VariableCollection : IVariableCollection
	{
		private readonly IDictionary<string, object> _values = new Dictionary<string, object>();

		public object this[string name]
		{
			get => Get(name);
			set => Set(name, value);
		}

		public T Get<T>(string name)
		{
			if (_values.ContainsKey(name))
			{
				return (T)_values[name];
			}

			return default;
		}

		public object Get(string name)
		{
			return Get<object>(name);
		}

		public void Set(string name, object value)
		{
			if (_values.ContainsKey(name))
			{
				_values[name] = value;
			}
			else
			{
				_values.Add(name, value);
			}
		}
	}
}
