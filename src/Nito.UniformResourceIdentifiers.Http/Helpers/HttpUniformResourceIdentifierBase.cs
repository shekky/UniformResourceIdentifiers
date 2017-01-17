﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace Nito.UniformResourceIdentifiers.Helpers
{
    /// <summary>
    /// Base class for HTTP and HTTPS URIs, providing common functionality.
    /// </summary>
    public abstract class HttpUniformResourceIdentifierBase : UniformResourceIdentifier
    {
        /// <summary>
        /// Constructs a new URI instance.
        /// </summary>
        /// <param name="scheme">The scheme. This is converted to lowercase. Must be a valid scheme as defined by <see cref="Util.IsValidScheme"/>. Should be <c>"http"</c> or <c>"https"</c>.</param>
        /// <param name="host">The host name portion of the authority, if any. This is converted to lowercase. This may be <c>null</c> to indicate no host name, or the empty string to indicate an empty host name.</param>
        /// <param name="port">The port portion of the authority, if any. This may be <c>null</c> to indicate no port, or the empty string to indicate an empty port. This must be <c>null</c>, the empty string, or a valid port as defined by <see cref="Util.IsValidPort"/>.</param>
        /// <param name="pathSegments">The path segments. Dot segments are normalized. May not be <c>null</c>, neither may any element be <c>null</c>.</param>
        /// <param name="query">The query. This may be <c>null</c> to indicate no query, or the empty string to indicate an empty query.</param>
        /// <param name="fragment">The fragment. This may be <c>null</c> to indicate no fragment, or the empty string to indicate an empty fragment.</param>
        protected HttpUniformResourceIdentifierBase(string scheme, string host, string port, IEnumerable<string> pathSegments, string query, string fragment)
            : base(scheme, null, host, NormalizePort(port), NormalizePath(pathSegments), query, fragment)
        {
            if (host == null)
                throw new ArgumentNullException(nameof(host));
            if (host == "")
                throw new ArgumentException("Host is required.", nameof(host));
        }

        private static string NormalizePort(string port) => port == "" || port == "80" ? null : port;

        private static IEnumerable<string> NormalizePath(IEnumerable<string> pathSegments)
        {
            var first = true;
            foreach (var segment in pathSegments ?? Enumerable.Empty<string>())
            {
                if (first)
                {
                    if (segment != "")
                        yield return "";
                    yield return segment;
                    first = false;
                }
                else
                    yield return segment;
            }
            if (first)
                yield return "";
        }

        /// <summary>
        /// Parses the query of the URI as a series of name/value pairs, e.g., "q=test&amp;page=4". This can be <c>null</c> if there is no query, or an empty collection if the query is empty. Names can be empty but never <c>null</c>; values can be <c>null</c> (if there is no <c>=</c>) or empty.
        /// </summary>
        public virtual IEnumerable<KeyValuePair<string, string>> QueryValues => Query == null ? null : Util.FormUrlDecodeValues(Query);
    }
}
