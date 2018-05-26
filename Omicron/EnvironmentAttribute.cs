using System;

namespace Omicron
{
	/// <summary>
	/// Defines the environment that variables should be set for.
	///
	/// When added to a type that implements <see cref="ISetup"/> the type will be used to set variables if
	/// <see cref="Name"/> matches the value of the OMICRON_ENVIRONMENT environment variable. <see cref="ISetup"/> types
	/// with the <see cref="EnvironmentAttribute"/> will be called after those without, to allow environment-specific
	/// variables to overwrite global variables.
	///
	/// <see cref="ISetup"/> can have multiple <see cref="EnvironmentAttribute"/> if the variables should be set for
	/// multiple environments.
	/// </summary>
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
	public sealed class EnvironmentAttribute : Attribute
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EnvironmentAttribute"/> class with the specified ame.
		/// </summary>
		/// <param name="name">The name of the environment.</param>
		public EnvironmentAttribute(string name)
		{
			Name = name;
		}

		/// <summary>
		/// Gets the name of this environment.
		/// </summary>
		/// <returns>The name of this environment.</returns>
		public string Name { get; }
	}
}
