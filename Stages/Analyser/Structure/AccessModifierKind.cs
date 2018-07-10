using System;
using System.Collections.Generic;
using System.Text;

namespace Cdelta.Analyser.Structure
{
    public enum AccessModifierKind
    {
        Public              = 0,
        Protected           = 1,
        Internal            = 2,
        ProtectedInternal   = 3,
        Private             = 4,
        PrivateProtected    = 5
    }
}
