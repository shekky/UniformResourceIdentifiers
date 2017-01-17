﻿using System;
using System.Collections.Generic;

namespace Nito.UniformResourceIdentifiers.Helpers
{
    /// <summary>
    /// A URI builder base type for HTTP and HTTPS URIs.
    /// </summary>
    public abstract class HttpUniformResourceIdentifierBuilderBase<T> : UniformResourceIdentifierBuilderBase<T>
        where T : HttpUniformResourceIdentifierBuilderBase<T>
    {
        /// <summary>
        /// User information is no longer valid on HTTP or HTTPS URIs, as of RFC7230. This method throws <c>InvalidOperationException</c> if <paramref name="userInfo"/> is not <c>null</c>.
        /// </summary>
        /// <param name="userInfo">The user information. Must be <c>null</c>.</param>
        public override T WithUserInfo(string userInfo)
        {
            if (userInfo != null)
                throw new InvalidOperationException("HTTP/HTTPS URIs can no longer have UserInfo portions, as of RFC7230.");
            UserInfo = null;
            return (T)this;
        }

        /// <summary>
        /// Applies the query string to this builder, overwriting any existing query.
        /// </summary>
        /// <param name="values">The query values to encode.</param>
        public virtual T WithQueryValues(IEnumerable<KeyValuePair<string, string>> values) => WithQuery(Util.FormUrlEncode(values));

        // TODO: Fragment path segment support? Other query/fragment support?
    }
}
