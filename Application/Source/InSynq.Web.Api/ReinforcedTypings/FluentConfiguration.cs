﻿using InSynq.Common;
using InSynq.Core.Model;
using InSynq.Web.Api.Controllers._Base;
using InSynq.Web.Api.ReinforcedTypings.Generator;
using Reinforced.Typings.Ast.TypeNames;
using Reinforced.Typings.Fluent;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace InSynq.Web.Api.ReinforcedTypings;

public static class FluentConfiguration
{
	private static readonly Action<InterfaceExportBuilder> _interfaceConfiguration = config =>
		config.WithPublicProperties()
		.ConfigureTypeMapping()
		.ExportTo("interfaces.ts");

	private static readonly Action<ClassExportBuilder> _serviceConfiguration = config =>
		config
		.AddImport("{ Injectable }", "@angular/core")
		.AddImport("{ HttpParams, HttpClient }", "@angular/common/http")
		.AddImport("{ SettingsService }", "../services/settings.service")
		.AddImport("{ Observable }", "rxjs")
		.AddImport("{ map }", "rxjs/operators")
		.ExportTo("services.ts")
		.WithCodeGenerator<AngularControllerGenerator>();

	public static void Configure(Reinforced.Typings.Fluent.ConfigurationBuilder builder)
	{
		builder.Global(config => config.CamelCaseForProperties()
								.DontWriteWarningComment()
							   .AutoOptionalProperties()
							   .UseModules());

		// Enums

		Type[] enums = [
			typeof(eSystemRole),
		];

		builder.ExportAsEnums(enums,
			config =>
				config.DontIncludeToNamespace()
				.ExportTo("enums.ts")
			);

		// Dtos

		var coreAssembly = Assembly.Load($"{Constants.SOLUTION_NAME}.Core");
		var dtos = coreAssembly
			.GetTypes()
			.Where(t => t.IsClass
				&& t.Namespace != null
				&& t.Namespace.Contains($"{Constants.SOLUTION_NAME}.Core.Dtos")
				&& !t.IsDefined(typeof(CompilerGeneratedAttribute), false));

		builder.ExportAsInterfaces(
			dtos
			.OrderBy(i => i.Name)
			.ToArray(),
			_interfaceConfiguration
			);

		// Controllers

		builder.ExportAsClasses(
			Assembly.GetAssembly(typeof(BaseController)).ExportedTypes
			.Where(i => i.Namespace.StartsWith($"{Constants.SOLUTION_NAME}.Web.Api.Controllers"))
			.OrderBy(i => i.Name)
			.OrderBy(i => i.Name != nameof(BaseController))
			.ToArray(),
			_serviceConfiguration
			);
	}

	private static TBuilder ConfigureTypeMapping<TBuilder>(this TBuilder config)
		where TBuilder : ClassOrInterfaceExportBuilder
	{
		config
			.Substitute(typeof(Guid), new RtSimpleTypeName("string"))
			.Substitute(typeof(DateTime), new RtSimpleTypeName("Date"))
			.Substitute(typeof(DateOnly), new RtSimpleTypeName("Date"))
			.Substitute(typeof(IFormFile), new RtSimpleTypeName("File"));

		return config;
	}
}