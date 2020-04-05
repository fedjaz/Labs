using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanProject
{
    interface IRunner
    {
        float Run(float distance);

        string Name { get; }
        string Surname { get; }
    }
}
