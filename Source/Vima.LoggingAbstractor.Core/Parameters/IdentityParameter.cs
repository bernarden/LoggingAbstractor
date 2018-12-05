using System;

namespace Vima.LoggingAbstractor.Core.Parameters
{
    /// <summary>
    /// Represents identity parameter.
    /// </summary>
    public class IdentityParameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IdentityParameter"/> class.
        /// </summary>
        /// <param name="identity">The identity.</param>
        /// <param name="name">The name. Can be null.</param>
        public IdentityParameter(string identity, string name)
        {
            Identity = identity ?? throw new ArgumentNullException(nameof(identity));
            Name = name;
        }

        /// <summary>
        /// Gets the identity.
        /// </summary>
        /// <value>
        /// The identity.
        /// </value>
        public string Identity { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }
    }
}