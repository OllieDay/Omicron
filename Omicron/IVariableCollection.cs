namespace Omicron
{
	/// <summary>
	/// Represents a collection of variables.
	/// </summary>
	public interface IVariableCollection
	{
		/// <summary>
		/// Gets or sets the variable associated with the specified name.
		/// </summary>
		/// <param name="name">The name of the variable to get or set.</param>
		/// <returns>The variable associated with the specified key.</returns>
		object this[string name] { get; set; }
	}
}
