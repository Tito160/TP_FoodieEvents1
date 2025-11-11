using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class ErrorValidacionException : Exception
{
    public ErrorValidacionException(string message) : base(message) { }
}