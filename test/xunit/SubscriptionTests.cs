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

using Xunit;

namespace Enbrea.MessageBroker.Tests
{
    public class RoutingKeyTests
    {
        [Fact]
        public void MatchingRoutingKeys()
        {
            Assert.True(RoutingKeys.Match("word1.word2.word3", "word1.word2.word3"));
            Assert.True(RoutingKeys.Match("*.word2.word3", "word1.word2.word3"));
            Assert.True(RoutingKeys.Match("word1.*.word3", "word1.word2.word3"));
            Assert.True(RoutingKeys.Match("#", "word1.word2.word3"));
            Assert.True(RoutingKeys.Match("#.word3", "word1.word2.word3"));
            Assert.True(RoutingKeys.Match("word1.#", "word1.word2.word3"));
        }

        [Fact]
        public void NoneMatchingRoutingKeys()
        {
            Assert.False(RoutingKeys.Match("Word1.Word2.Word3", "word1.word2.word3"));
            Assert.False(RoutingKeys.Match("#word2.word3", "word1.word2.word3"));
            Assert.False(RoutingKeys.Match("word1#word3", "word1.word2.word3"));
            Assert.False(RoutingKeys.Match("*", "word1.word2.word3"));
            Assert.False(RoutingKeys.Match("*.word3", "word1.word2.word3"));
            Assert.False(RoutingKeys.Match("word1.*", "word1.word2.word3"));
        }
    }
}
