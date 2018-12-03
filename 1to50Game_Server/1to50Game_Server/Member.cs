using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace _1to50Game_Server
{
    class Member
    {
        string ID;
        public Socket client;

        public Member(string id, Socket soc)
        {
            ID = id;
            client = soc;
        }

        public void setId(string id)
        {
            ID = id;
        }

        public string getId()
        {
            return ID;
        }
            
    }
}
