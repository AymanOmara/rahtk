using System;
using System.Globalization;
using Rahtk.Domain.Features.User;

namespace Rahtk.Application.Features.User.DTO
{
    public static class NotificationModelMapper
    {
        public static NotificationModel ToModel(this NotificationEntity entity)
        {
            return new NotificationModel
            {
                Date = entity.Date.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture),
                Id = entity.Id,
                Title = entity.Title,
                Body = entity.Body,
            };
        }
    }
}

