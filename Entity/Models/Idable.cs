﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Entity.Models
{
    public interface Idable
    {
        int GetId();
        void SetId(int id);
    }
}
