﻿using System;
using System.Collections.Generic;
using System.Text;

namespace YoutubeCrawlDotnet.Shared.DTOs
{
    public class PaginatedResponse<T>
    {
        public T Response { get; set; }
        public int TotalAmountPages { get; set; }
    }
}
