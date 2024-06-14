using System.Text;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Orders.Application.Commands;
using Orders.Application.Commands.Handlers;
using Orders.Application.Subscribers;

namespace Orders.Application;

public static class ApplicationInjection
{
	public static IServiceCollection AddHandlers(this IServiceCollection services)
	{
		services.AddMediatR(typeof(AddOrderCommand).Assembly);
		services.AddMediatR(typeof(AddOrderCommandHandler).Assembly);


		return services;
	}

	public static IServiceCollection AddSubscribers(this IServiceCollection services)
	{
		services.AddHostedService<PaymentAcceptedSubscriber>();

		return services;
	}

	public static string ToDashCase(this string text)
	{
		if (text == null)
		{
			throw new ArgumentNullException(nameof(text));
		}

		if (text.Length < 2)
		{
			return text;
		}

		var sb = new StringBuilder();
		sb.Append(char.ToLowerInvariant(text[0]));
		for (var i = 1; i < text.Length; ++i)
		{
			var c = text[i];
			if (char.IsUpper(c))
			{
				sb.Append('-');
				sb.Append(char.ToLowerInvariant(c));
			}
			else
			{
				sb.Append(c);
			}
		}

		return sb.ToString();
	}
}