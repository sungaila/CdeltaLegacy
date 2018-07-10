using System;
using System.Collections.Generic;
using System.Text;

namespace Cdelta.Analyser.Structure
{
    public interface IStructure
    {
        void Finish();

        void Verify();
    }
}
