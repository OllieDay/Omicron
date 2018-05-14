using System.Collections.Generic;

namespace Omicron
{
	internal sealed class VariableCollection : IVariableCollection
	{
		private readonly IDictionary<string, object> _values = new Dictionary<string, object>();

		public object this[string name]
		{
			get => _values[name];
			set => _values[name] = value;
		}
	}
}
