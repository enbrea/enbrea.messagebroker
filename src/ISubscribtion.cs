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

namespace Enbrea.MessageBroker
{
    /// <summary>
    /// A subscription determines when and how a message is processed.
    /// </summary>
    /// <typeparam name="T">The typ of the message object</typeparam>
    public interface ISubscription<T>
        where T : class
    {
        /// <summary>
        /// Delivery of a message.
        /// </summary>
        /// <param name="routingKey">The rpouting key</param>
        /// <param name="message">The message</param>
        /// <returns>TRUE, if message was processed</returns>
        bool Deliver(string routingKey, T message);
    }
}
