using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace CIM_Labyrint
{
    class Director
    {
        private IBuilder builder;
        private Thread thread;

        public Director(IBuilder builder)
        {
            this.builder = builder;
        }

        public Director(Thread thread)
        {
            this.thread = thread;
        }

        public GameObject Construct()
        {
            builder.BuildGameObject();

            return builder.GetResult();
        }
    }
}
