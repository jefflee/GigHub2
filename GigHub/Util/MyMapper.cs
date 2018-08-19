using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using GigHub.Controllers.Api;
using GigHub.Core.Dtos;
using GigHub.Core.Models;

namespace GigHub.Util
{
    public sealed class MyMapper
    {
        private static readonly Lazy<MyMapper> lazy =
            new Lazy<MyMapper>(() => new MyMapper());

        public static MyMapper Instance
        {
            get { return lazy.Value; }
        }

        private MyMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<ApplicationUser, UserDto>();
                cfg.CreateMap<Gig, GigDto>();
                cfg.CreateMap<Notification, NotificationDto>();
            });

            IMapper mapper = config.CreateMapper();
            _mapper = mapper;
        }

        private IMapper _mapper;

        public IMapper Mapper
        {
            get { return _mapper; }
            set { _mapper = value; }
        }
    }
}