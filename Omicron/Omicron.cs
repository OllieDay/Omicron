using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using MockableHttp;

namespace Omicron
{
	/// <summary>
	/// Used to create <see cref="IRequest"/> objects that represent an HTTP request.
	/// </summary>
	public static class O
	{
		private static readonly IHttpService HttpService = new HttpService();
		private static IVariableCollection _variables;

		/// <summary>
		/// Gets the collection of variables.
		/// </summary>
		/// <returns>The collection of variables.</returns>
		public static IVariableCollection Variables
		{
			get
			{
				// This is the earliest place that can access the calling assembly to instantiate an instance of the
				// derived Setup type containing variable set up logic.
				if (_variables == null)
				{
					_variables = new VariableCollection();

					AddVariables(_variables, Assembly.GetCallingAssembly());
				}

				return _variables;
			}
		}

		/// <summary>
		/// Sends an HTTP request to the specified URI.
		/// </summary>
		/// <param name="method">The HTTP method.</param>
		/// <param name="uri">The URI the request is sent to.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Request(HttpMethod method, string uri)
			=> new Request(HttpService, method, uri);

		/// <summary>
		/// Sends an HTTP DELETE request to the specified URI.
		/// </summary>
		/// <param name="uri">The URI the request is sent to.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Delete(string uri)
			=> Request(HttpMethod.Delete, uri);

		/// <summary>
		/// Sends an HTTP GET request to the specified URI.
		/// </summary>
		/// <param name="uri">The URI the request is sent to.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Get(string uri)
			=> Request(HttpMethod.Get, uri);

		/// <summary>
		/// Sends an HTTP HEAD request to the specified URI.
		/// </summary>
		/// <param name="uri">The URI the request is sent to.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Head(string uri)
			=> Request(HttpMethod.Head, uri);

		/// <summary>
		/// Sends an HTTP OPTIONS request to the specified URI.
		/// </summary>
		/// <param name="uri">The URI the request is sent to.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Options(string uri)
			=> Request(HttpMethod.Options, uri);

		/// <summary>
		/// Sends an HTTP POST request to the specified URI.
		/// </summary>
		/// <param name="uri">The URI the request is sent to.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Post(string uri)
			=> Request(HttpMethod.Post, uri);

		/// <summary>
		/// Sends an HTTP PUT request to the specified URI.
		/// </summary>
		/// <param name="uri">The URI the request is sent to.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Put(string uri)
			=> Request(HttpMethod.Put, uri);

		/// <summary>
		/// Sends an HTTP TRACE request to the specified URI.
		/// </summary>
		/// <param name="uri">The URI the request is sent to.</param>
		/// <returns>An <see cref="IRequest"/> object that represents the request.</returns>
		public static IRequest Trace(string uri)
			=> Request(HttpMethod.Trace, uri);

		private static void AddVariables(IVariableCollection variables, Assembly assembly)
		{
			var setups = GetSetups(assembly);

			foreach (var setup in setups)
			{
				setup.AddVariables(variables);
			}
		}

		private static IEnumerable<ISetup> GetSetups(Assembly assembly)
		{
			// Create an instance of each type in the target assembly that implements ISetup, is not an interface, is
			// not abstract, has a parameterless constructor, and either doesn't have EnvironmentAttribute or has an
			// EnvironmentAttribute that matches OMICRON_ENVIRONMENT (if set). Order so that types that don't have the
			// EnvironmentAttribute come first as these are used to set global variables - environment variables should
			// be set afterwards to overwrite them.

			return assembly
				.GetTypes()
				.Where(IsSetup)
				.Where(IsInstantiable)
				.Where(IsGlobalOrCurrentEnvironmentSetup)
				.OrderBy(GlobalThenEnvironmentSetup)
				.Select(Activator.CreateInstance)
				.Cast<ISetup>();
		}

		private static bool IsSetup(Type type)
			=> typeof(ISetup).IsAssignableFrom(type);

		private static bool IsInstantiable(Type type)
			=> !type.IsInterface && !type.IsAbstract && HasParameterlessConstructor(type);

		private static bool HasParameterlessConstructor(Type type)
			=> type.GetConstructor(Type.EmptyTypes) != null;

		private static bool IsGlobalOrCurrentEnvironmentSetup(Type type)
			=> IsGlobalSetup(type) || IsCurrentEnvironmentSetup(type);

		private static bool IsGlobalSetup(Type type)
			=> !type.IsDefined(typeof(EnvironmentAttribute));

		private static bool IsCurrentEnvironmentSetup(Type type)
		{
			var environment = GetCurrentEnvironment();

			return type
				.GetCustomAttributes(typeof(EnvironmentAttribute), inherit: false)
				.Cast<EnvironmentAttribute>()
				.Any(x => x.Name == environment);
		}

		private static bool GlobalThenEnvironmentSetup(Type type)
			=> !IsGlobalSetup(type);

		private static string GetCurrentEnvironment()
			=> Environment.GetEnvironmentVariable("OMICRON_ENVIRONMENT");
	}
}
