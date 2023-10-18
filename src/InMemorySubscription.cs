#region ENBREA - Copyright (c) 2023 STÜBER SYSTEMS GmbH
/*    
 *    ENBREA 
 *    
 *    Copyright (c) 2023 STÜBER SYSTEMS GmbH
 *
 *    Licensed under the MIT License, Version 2.0. 
 * 
 */
#endregion

using Microsoft.Extensions.Logging;
using System;

namespace Enbrea.MessageBroker
{
    public class InMemorySubscription<T> : ISubscription<T>
        where T : class
    {
        private readonly ILogger<IMessageBroker<T>> _logger;
        private readonly Func<string, T, bool> _messageHandler;
        private readonly string _routingKey;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBroker<T>"/> class.
        /// </summary>
        /// <param name="routingKey">The routing key</param>
        /// <param name="messageHandler">A delegate for handling the incoming message</param>
        /// <param name="logger">A Logger implementation</param>
        public InMemorySubscription(string routingKey, Func<string, T, bool> messageHandler, ILogger<IMessageBroker<T>> logger)
        {
            _routingKey = routingKey;
            _messageHandler = messageHandler;
            _logger = logger;
        }

        /// <summary>
        /// Delivery of a message.
        /// </summary>
        /// <param name="routingKey">The rpouting key</param>
        /// <param name="message">The message</param>
        /// <returns>TRUE, if message was processed</returns>
        public bool Deliver(string routingKey, T message)
        {
            if (RoutingKeys.Match(_routingKey, routingKey))
            {
                return _messageHandler(routingKey, message);
            }
            else
            {
                return false;
            }
        }
    }
}
