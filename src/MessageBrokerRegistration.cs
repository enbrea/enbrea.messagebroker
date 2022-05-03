#region ENBREA - Copyright (c) 2022 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA 
 *    
 *    Copyright (c) 2022 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using Microsoft.Extensions.DependencyInjection;
using System;

namespace Enbrea.MessageBroker
{
    /// <summary>
    /// Extension methods for setting up a Message Broker service in an <see cref="IServiceCollection" />.
    /// </summary>
    public static class MessageBrokerRegistration
    {
        /// <summary>
        /// Registers the given Message Broker as a service in the <see cref="IServiceCollection" />.
        /// </summary>
        /// <typeparam name="T">The type of the message object</typeparam>
        /// <param name="services">The <see cref="IServiceCollection" /> to add services to</param>
        /// <returns>The same service collection so that multiple calls can be chained.</returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IServiceCollection AddInMemoryMessageBroker<T>(this IServiceCollection services) 
            where T : class
        {
            if (services == null)
            {
                throw new ArgumentNullException(nameof(services));
            }

            services.AddSingleton<IMessageBroker<T>, InMemoryMessageBroker<T>>();

            return services;
        }
    }
}
