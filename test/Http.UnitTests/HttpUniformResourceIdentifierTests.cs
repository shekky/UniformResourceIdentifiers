﻿using System;
using System.Collections.Generic;
using System.Text;
using Nito.UniformResourceIdentifiers;
using Xunit;

namespace Http.UnitTests
{
    public class HttpUniformResourceIdentifierTests
    {
        [Theory]
        [InlineData("http://www.ietf.org/rfc/rfc2396.txt", null, "www.ietf.org", null, new[] { "", "rfc", "rfc2396.txt" }, null, null)]
        public void RoundTripFormatting(string expectedUrl, string userInfo, string host, string port, IEnumerable<string> path, string query, string fragment)
        {
            var uri = new HttpUniformResourceIdentifier(host, port, path, query, fragment);
            Assert.Equal(expectedUrl, uri.ToString());

            var parsed = HttpUniformResourceIdentifier.Parse(uri.ToString());
            Assert.Equal(HttpUniformResourceIdentifier.HttpScheme, ((IUniformResourceIdentifier) parsed).Scheme);
            Assert.Equal(userInfo, ((IUniformResourceIdentifierReference) parsed).UserInfo);
            Assert.Equal(host, parsed.Host);
            Assert.Equal(port, parsed.Port);
            Assert.Equal(path, parsed.PathSegments);
            Assert.Equal(query, parsed.Query);
            Assert.Equal(fragment, parsed.Fragment);

            var deconstructed = new HttpUniformResourceIdentifierBuilder(uri).Build();
            Assert.Equal(HttpUniformResourceIdentifier.HttpScheme, ((IUniformResourceIdentifier)deconstructed).Scheme);
            Assert.Equal(userInfo, ((IUniformResourceIdentifierReference) deconstructed).UserInfo);
            Assert.Equal(host, deconstructed.Host);
            Assert.Equal(port, deconstructed.Port);
            Assert.Equal(path, deconstructed.PathSegments);
            Assert.Equal(query, deconstructed.Query);
            Assert.Equal(fragment, deconstructed.Fragment);

            var systemUri = uri.ToUri();
            Assert.True(systemUri.IsAbsoluteUri);
            Assert.Equal(uri.ToString(), systemUri.ToString());
        }

        [Fact]
        public void Parse_InvalidScheme_ThrowsException() => Assert.Throws<ArgumentException>(() => HttpUniformResourceIdentifier.Parse("https://www.example.com/"));
    }
}
