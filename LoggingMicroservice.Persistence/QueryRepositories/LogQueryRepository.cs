﻿using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LoggingMicroservice.Persistence.QueryRepositories
{
	public class LogQueryRepository :
		Dtx.Persistence.QueryRepository<Domain.Models.Log>,
		Domain.IQueryRepositories.ILogQueryRepository
	{
		public LogQueryRepository(QueryDatabaseContext databaseContext) : base(databaseContext)
		{
		}

		public
			async
			System.Threading.Tasks.Task
			<System.Collections.Generic.IList<Domain.ViewModels.GetLogsQueryResponseViewModel>>
			GetSomeAsync(int count)
		{
			// Note: ToListAsync -> Extension Method -> using Microsoft.EntityFrameworkCore;
			var result =
				await
				DbSet
				.OrderByDescending(current => current.TimeStamp)
				.Skip(count: 0)
				.Take(count: count)
				.Select(current => new Domain.ViewModels.GetLogsQueryResponseViewModel()
				{
					Id = current.Id,
					Level = current.Level,
					TimeStamp = current.TimeStamp,

					Message = current.Message,

					ApplicationName = current.ApplicationName,
					Namespace = current.Namespace,
					ClassName = current.ClassName,
					MethodName = current.MethodName,

					RemoteIP = current.RemoteIP,
					Username = current.Username,
					RequestPath = current.RequestPath,
				})
				.ToListAsync()
				;

			return result;
		}
	}
}
