using System;
using System.Collections.Generic;

namespace ConventionsAide.Core.Common.Architecture.Registration
{
    internal abstract class TypesRegistratorsManagerBase
	{
		public IEnumerable<StartupRegistratorBase> GetAllRegistrators()
		{
			return DeduplicateRegistrators(GetTypeRegistrators());
		}

		protected IEnumerable<StartupRegistratorBase> DeduplicateRegistrators(IEnumerable<StartupRegistratorBase> typeRegistrators)
		{
			var deduplicated = new List<StartupRegistratorBase>();
			
			if (typeRegistrators == null)
			{
				return null;
			}

			var added = new HashSet<Type>();

			foreach (var registrator in typeRegistrators)
			{
				if (added.Contains(registrator.GetType()))
				{
					continue;
				}

				added.Add(registrator.GetType());

				deduplicated.Add(registrator);
			}

			return deduplicated;
		}

		protected abstract IEnumerable<StartupRegistratorBase> GetTypeRegistrators();
	}
}
