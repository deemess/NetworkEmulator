using System;
using System.Runtime.Serialization;

namespace NetworkEmulator
{
	/// <summary>
	/// Desctiption of GatewayAlreadyDefined.
	/// </summary>
	public class GatewayAlreadyDefined : Exception, ISerializable
	{
		public GatewayAlreadyDefined()
		{
		}

	 	public GatewayAlreadyDefined(string message) : base(message)
		{
		}

		public GatewayAlreadyDefined(string message, Exception innerException) : base(message, innerException)
		{
		}

		// This constructor is needed for serialization.
		protected GatewayAlreadyDefined(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
	
	public class DeviceAlreadyExist : Exception, ISerializable
	{
		public DeviceAlreadyExist()
		{
		}

	 	public DeviceAlreadyExist(string message) : base(message)
		{
		}

		public DeviceAlreadyExist(string message, Exception innerException) : base(message, innerException)
		{
		}

		// This constructor is needed for serialization.
		protected DeviceAlreadyExist(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
	
	public class IPConflict : Exception, ISerializable
	{
		public IPConflict()
		{
		}

	 	public IPConflict(string message) : base(message)
		{
		}

		public IPConflict(string message, Exception innerException) : base(message, innerException)
		{
		}

		// This constructor is needed for serialization.
		protected IPConflict(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}