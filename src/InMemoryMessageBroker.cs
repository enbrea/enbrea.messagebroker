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
using System.Collections.Concurrent;

namespace Enbrea.MessageBroker
{
    /// <summary>
    /// An in-memory implementation of the <see cref="IMessageBroker<T>"/> interface.
    /// </summary>
    /// <typeparam name="T">The typ of the message object</typeparam>
    public class InMemoryMessageBroker<T> : IMessageBroker<T>
        where T : class
    {
        private readonly ConcurrentDictionary<Guid, ISubscription<T>> _subscriptionDict;
        private readonly ILogger<IMessageBroker<T>> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="InMemoryMessageBroker<T>"/> class.
        /// </summary>
        /// <param name="logger">A Logger implementation</param>
        public InMemoryMessageBroker(ILogger<IMessageBroker<T>> logger)
        {
            _logger = logger;
            _subscriptionDict = new ConcurrentDictionary<Guid, ISubscription<T>>();
        }

        /// <summary>
        /// Returns whether a subscription exists.
        /// </summary>
        /// <param name="subscriptionId">The subscription Id</param>
        /// <returns>TRUE, if subscription exists</returns>
        public bool ContainsSubscribtion(Guid subscriptionId)
        {
            return _subscriptionDict.ContainsKey(subscriptionId);
        }

        /// <summary>
        /// Returns the number of active subscriptions.
        /// </summary>
        /// <returns>Number of subscriptions</returns>
        public int GetSubscriptionCount()
        {
            return _subscriptionDict.Count;
        }

        /// <summary>
        /// Publishs a message 
        /// </summary>
        /// <param name="routingKey">The routing key</param>
        /// <param name="message">The message object</param>
        /// <returns>The number of deliveries</returns>
        public int Publish(string routingKey, T message)
        {
            int delivered = 0;
            foreach (var subscription in _subscriptionDict)
            {
                if (subscription.Value.Deliver(routingKey, message))
                {
                    delivered++;
                }
            }
            return delivered;
        }

        /// <summary>
        /// Subscribes to a routing key.
        /// </summary>
        /// <param name="routingKey">The routing key</param>
        /// <param name="messageHandler">A delegate for handling the incoming message</param>
        /// <returns>The subscription Id</returns>
        public Guid Subscribe(string routingKey, Func<string, T, bool> messageHandler)
        {
            var subscriptionId = Guid.NewGuid();
            if  (_subscriptionDict.TryAdd(subscriptionId, new InMemorySubscription<T>(routingKey, messageHandler, _logger)))
            {
                return subscriptionId;
            }
            else
            {
                throw new Exception("Fehler"); 
            }
        }

        /// <summary>
        /// Unsubscribes a subscription.
        /// </summary>
        /// <param name="subscriptionId">The subscription Id</param>
        /// <returns>TRUE, if subscription was removed</returns>
        public bool Unsubscribe(Guid subscriptionId)
        {
            return _subscriptionDict.TryRemove(subscriptionId, out ISubscription<T> subscription);
        }

        /// <summary>
        /// Unsubscribes all subscriptions.
        /// </summary>
        public void UnsubscribeAll()
        {
            _subscriptionDict.Clear();
        }
    }
}
