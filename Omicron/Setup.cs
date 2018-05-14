using System;

namespace Omicron
{
	/// <summary>
	/// Represents an object used to add variables to a collection. Derived types should override
	/// <see cref="AddVariables"/> to add variables to the collection.
	/// </summary>
	public abstract class Setup
	{
		/// <summary>
		/// Adds variables to the specified collection.
		/// </summary>
		/// <param name="variables">The collection of variables.</param>
		public virtual void AddVariables(IVariableCollection variables)
		{

		}
	}
}
