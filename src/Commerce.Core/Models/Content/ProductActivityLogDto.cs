﻿using AutoMapper;
using Commerce.Core.Domain.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commerce.Core.Models.Content
{
    public class ProductActivityLogDto
    {
        public PostStatus FromStatus { set; get; }

        public PostStatus ToStatus { set; get; }

        public DateTime DateCreated { get; set; }

        public string? Note { set; get; }

        public string UserName { get; set; }

        public class AutoMapperProfiles : Profile
        {
            public AutoMapperProfiles()
            {
                CreateMap<ProductActivityLog, ProductActivityLogDto>();
            }
        }
    }
}
