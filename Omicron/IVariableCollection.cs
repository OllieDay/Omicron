namespace Omicron
{
	/// <summary>
	/// Represents a collection of variables.
	/// </summary>
	public interface IVariableCollection
	{
		/// <summary>
		/// Gets or sets the variable with the specified name.
		/// </summary>
		/// <param name="name">The name of the variable to get or set.</param>
		/// <returns>The variable with the specified name, or <c>null</c> if it does not exist.</returns>
		object this[string name] { get; set; }

		/// <summary>
		/// Gets the variable with the specified name.
		/// </summary>
		/// <param name="name">The name of the variable to get.</param>
		/// <typeparam name="T"></typeparam>
		/// <returns>The variable with the specified name, or a default value if it does not exist.</returns>
		T Get<T>(string name);

		/// <summary>
		/// Gets the variable with the specified name.
		/// </summary>
		/// <param name="name">The name of the variable to get.</param>
		/// <returns>The variable with the specified name, or <c>null</c> if it does not exist.</returns>
		object Get(string name);

		/// <summary>
		/// Sets the variable with the specified name and value.
		/// </summary>
		/// <param name="name">The name of the variable to set.</param>
		/// <param name="value">The value of the variable to set.</param>
		void Set(string name, object value);
	}
}
