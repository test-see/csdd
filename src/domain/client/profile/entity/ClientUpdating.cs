﻿using Mediator.Net.Contracts;

namespace domain.client.profile.entity
{
    public class ClientUpdating : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }

    }
}
