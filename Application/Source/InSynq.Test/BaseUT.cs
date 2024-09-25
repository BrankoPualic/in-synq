using Autofac;
using AutoMapper;
using InSynq.Core.Model.Interfaces;
using InSynq.Infrastructure.Data;
using InSynq.Infrastructure.DependencyRegister.Modules;

namespace InSynq.Test;

public abstract class BaseUT
{
	private ILifetimeScope _scope = null;

	private ILifetimeScope Scope
	{
		get
		{
			if (_scope == null)
			{
				var builder = new ContainerBuilder();
				builder.RegisterModule<UnitTestModule>();

				var container = builder.Build();
				_scope = container.BeginLifetimeScope();
			}

			return _scope;
		}
	}

	protected TService Get<TService>() where TService : notnull => Scope.Resolve<TService>();

	protected IIdentityUser CurrentUser => Get<IIdentityUser>();

	protected IMapper Mapper => Get<IMapper>();

	private DatabaseContext _db { get; set; }

	protected DatabaseContext db
	{
		get
		{
			_db ??= new(CurrentUser);
			return _db;
		}
	}
}