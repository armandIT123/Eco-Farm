﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoFarm;

public interface IPlatformHttpMessageHandler
{
    HttpMessageHandler GetHttpMessageHandler();
}
