﻿using InSynq.Core.Model.Interfaces;
using System.Reflection;

namespace InSynq.Infrastructure.Data;

internal static class ModelBuilderExtensions
{
	internal static void ApplyEntityConfigurations(this ModelBuilder builder, Assembly assembly)
	{
		var entityTypes = assembly.GetTypes()
			.Where(_ => typeof(IConfigurableEntity).IsAssignableFrom(_) && !_.IsAbstract)
			.ToList();

		foreach (var type in entityTypes)
		{
			var instance = Activator.CreateInstance(type) as IConfigurableEntity;
			instance?.Configure(builder);
		}
	}
}