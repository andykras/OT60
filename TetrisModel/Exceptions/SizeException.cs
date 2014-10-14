using System;

namespace TetrisModel
{
  [Serializable]
  public class SizeException : Exception
  {
    /// <summary>
    /// Initializes a new instance of the <see cref="T:SizeException"/> class
    /// </summary>
    public SizeException()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:SizeException"/> class
    /// </summary>
    /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
    public SizeException(string message) : base(message)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:SizeException"/> class
    /// </summary>
    /// <param name="message">A <see cref="T:System.String"/> that describes the exception. </param>
    /// <param name="inner">The exception that is the cause of the current exception. </param>
    public SizeException(string message, Exception inner) : base(message, inner)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="T:SizeException"/> class
    /// </summary>
    /// <param name="context">The contextual information about the source or destination.</param>
    /// <param name="info">The object that holds the serialized object data.</param>
    protected SizeException(System.Runtime.Serialization.SerializationInfo info, System.Runtime.Serialization.StreamingContext context) : base(info, context)
    {
    }

    public override string ToString()
    {
      return string.Format("[SizeException: Message={0}]", Message);
    }
  }
}

