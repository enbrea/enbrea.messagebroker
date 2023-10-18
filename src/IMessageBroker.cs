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

using System;

namespace Enbrea.MessageBroker
{
    /// <summary>
    /// A message broker accepts and forwards messages. This interface defines how to publish a message 
    /// and how to subscribe for receiving messages. A message can be a simple string or any other object.
    /// </summary>
    /// <typeparam name="T">The typ of the message object</typeparam>
    public interface IMessageBroker<T>
        where T : class
    {
        /// <summary>
        /// Returns whether a subscription exists.
        /// </summary>
        /// <param name="subscriptionId">The subscription Id</param>
        /// <returns>TRUE, if subscription exists</returns>
        bool ContainsSubscribtion(Guid subscriptionId);

        /// <summary>
        /// Returns the number of active subscriptions.
        /// </summary>
        /// <returns>Number of subscriptions</returns>
        int GetSubscriptionCount();

        /// <summary>
        /// Publishs a message 
        /// </summary>
        /// <param name="routingKey">The routing key</param>
        /// <param name="message">The message object</param>
        /// <returns>The number of deliveries</returns>
        int Publish(string routingKey, T message);

        /// <summary>
        /// Subscribes to a routing key.
        /// </summary>
        /// <param name="routingKey">The routing key</param>
        /// <param name="messageHandler">A delegate for handling the incoming message</param>
        /// <returns>The subscription Id</returns>
        Guid Subscribe(string routingKey, Func<string, T, bool> messageHandler);

        /// <summary>
        /// Unsubscribes a subscription.
        /// </summary>
        /// <param name="subscriptionId">The subscription Id</param>
        /// <returns>TRUE, if subscription was removed</returns>
        bool Unsubscribe(Guid subscriptionId);

        /// <summary>
        /// Unsubscribes all subscriptions.
        /// </summary>
        void UnsubscribeAll();
    }
}
