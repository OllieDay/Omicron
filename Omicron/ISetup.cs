using System;

namespace Omicron
{
	/// <summary>
	/// Represents an object used to add variables to a collection.
	///
	/// See <see cref="EnvironmentAttribute"/> for setting variables for different environments.
	/// </summary>
	public interface ISetup
	{
		/// <summary>
		/// Adds variables to the specified collection.
		/// </summary>
		/// <param name="variables">The collection of variables.</param>
		void AddVariables(IVariableCollection variables);
	}
}
