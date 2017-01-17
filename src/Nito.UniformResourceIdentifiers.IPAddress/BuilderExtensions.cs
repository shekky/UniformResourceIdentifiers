﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Nito.UniformResourceIdentifiers.Helpers;

namespace Nito.UniformResourceIdentifiers
{
    /// <summary>
    /// Provides utility methods for URIs to work with <see cref="IPAddress"/> instances.
    /// </summary>
    public static class BuilderExtensions
    {
        /// <summary>
        /// Applies the host portion of the authority to this builder, overwriting any existing host.
        /// </summary>
        /// <typeparam name="T">The type of the builder.</typeparam>
        /// <param name="builder">The builder.</param>
        /// <param name="host">The host IP address.</param>
        public static T WithHost<T>(this UniformResourceIdentifierBuilderBase<T> builder, IPAddress host)
            where T : UniformResourceIdentifierBuilderBase<T>
        {
            if (host.AddressFamily == AddressFamily.InterNetwork)
                return builder.WithHost(host.ToString());
            if (host.AddressFamily == AddressFamily.InterNetworkV6)
                return builder.WithHost($"[{host}]"); // TODO: ZoneId?
            throw new InvalidOperationException($"IP Address is not an IPv4 or IPv6 address: {host}");
        }
    }
}